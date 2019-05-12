using System;
using System.Collections.Generic;
using System.Text;

namespace Bryan.Common.Extension
{
    public static class UtilSafeExtension
    {
        public static string ToSafeString(this object obj)
        {
            if (obj == null)
                return string.Empty;
            return obj.ToString();
        }

        /// <summary>
        /// 转换成整形
        /// </summary>
        /// <param name="o">需要转换的字符</param>
        /// <param name="i">转换失败的默认值</param>
        /// <returns></returns>
        public static int ToSafeInt(this object o, int i = 0)
        {
            int.TryParse(o.ToString(), out i);
            return i;
        }

        public static sbyte ToSafeSbyte(this object o, sbyte i = 0)
        {
            sbyte.TryParse(o.ToString(), out i);
            return i;
        }
        
        public static long ToSafeLong(this object o, long i = 0)
        {
            long.TryParse(o.ToString(), out i);
            return i;
        }

        public static double ToSafeDouble(this object o, double i = 0)
        {
            double.TryParse(o.ToString(), out i);
            return i;
        }

        public static decimal ToSafeDecimal(this object o, decimal i = 0)
        {
            decimal.TryParse(o.ToString(), out i);
            return i;
        }

        /// <summary>
        /// 转换成日期型
        /// </summary>
        /// <param name="o">需要转换的字符</param>
        /// <returns></returns>
        public static DateTime ToSafeDate(this object o)
        {
            DateTime d = DateTime.MinValue;
            DateTime.TryParse(o.ToString(), out d);
            return d;
        }

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
                return null;
        }

        /// <summary>
        /// 字符串转枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public static T ToSafeEnum<T>(this string e)
        {
            Type eType = typeof(T);
            object obj = Enum.Parse(eType, e);
            if (obj == null)
            {
                int v = (int)Enum.GetValues(eType).GetValue(0);
                return (T)Enum.ToObject(eType, v);
            }
            return (T)obj;
        }

        /// <summary>
        /// 整型枚举值转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public static T ToSafeEnum<T>(this int e)
        {
            Type eType = typeof(T);
            object obj = Enum.ToObject(eType, e);
            if (obj == null)
            {
                int v = (int)Enum.GetValues(eType).GetValue(0);
                return (T)Enum.ToObject(eType, v);
            }
            return (T)obj;
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
