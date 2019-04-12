﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bryan.WebApi.Common;
using BryanWu.Domain.Interface;
using Bryan.Common;
using Bryan.Common.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Bryan.Common.Jwt;

namespace Bryan.WebApi.Controllers
{
    /// <summary>
    /// 基类
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")] //设置跨域处理的 代理
    public class BaseKKController : Controller
    {
        protected ILog_AdminService _logAdmin;//操作数据记录（数据库）
        protected ILog _log;//操作日志（log4Net）

        #region ajax结果返回
        /// <summary>
        /// 返回json数据（int）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected IActionResult ReturnJson(string code, object obj = null)
        {
            string msg = RedisOptHelper.GetMsgCode(code);
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
            string msg = RedisOptHelper.GetMsgCode(code);

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

    }
}