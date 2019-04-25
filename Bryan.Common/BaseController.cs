using Bryan.Common.Enums;
using Bryan.Common.Extension;
using Bryan.Common.Interface;
using Bryan.Common.Jwt;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bryan.Common
{
    /// <summary>
    /// 基类
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")] //设置跨域处理的 代理
    public class BaseController : Controller
    {
        protected ILogger _log;//操作日志

        #region ajax结果返回
        /// <summary>
        /// 返回json数据（int）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected IActionResult ReturnJson(string code, object obj = null)
        {
            string msg = GetMsgCode(code);
            if (string.IsNullOrEmpty(msg))
                msg = "未知类型";
            if (obj == null)
                return Json(new ReturnMsgCode(code, msg));
            else
                return Json(new ReturnMsgCode(code, msg, obj));
        }

        /// <summary>
        /// 返回json数据（int）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="obj"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected IActionResult ReturnJsonByParms(string code, object obj, params string[] param)
        {
            string msg = GetMsgCode(code);

            if (string.IsNullOrEmpty(msg))
            {
                msg = "未知类型";
            }
            if (obj == null)
                return Json(new ReturnMsgCode(code, string.Format(msg, param)));
            else
                return Json(new ReturnMsgCode(code, msg, obj));
        }

        #endregion

        protected JwtEntity GetJwtIEntity(string header = "")
        {
            JwtEntity jwtEntity = new JwtEntity();
            if (string.IsNullOrEmpty(header))
            {
                header = base.Request.Headers["Authorization"].FirstOrDefault();
                jwtEntity = JwtEntity.GetJwtIEntity(header);
            }
            return jwtEntity;
        }

        protected string GetMsgCode(string code)
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
