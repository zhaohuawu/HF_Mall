using Bryan.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.Common.Extension
{
    /// <summary>
    /// RedisKey扩展方法
    /// </summary>
    public static class RedisKeyExtension
    {
        public static string _defaultKey = string.Empty;

        /// <summary>
        /// HFMall的Redis关键字
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHFMallKey(this RedisKeysEnum key)
        {
            return _defaultKey + key.ToString();
        }
    }
}
