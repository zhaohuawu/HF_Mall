using System;
using System.Text.RegularExpressions;

namespace Bryan.Common
{
    /// <summary>
    /// GUID辅助类
    /// </summary>
    public abstract class GUIDHelper
    {
        #region 公共属性方法

        /// <summary>
        /// 产生一个新的GUID(小写)
        /// </summary>
        /// <returns>GUID字符串(小写)</returns>
        public static string CreateGUID()
        {
            return Guid.NewGuid().ToString().ToLower();
        }

        /// <summary>
        /// 产生一个新的GUID(小写)，并指定是否显示中间的横线分隔符 
        /// </summary>
        /// <param name="bShowLine">否显示中间的横线分隔符</param>
        /// <returns>GUID字符串(小写)</returns>
        public static string CreateGUIDWithNoSplit()
        {
            string str = CreateGUID();
            str = str.Replace("-", "");
            return str;
        }

        /// <summary>
        /// 验证给定字符串是否是合法的GUID
        /// </summary>
        /// <param name="strInputData">待验证字符串</param>
        /// <returns>是|否</returns>
        public static bool IsGuid(string strInputData)
        {
            bool flag = false;
            string pattern = @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$";
            try
            {
                if ((strInputData != null) && !strInputData.Equals(""))
                {
                    flag = Regex.IsMatch(strInputData, pattern);
                }
            }
            catch (Exception)
            {
            }
            return flag;
        }

        /// <summary> 
        /// 根据GUID获取16位的唯一字符串 
        /// </summary>  
        /// <returns></returns> 
        public static string GetStringID()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 根据GUID获取19位的唯一数字序列
        /// </summary>
        /// <returns></returns>
        public static long GetLong()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>  
        /// 生成22位唯一的数字 并发可用  
        /// </summary>  
        /// <returns></returns>
        public static string GetUniqueId()
        {
            System.Threading.Thread.Sleep(1); //保证yyyyMMddHHmmssffff唯一  
            Random d = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string strUnique = DateTime.Now.ToString("yyyyMMddHHmmssffff") + d.Next(1000, 9999);
            return strUnique;
        }

        #endregion
    }
}
