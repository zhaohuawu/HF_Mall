using Bryan.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.Base.Common
{
    public class Encrypt
    {
        /// <summary>
        /// 注册密码加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPsw(string password)
        {
            string iv = "nk7TWCsj275NMNfp";
            string key = "ATKTyyZ3EN3FiCSWMxVeE3d4kP7MvSnt";
            return AESUtil.EncryptToHex(password, key, iv);
        }

        public static string DecryptPsw(string password)
        {
            string iv = "nk7TWCsj275NMNfp";
            string key = "ATKTyyZ3EN3FiCSWMxVeE3d4kP7MvSnt";
            return AESUtil.DecryptHex(password, key, iv);
        }
    }
}
