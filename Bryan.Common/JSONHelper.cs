using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bryan.Common
{
    /// <summary>
    /// JSON帮助类
    /// </summary>
    public static class JSONHelper
    {
        #region 编码
        /// <summary>
        /// 编码
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">要转换的类型数据</param>
        /// <returns>json字符串</returns>
        public static string Encode<T>(T t)
        {
            return Encode(t, Formatting.None);
        }
        #endregion

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Dseriallize<T>(string json)
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string Seriallize(object o)
        {
            if (o != null)
            {
                return JsonConvert.SerializeObject(o);
            }
            return string.Empty;
        }

        public static List<T> ToList<T>(string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<List<T>>(Json);
        }

        public static DataTable ToTable(string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<DataTable>(Json);
        }

        public static JObject ToJObject(string jsonStr)
        {
            return (JObject)JsonConvert.DeserializeObject(jsonStr);
        }

        #region 编码
        /// <summary>
        /// 编码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Encode<T>(T t, Formatting format)
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            BigintConverter bigintConverter = new BigintConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式           
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";

            return JsonConvert.SerializeObject(t, format, timeConverter, bigintConverter);
        }
        #endregion

        #region 解码
        /// <summary>
        /// 解码
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>类型数据</returns>
        public static T Decode<T>(string json)
        {
            try
            {
                BigintConverter bigintConverter = new BigintConverter();

                return (T)JsonConvert.DeserializeObject(json, typeof(T), bigintConverter);
            }
            catch
            {
                return default(T);
            }
        }
        #endregion
    }

    #region Bigint转换成字符串
    /// <summary>
    /// Bigint类型转换处理
    /// </summary>
    public class BigintConverter : JsonConverter
    {
        /// <summary>
        /// 是否可转换
        /// </summary>
        /// <param name="objectType">对象类型</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            var ll = "".ToList();
            return objectType == typeof(System.Int64)
                || objectType == typeof(System.UInt64);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return 0;
            }
            else
            {
                IConvertible convertible = reader.Value as IConvertible;
                if (objectType == typeof(System.Int64))
                {
                    return convertible.ToInt64(CultureInfo.InvariantCulture);
                }
                else if (objectType == typeof(System.UInt64))
                {
                    return convertible.ToUInt64(CultureInfo.InvariantCulture);
                }
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteValue("0");
            }
            else if (value is Int64 || value is UInt64)
            {
                writer.WriteValue(value.ToString());
            }
            else
            {
                throw new Exception("Expected Bigint value");
            }
        }
    }

    /// <summary>
    /// 时间类型转为整型
    /// </summary>
    public class DateTimeIntConvert : IsoDateTimeConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return DateTime.MinValue;
            }
            else
            {
                IConvertible convertible = reader.Value as IConvertible;
                if (objectType == typeof(DateTime))
                {
                    return convertible.ToDateTime(new System.Globalization.DateTimeFormatInfo());
                    //return ((DateTime)existingValue).ToCommonTime();
                }
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteValue(string.Empty);
            }
            else if (value is DateTime)
            {
                writer.WriteValue(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
                //writer.WriteValue(((DateTime)value).ToCommonTime());
            }
            else
            {
                throw new Exception("Expected DateTime value");
            }
        }
    }

    #endregion
}
