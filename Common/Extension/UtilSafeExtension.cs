using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extension
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
    }
}
