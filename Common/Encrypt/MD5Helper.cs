using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Common
{
    /// <summary>
    /// Md5辅助类
    /// </summary>
    public class MD5Helper
    {
        /// <summary>
        /// 转换为md5字符串
        /// </summary>
        /// <param name="sourceString">原字符串</param>
        /// <returns></returns>
        public static string ToMD5(string sourceString)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            return BitConverter.ToString(provider.ComputeHash(Encoding.Default.GetBytes(sourceString))).Replace("-", "");
        }

        /// <summary>
        /// 转换为md5字符串小写
        /// </summary>
        /// <param name="sourceString">原字符串</param>
        /// <returns></returns>
        public static string ToMD5Lower(string sourceString)
        {
            return ToMD5(sourceString).ToLower();
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="encypStr">加密字符串</param>
        /// <param name="charset">编码方式</param>
        /// <returns></returns>
        public static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception ex)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }
        
    }
}