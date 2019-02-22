using System;

namespace Common.Infrastructure
{
    /// <summary>
    /// 时间日期辅助类
    /// </summary>
    public static class DateTimeHelper
    {
        private static readonly DateTime baseDate = new DateTime(1970, 1, 1);

        #region 公共属性方法



        /// <summary>
        /// 返回标准日期格式 
        /// </summary>
        public static string GetDate()
        {
            return GetDate(DateTime.Now);
        }

        /// <summary>
        /// 返回标准日期格式
        /// </summary>
        public static string GetDate(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回标准时间格式
        /// </summary>
        public static string GetTime()
        {
            return GetTime(DateTime.Now);
        }

        /// <summary>
        /// 返回标准时间格式
        /// </summary>
        public static string GetTime(this DateTime dt)
        {
            return dt.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回标准日期时间格式
        /// </summary>
        public static string GetDateTime()
        {
            return GetDateTime(DateTime.Now);
        }

        /// <summary>
        /// 返回标准日期时间格式 
        /// </summary>
        public static string GetDateTime(this DateTime dt)
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准日期时间格式 
        /// </summary>
        public static string GetDateCh(DateTime dt)
        {
            var strArr = dt.ToString("yyyy-MM-dd").Split('-');
            return strArr[0] + "年" + strArr[1] + "月" + strArr[2] + "日";
        }

        public static string GetDateStr(this DateTime dt)
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssff");
        }

        /// <summary>
        /// C#时间转换成长整形时间(1970.1.1日至今的秒数)
        /// </summary>
        public static long ToCommonTime(this DateTime currDate)
        {
            return (long)(currDate.ToUniversalTime() - baseDate).TotalSeconds;
        }

        public static long GetTimeSpan()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        public static TimeSpan GetTimeSpanByNow()
        {
            return DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
        }

        public static TimeSpan GetTimeSpan(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new
            TimeSpan(DateTime2.Ticks);
            return ts1.Subtract(ts2).Duration();
        }

        /// <summary>
        /// 长整形时间转换成c#时间
        /// </summary>
        public static DateTime ToCsharpTime(long time)
        {
            if (time == 0)
                return DateTime.Now;
            return baseDate.AddSeconds(time).ToLocalTime();
        }

        #endregion
    }
}
