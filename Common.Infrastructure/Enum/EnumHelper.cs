using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Common.Infrastructure
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public abstract class EnumHelper
    {
        #region 公共属性方法

        /// <summary>
        /// 获取枚举值的名称
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举值的名称</returns>
        public static string GetNameOfEnum<T>(object value)
        {
            return Enum.GetName(typeof(T), value);
        }
        /// <summary>  
        /// 获取枚举变量值的 Description 属性  
        /// </summary>  
        /// <param name="obj">枚举变量</param>  
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns>  
        public static string GetDescriptionOfEnum(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            try
            {
                Type _enumType = obj.GetType();
                DescriptionAttribute dna = null;
                FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, obj));
                dna = (DescriptionAttribute)Attribute.GetCustomAttribute(
                   fi, typeof(DescriptionAttribute));
                if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                    return dna.Description;
            }
            catch
            {
            }
            return obj.ToString();
        }

        /// <summary>  
        /// 获取枚举变量值的 Description 属性  
        /// </summary>  
        /// <param name="obj">枚举变量</param>  
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns>  
        public static string GetDescriptionOfEnum<T>(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            try
            {
                Type _enumType = typeof(T);
                DescriptionAttribute dna = null;
                FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, obj));
                dna = (DescriptionAttribute)Attribute.GetCustomAttribute(
                   fi, typeof(DescriptionAttribute));
                if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                    return dna.Description;
            }
            catch
            {
            }
            return obj.ToString();
        }

        /// <summary>
        /// 获取枚举对象的枚举名称
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>枚举对象的枚举项</returns>
        public static Dictionary<int, string> GetNamesOfEnum<T>()
        {
            Dictionary<int, string> rets = new Dictionary<int, string>();
            foreach (object value in Enum.GetValues(typeof(T)))
            {
                rets.Add((int)value, GetNameOfEnum<T>(value));
            }
            return rets;
        }

        /// <summary>
        /// 获取枚举对象的枚举的描述
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns>枚举对象的枚举项</returns>
        public static Dictionary<int, string> GetDescriptionsOfEnum<T>()
        {
            Dictionary<int, string> rets = new Dictionary<int, string>();
            foreach (object value in Enum.GetValues(typeof(T)))
            {
                rets.Add((int)value, GetDescriptionOfEnum(value).ToString());
            }
            return rets;
        }
        
        #endregion
    }
}
