﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace Bryan.Common.Extension
{
    public static class ClassExtension
    {
        /// <summary>
        /// 获取对象属性描述信息 例如GetEnumDesc(Days.Sunday)
        /// </summary>
        /// <param name="en">枚举项 如Days.Sunday</param>
        /// <returns></returns>
        public static string GetDesc<T>(this T value)
        {
            Type t = value.GetType();
            MemberInfo[] array = t.GetMember(value.ToString());
            if (array != null && array.Length > 0)
            {
                MemberInfo ob = array[0];
                object[] desc = ob.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (desc != null && desc.Length > 0)
                    return ((DescriptionAttribute)desc[0]).Description;
            }
            return value.ToString();
        }

        /// <summary>
        /// 获取枚举项描述信息 例如GetEnumDesc(Days.Sunday)
        /// </summary>
        /// <param name="en">枚举项 如Days.Sunday</param>
        /// <returns></returns>
        public static string GetP<T>(this T value)
        {
            Type t = value.GetType();
            MemberInfo[] array = t.GetMember(value.ToString());
            if (array != null && array.Length > 0)
            {
                MemberInfo ob = array[0];
                object[] desc = ob.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (desc != null && desc.Length > 0)
                    return ((DescriptionAttribute)desc[0]).Description;
            }
            return value.ToString();
        }

    }
}
