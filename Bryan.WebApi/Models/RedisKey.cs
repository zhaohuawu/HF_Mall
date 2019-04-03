using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Models
{
    /// <summary>
    /// RedisKey扩展方法
    /// </summary>
    public static class RedisKeyExtension
    {
        /// <summary>
        /// HFMall的Redis关键字
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHFMallKey(this RedisKeysEnum key)
        {
            return "hfmall:" + key.ToString();
        }
    }
}
