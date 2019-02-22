using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AESUtil
    {
        private static string _key = "DVfRj6DUDMa5kyRr9AjfzxzWwhXWs5GM";//必须为16的倍数
        private static string _iv = "BW7NEZw53R4sQMNZ";//必须为16的倍数

        /// <summary>
        /// 使用AESUtil固定的key和iv
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string EncryptToHex(string data)
        {
            return EncryptToHex(data, _key, _iv);
        }

        /// <summary>
        /// 使用AESUtil固定的key和iv
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static string DecryptHex(string hexStr)
        {
            return DecryptHex(hexStr, _key, _iv);
        }

        /// <summary>
        /// 使用传入的key且iv=key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptToHex(string data, string key)
        {
            return EncryptToHex(data, key, key);
        }

        /// <summary>
        /// 使用传入的key且iv=key
        /// </summary>
        /// <param name="hexStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptHex(string hexStr, string key)
        {
            return DecryptHex(hexStr, key, key);
        }

        /// <summary>
        /// 使用传入的key和iv
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string EncryptToHex(string data, string key, string iv)
        {
            Byte[] resultArray = AesEncrypt(data, key, iv);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < resultArray.Length; i++)
                sb.Append(resultArray[i].ToString("X2"));//"x2"小写，"X2"为大写

            return sb.ToString();
        }

        /// <summary>
        /// 使用传入的key和iv
        /// </summary>
        /// <param name="hexStr"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string DecryptHex(string hexStr, string key, string iv)
        {
            Byte[] toDecryptArray = new Byte[hexStr.Length / 2];
            for (int i = 0; i < toDecryptArray.Length; i++)
                toDecryptArray[i] = Convert.ToByte(hexStr.Substring(i * 2, 2), 16);//每两个字符一个byte

            return AesDecrypt(toDecryptArray, key, iv);
        }

        /// <summary>
        /// 使用AESUtil固定的key和iv，加密的结果为加密后的原始字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encrypt(string data)
        {
            return Convert.ToBase64String(AesEncrypt(data, _key, _iv));
        }

        /// <summary>
        /// 使用传入的key且iv=key，加密的结果为加密后的原始字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string data, string key)
        {
            return Convert.ToBase64String(AesEncrypt(data, key, key));
        }

        /// <summary>
        /// 使用传入的key和iv，加密的结果为加密后的原始字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, string iv)
        {
            return Convert.ToBase64String(AesEncrypt(data, key, iv));
        }

        /// <summary>
        /// 使用AESUtil固定的key和iv
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptStr)
        {
            return Decrypt(encryptStr, _key, _iv);
        }

        /// <summary>
        /// 使用传入的key且iv=key
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptStr, string key)
        {
            return Decrypt(encryptStr, key, key);
        }

        /// <summary>
        /// 使用传入的key和iv，CipherMode.CBC，PaddingMode.PKCS7
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptStr, string key, string iv)
        {
            Byte[] toDecryptArray = Convert.FromBase64String(encryptStr);
            return AesDecrypt(toDecryptArray, key, iv);
        }

        /// <summary>
        /// 使用传入的key和iv,CipherMode.CBC,PaddingMode.PKCS7,KeySize=256,BlockSize=128
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv">只取16位</param>
        /// <returns></returns>
        public static byte[] AesEncrypt(string data, string key, string iv)
        {
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(data);
            Byte[] resultArray;

            using (RijndaelManaged rm = new RijndaelManaged())
            {
                rm.KeySize = 256;
                rm.BlockSize = 128;
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                if (key.Length == 44 && key.EndsWith("="))
                {
                    rm.Key = Convert.FromBase64String(key);//这个对于key的值要求比较高，需要符合计算后的长度标准
                    rm.IV = Convert.FromBase64String(iv);//这个对于iv的值要求比较高
                }
                else
                {
                    rm.Key = Encoding.UTF8.GetBytes(key);//这个只需要key的字符串达到16的整数倍即可
                    rm.IV = Encoding.UTF8.GetBytes(iv.Substring(0, 16));//16位
                }

                ICryptoTransform itf = rm.CreateEncryptor();
                resultArray = itf.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            }

            return resultArray;
        }

        /// <summary>
        /// 使用传入的key和iv,CipherMode.CBC,PaddingMode.PKCS7,KeySize=256,BlockSize=128
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <param name="key"></param>
        /// <param name="iv">只取16位</param>
        /// <returns></returns>
        public static string AesDecrypt(byte[] toDecryptArray, string key, string iv)
        {
            Byte[] resultArray;
            using (RijndaelManaged rm = new RijndaelManaged())
            {
                rm.KeySize = 256;
                rm.BlockSize = 128;
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                if (key.Length == 44 && key.EndsWith("="))
                {
                    rm.Key = Convert.FromBase64String(key);//这个对于key的值要求比较高，需要符合计算后的长度标准
                    rm.IV = Convert.FromBase64String(iv);//这个对于iv的值要求比较高
                }
                else
                {
                    rm.Key = Encoding.UTF8.GetBytes(key);//这个只需要key的字符串达到16的整数倍即可
                    rm.IV = Encoding.UTF8.GetBytes(iv.Substring(0, 16));//16
                }

                ICryptoTransform itf = rm.CreateDecryptor();
                resultArray = itf.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
            }
            return Encoding.UTF8.GetString(resultArray).Trim('\0');
        }

        /// <summary>
        /// 注册密码加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPsw(string password)
        {
            string iv = "nk7TWCsj275NMNfp";
            string key = "ATKTyyZ3EN3FiCSWMxVeE3d4kP7MvSnt";
            return EncryptToHex(password, key, iv);
        }

        public static string DecryptPsw(string password)
        {
            string iv = "nk7TWCsj275NMNfp";
            string key = "ATKTyyZ3EN3FiCSWMxVeE3d4kP7MvSnt";
            return DecryptHex(password, key, iv);
        }

    }
}
