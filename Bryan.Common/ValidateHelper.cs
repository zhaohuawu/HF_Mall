using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Bryan.Common
{
    /// <summary>
    /// 数据验证辅助类
    /// </summary>
    public static class ValidateHelper
    {
        #region 私有属性方法

        private static Regex RegNoSpecialLetter = new Regex(@"^[0-9a-zA-Z]+$");//英文字母和数字
        private static Regex RegLetter = new Regex(@"^[a-zA-Z]+$");//英文字母
        private static Regex RegUpperLetter = new Regex(@"^[A-Z]+$");//大写字母
        private static Regex RegLowerLetter = new Regex(@"^[a-z]+$");//小写字母
        private static Regex RegInt = new Regex(@"^[0-9]+$");//整数
        private static Regex RegIntSign = new Regex(@"^[+-]?[0-9]+$");//带符号整数
        private static Regex RegFloat = new Regex(@"^[0-9]+[.]?[0-9]+$");//浮点数
        private static Regex RegFloatSign = new Regex(@"^[+-]?[0-9]+[.]?[0-9]+$");//带符号浮点数
        private static Regex RegEmail = new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");//电子邮件
        private static Regex RegQQ = new Regex(@"^[1-9]*[1-9][0-9]*$");//QQ号码
        private static Regex RegHttpUrl = new Regex(@"((http|https)://)+([\w-]+\.)+[\w-]+(/[\w- ./?%&#=]*)?$");//Http地址
        private static Regex RegChinese = new Regex(@"^[\u4e00-\u9fa5]$");//中文
        private static Regex RegTelephone = new Regex(@"\d{3}-\d{7,8}|\d{4}-\d{7}");//电话号码
        private static Regex RegMobilePhone = new Regex(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$");//手机号码
        private static Regex RegZipCode = new Regex(@"^[1-9][0-9]{5}$");//邮编
        private static Regex RegIPV4 = new Regex(@"^((25[0-5]|2[0-4]\d|1\d\d|[1-9]\d|\d)\.){3}(25[0-5]|2[0-4]\d|1\d\d|[1-9]\d|[1-9])$");//IPV4地址
        private static Regex RegDate = new Regex(@"^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)$");//日期
        private static Regex RegTime = new Regex(@"^(20|21|22|23|[0-1]\d):[0-5]\d:[0-5]\d$");//时间

        #endregion

        #region 公共属性方法

        /// <summary>
        /// 是否英文字母和数字
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsNoSpecialLetter(this string inputData)
        {
            Match m = RegNoSpecialLetter.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否字母
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsLetter(this string inputData)
        {
            Match m = RegLetter.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否大写字母
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsUpperLetter(this string inputData)
        {
            Match m = RegUpperLetter.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否小写字母
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsLowerLetter(this string inputData)
        {
            Match m = RegLowerLetter.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否整数
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsInt(this string inputData)
        {
            Match m = RegInt.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否有符号整数
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsIntSign(this string inputData)
        {
            Match m = RegIntSign.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否浮点数[包括整数]
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsFloat(this string inputData)
        {
            Match m1 = RegInt.Match(inputData);
            Match m2 = RegFloat.Match(inputData);
            return m1.Success || m2.Success;
        }

        /// <summary>
        /// 是否有符号浮点数
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsFloatSign(this string inputData)
        {
            Match m1 = RegIntSign.Match(inputData);
            Match m2 = RegFloatSign.Match(inputData);
            return m1.Success || m2.Success;
        }

        /// <summary>
        /// 是否电子邮件地址
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsEmail(this string inputData)
        {
            Match m = RegEmail.Match(inputData.ToLower());
            return m.Success;
        }

        /// <summary>
        /// 是否QQ号码
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsQQ(this string inputData)
        {
            Match m = RegQQ.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否为Http地址
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsHttpUrl(this string inputData)
        {
            Match m = RegHttpUrl.Match(inputData.ToLower());
            return m.Success;
        }

        /// <summary>
        /// 是否含有中文字符
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsHasChinese(this string inputData)
        {
            Match m = RegChinese.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否为电话号码
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsTelephone(this string inputData)
        {
            Match m = RegTelephone.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否为手机号码
        /// </summary>
        /// <param name="inputData">待验证字符串</param>
        /// <returns>是|否</returns>
        public static bool IsMobilePhone(this string inputData)
        {
            Match m = RegMobilePhone.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否为邮政编码
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsZipCode(this string inputData)
        {
            Match m = RegZipCode.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否为IPV4地址
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsIPV4(this string inputData)
        {
            Match m = RegIPV4.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否为日期格式[如：2008-08-08]
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsDate(this string inputData)
        {
            Match m = RegDate.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否为时间格式[如：12:08:08]
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsTime(this string inputData)
        {
            Match m = RegTime.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否日期时间
        /// </summary>
        /// <param name="inputData">待判断字符串</param>
        /// <returns>是|否</returns>
        public static bool IsDateTime(this string inputData)
        {
            try
            {
                if (!string.IsNullOrEmpty(inputData))
                {
                    DateTime.Parse(inputData);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 是否为身份证号码
        /// </summary>
        /// <param name="inputData">待判定字符串</param>
        /// <returns>是|否</returns>
        public static bool IsPersonID(this string inputData)
        {
            if (inputData == null || inputData == String.Empty)
            {
                return false;
            }

            Regex reg15 = new Regex(@"^\d{15}$");
            Regex reg18 = new Regex(@"^\d{17}[0|1|2|3|4|5|6|7|8|9|x|X]$");
            if (!reg15.IsMatch(inputData) && !reg18.IsMatch(inputData))
                return false;
            else
            {
                int[] everyMonthDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                int year, month, day;
                if (inputData.Length == 18)
                {
                    year = int.Parse(inputData.Substring(6, 4));
                    month = int.Parse(inputData.Substring(10, 2));
                    day = int.Parse(inputData.Substring(12, 2));
                }
                else
                {
                    year = 1900 + int.Parse(inputData.Substring(6, 2));
                    month = int.Parse(inputData.Substring(8, 2));
                    day = int.Parse(inputData.Substring(10, 2));
                }
                if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                    everyMonthDays[1] = 29;
                if (month < 1 || month > 12) return false;
                if (day < 1 || day > everyMonthDays[month - 1]) return false;
                return true;
            }
        }

        /// <summary>
        /// 检测字符串是否为有效的ID
        /// </summary>
        /// <param name="inputData">需要检查的字符串</param>
        /// <param name="split">分隔字符</param>
        /// <returns>如果字符串为有效的ID，则为 true；否则为 false。</returns>
        public static bool IsValidId(this string inputData, string split = ",")
        {
            bool valid;
            if (string.IsNullOrEmpty(inputData))
            {
                valid = false;
            }
            else
            {
                inputData = inputData.Replace(split, string.Empty).Trim();
                if (string.IsNullOrEmpty(inputData))
                {
                    valid = false;
                }
                else
                {
                    if (IsInt(inputData))
                    {
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                    }
                }
            }
            return valid;
        }

        /// <summary>
        /// 过滤掉字符串中会引起注入攻击的字符
        /// </summary>
        /// <param name="forFilter">要过滤的字符串</param>
        /// <returns>已过滤的字符串</returns>
        public static string FilterBadChar(this string forFilter)
        {
            string tempstrChar;
            string newstrChar = string.Empty;
            if (string.IsNullOrEmpty(forFilter))
            {
                newstrChar = string.Empty;
            }
            else
            {
                tempstrChar = forFilter;
                string[] strBadChar = { "+", "'", "%", "^", "&", "?", "(", ")", "<", ">", "[", "]", "{", "}", "/", "\"", ";", ":", "Chr(34)", "Chr(0)", "--" };
                StringBuilder strBuilder = new StringBuilder(tempstrChar);
                for (int i = 0; i < strBadChar.Length; i++)
                {
                    newstrChar = strBuilder.Replace(strBadChar[i], string.Empty).ToString();
                }
                newstrChar = Regex.Replace(newstrChar, "@+", "@");
            }
            return newstrChar;
        }

        /// <summary>
        /// 过滤sql语句中like的内容
        /// </summary>
        /// <param name="strSql">like的内容</param>
        /// <returns>返回过滤后sql语句中like的内容</returns>
        public static string FilterLikeSql(this string strSql)
        {
            string tempstrChar;
            string newstrChar = string.Empty;
            if (string.IsNullOrEmpty(strSql))
            {
                newstrChar = string.Empty;
            }
            else
            {
                tempstrChar = strSql;
                string[] strBadChar = { "'", "%" };
                StringBuilder strBuilder = new StringBuilder(tempstrChar);
                for (int i = 0; i < strBadChar.Length; i++)
                {
                    newstrChar = strBuilder.Replace(strBadChar[i], "\\" + strBadChar[i]).ToString();
                }
            }
            return newstrChar;
        }

        #endregion
    }

}
