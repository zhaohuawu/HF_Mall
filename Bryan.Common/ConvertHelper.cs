using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bryan.Common
{
    public class ConvertHelper
    {
        #region 单例
        private static ConvertHelper _instance = null;
        static object obj = new object();
        public static ConvertHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (obj)
                    {
                        if (_instance == null)
                        {
                            _instance = new ConvertHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        private ConvertHelper() { }
        #endregion

        /// <summary>
        /// 转换成整形
        /// </summary>
        /// <param name="o">需要转换的字符</param>
        /// <param name="i">转换失败的默认值</param>
        /// <returns></returns>
        public int ConvertToInt(object o, int i = 0)
        {
            int.TryParse(o.ToString(), out i);
            return i;
        }
        
        public sbyte ConvertToSbyte(object o, sbyte i = 0)
        {
            sbyte.TryParse(o.ToString(), out i);
            return i;
        }

        public long ConvertToLong(object o, long i = 0)
        {
            long.TryParse(o.ToString(), out i);
            return i;
        }

        public double ConvertToDouble(object o, double i = 0)
        {
            double.TryParse(o.ToString(), out i);
            return i;
        }

        public decimal ConvertToDecimal(object o, decimal i = 0)
        {
            decimal.TryParse(o.ToString(), out i);
            return i;
        }

        /// <summary>
        /// 转换成日期型
        /// </summary>
        /// <param name="o">需要转换的字符</param>
        /// <returns></returns>
        public DateTime ConvertToDate(object o)
        {
            DateTime d = DateTime.MinValue;
            DateTime.TryParse(o.ToString(), out d);
            return d;
        }

        /// <summary>
        /// 字符串转枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public T ConvertToEnum<T>(string e)
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
        public T ConvertToEnum<T>(int e)
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

        public string ConvertToEn(string text)
        {
            const string s1 = "。；，？！、“”‘’";
            const string s2 = @".;,?!\""""''";
            char[] c = text.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int n = s1.IndexOf(c[i]);
                if (n != -1) c[i] = s2[n];
            }
            return new string(c);
        }
        public string ConvertToDouHao(string str)
        {
            return str.Replace('，', ',');
        }
    }
}
