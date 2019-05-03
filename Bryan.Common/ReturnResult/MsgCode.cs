using Bryan.Common.Enums;
using Bryan.Common.Extension;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Bryan.Common.ReturnResult
{
    public class MsgCode
    {
        public static string GetMsgCode(string code)
        {
            string msg = RedisHelper.HGet(RedisKeysEnum.ReturnCodeHash.GetHFMallKey(), code);

            if (string.IsNullOrEmpty(msg))
            {
                var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Config/msgCode.json");
                var _msgCode = build.Build();
                msg = _msgCode[code.ToString()];
                if (!string.IsNullOrEmpty(msg))
                    RedisHelper.HSet(RedisKeysEnum.ReturnCodeHash.GetHFMallKey(), code, msg);
            }
            return msg;
        }
    }
}
