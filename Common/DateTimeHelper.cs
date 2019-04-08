using System;

namespace Common
{
    /// <summary>
    /// 时间日期辅助类
    /// </summary>
    public static class DateTimeHelper
    {
        private static readonly DateTime baseDate = new DateTime(1970, 1, 1);

        #region 公共属性方法

        /// <summary>
        /// 返回标准年月日时间格式 
        /// </summary>
        public static string ToDateCh(this DateTime dt)
        {
            return string.Format("{0:F}", dt);
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
        public static DateTime ConvertToCsharpTime(long time)
        {
            if (time == 0)
                return DateTime.Now;
            return baseDate.AddSeconds(time).ToLocalTime();
        }

        #endregion
    }
}
