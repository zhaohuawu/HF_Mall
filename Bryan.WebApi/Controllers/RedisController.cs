using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bryan.WebApi.Models;
using Bryan.Common.Enums;
using Bryan.Common.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Bryan.Common;
using Bryan.Common.Extension;
using Microsoft.Extensions.Logging;

namespace Bryan.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RedisController : BaseController
    {
        public RedisController(ILogger<RedisController> log)
        {
            _log = log;
        }

        /// <summary>
        /// 覆盖返回码redis
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateMsgCodeToRedis()
        {
            RedisHelper.HDel(RedisKeysEnum.ReturnCodeHash.GetHFMallKey());
            var build = new ConfigurationBuilder().AddJsonFile("Config/msgCode.json");
            var _msgCode = build.Build().AsEnumerable();
            var code = await Task.Run(() =>
            {
                try
                {
                    Parallel.ForEach(_msgCode, n =>
                    {
                        RedisHelper.HSet(RedisKeysEnum.ReturnCodeHash.GetHFMallKey(), n.Key, n.Value);
                    });
                    return "000030";
                }
                catch (Exception ex)
                {
                    _log.LogError(ex.Message);
                    return "000031";
                }
            });
            return ReturnJson(code);
        }


    }
}