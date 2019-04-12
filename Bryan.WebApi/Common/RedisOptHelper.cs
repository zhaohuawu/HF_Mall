using Bryan.WebApi.Models;
using Bryan.Common.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bryan.Common.Extension;

namespace Bryan.WebApi.Common
{
    public class RedisOptHelper
    {
        public static string GetMsgCode(string code)
        {
            string msg = RedisHelper.HGet(RedisKeysEnum.ReturnCodeHash.GetHFMallKey(), code);

            if (string.IsNullOrEmpty(msg))
            {
                var build = new ConfigurationBuilder().AddJsonFile("Config/msgCode.json");
                var _msgCode = build.Build();
                msg = _msgCode[code.ToString()];
                if (!string.IsNullOrEmpty(msg))
                    RedisHelper.HSet(RedisKeysEnum.ReturnCodeHash.GetHFMallKey(), code, msg);
            }
            return msg;
        }
    }
}
