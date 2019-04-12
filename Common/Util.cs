using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bryan.Common
{
    public class Util
    {
        #region 单例
        private static Util _instance = null;
        static object obj = new object();
        public static Util Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (obj)
                    {
                        if (_instance == null)
                        {
                            _instance = new Util();
                        }
                    }
                }
                return _instance;
            }
        }
        private Util() { }
        #endregion

        /// <summary>  
        /// 根据年月日计算星期几(Label2.Text=CaculateWeekDay(2004,12,9);)  
        /// </summary>  
        /// <param name="y">年</param>  
        /// <param name="m">月</param>  
        /// <param name="d">日</param>  
        /// <returns></returns>  
        public int CaculateWeekDay(DateTime date)
        {
            int y = date.Year;
            int m = date.Month;
            int d = date.Day;
            if (m == 1) m = 13;
            if (m == 2) m = 14;
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7 + 1;
            return week;
        }

        public string GetLastStr(string str, int len)
        {
            if (str == null || str.Length <= len)
                return str;
            else
                return str.Substring(str.Length - len);
        }

        public string GetFiltStr(int num, int len)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < len - num.ToString().Length; i++)
            {
                str.Append("0");
            }
            str.Append(num);
            return str.ToString();
        }

        public string GetFileStr(int typeId, int housesId, int num)
        {
            return GetFiltStr(typeId, num) + GetFiltStr(housesId, num);
        }

        public string GetFiltLinkStr(string str, int typeId, int housesId, int num)
        {
            var arr = str.Split('_');
            arr[num] = GetFiltStr(typeId, 3) + GetFiltStr(housesId, 3);
            StringBuilder strB = new StringBuilder();
            for (int i = 0; i < arr.Count(); i++)
            {
                strB.Append(arr[i]);
                if (i < arr.Count() - 1)
                    strB.Append("_");
            }

            return strB.ToString();
        }

    }
}
