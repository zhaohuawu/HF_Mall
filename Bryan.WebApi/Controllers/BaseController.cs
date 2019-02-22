using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BryanWu.Domain.Interface;
using BryanWu.Domain.Model;
using Common;
using Common.Interface;
using Common.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Bryan.WebApi.Controllers
{
    /// <summary>
    /// 基类
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")] //设置跨域处理的 代理
    public class BaseController : ControllerBase
    {
        protected ILog_AdminService _logAdmin;//操作数据记录（数据库）
        protected ILog _log;//操作日志（log4Net）
        public static string _userName = string.Empty;//用户名
        public static int _userId = 0;//用户ID
        public static int _role = 0;//用户角色ID
        private static IConfigurationRoot _msgCode = null;//保存msgcode.json数据
      

        #region ajax结果返回
        /// <summary>
        /// 返回json数据（enum）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("")]
        protected IActionResult ToJson(ReturnResultEnum code, string msg = "")
        {
            return Ok(new ReturnResult(code, msg));
        }
        /// <summary>
        /// 返回json数据（enum）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Route("")]
        protected IActionResult ToJson(ReturnResultEnum code, object obj = null)
        {
            return Ok(new ReturnResult(code, obj));
        }
        /// <summary>
        /// 返回json数据（enum）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Route("")]
        protected IActionResult ToJson(ReturnResultEnum code, string msg, object obj = null)
        {
            ReturnResult result;
            if (obj == null)
                result = new ReturnResult(code, msg);
            else
                result = new ReturnResult(code, msg, obj);
            return Ok(result);
        }
        /// <summary>
        /// 返回json数据（int）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Route("")]
        protected IActionResult ReturnJson(string code, object obj = null)
        {
            //TODO msg根据code获取
            var build = new ConfigurationBuilder().AddJsonFile("Config/msgCode.json");
            if (_msgCode == null)
                _msgCode = build.Build();
            string msg = _msgCode[code.ToString()];
            if (msg == null)
                msg = "未知类型";
            if (obj == null)
                return Ok(new ReturnMsgCode(code, msg));
            else
                return Ok(new ReturnMsgCode(code, msg, obj));
        }
        /// <summary>
        /// 返回json数据（int）
        /// </summary>
        /// <param name="code"></param>
        /// <param name="obj"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [Route("")]
        protected IActionResult ReturnJsonByParms(string code, object obj, params string[] param)
        {
            //TODO msg根据code获取
            var build = new ConfigurationBuilder().AddJsonFile("Config/msgCode.json");
            if (_msgCode == null)
                _msgCode = build.Build();
            string msg = _msgCode[code.ToString()];
            if (msg == null)
            {
                msg = "未知类型";
            }
            if (obj == null)
                return Ok(new ReturnMsgCode(code, string.Format(msg, param)));
            else
                return Ok(new ReturnMsgCode(code, msg, obj));
        }

        #endregion


    }
}