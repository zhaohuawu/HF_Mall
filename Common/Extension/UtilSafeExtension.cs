using System;
using System.Collections.Generic;
using System.Text;

namespace Bryan.Common.Extension
{
    public static class UtilSafeExtension
    {
        /// <summary>
        /// 切割为字符串数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="splitStr">切割的标识</param>
        /// <returns></returns>
        public static string[] ToSafeStrArray(this string str, params char[] splitStr)
        {
            if (!string.IsNullOrEmpty(str))
                return str.Split(splitStr);
            else
                return new string[] { };
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(this string plainText)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// Base64编码解码
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Base64Decode(this string base64EncodedData)
        {
            byte[] bytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
