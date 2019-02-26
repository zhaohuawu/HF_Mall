using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bryan.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BryanWu.Domain.Interface;
using Common.Interface;
using Common.Repository;
using Common;
using BryanWu.Domain.Model;
using Bryan.WebApi.Areas.Role.Models;

namespace Bryan.WebApi.Areas.Role.Controllers
{
    [Authorize]
    [Route("api/Sys/[controller]/[action]")]
    [ApiController]
    public class SysOptionController : BaseController
    {
        private ISys_OptionService _sysOptionService;
        public SysOptionController(ISys_OptionService sysOptionService, ILog_AdminService logAdmin, ILog log)
        {
            _logAdmin = logAdmin;
            _log = log;
            this._sysOptionService = sysOptionService;
        }

        /// <summary>
        /// 获取数据字典父列表
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetOptionList(int PageIndex, int PageSize, string GroupName)
        {
            string code = "000000";
            var pageSet = new PageSet(PageIndex, PageSize);
            var where = PredicateBuilder.True<Sys_Option>();
            if (!string.IsNullOrEmpty(GroupName))
                where = where.And(p => GroupName.Contains(p.GroupName));

            var list = _sysOptionService.GetPageList(where, pageSet, p => p.CrtDate, true, false);
            if (list.RecordCount == 0)
                code = "000200";
            return ReturnJson(code, list);
        }

        /// <summary>
        /// 获取数据字典子列表
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="GroupKey"></param>
        /// <param name="EnumName"></param>
        /// <param name="IsHide"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetOptionListByKey(int PageIndex, int PageSize, string GroupKey, string EnumName, int IsHide)
        {
            string code = "000000";
            var pageSet = new PageSet(PageIndex, PageSize);
            var where = PredicateBuilder.True<Sys_Option>();
            if (string.IsNullOrEmpty(GroupKey))
                return ReturnJson("000200");
            if (!string.IsNullOrEmpty(EnumName))
                where = where.And(p => EnumName.Contains(p.EnumName));
            if (IsHide > 0)
                where = where.And(p => p.IsHide == IsHide);

            var list = _sysOptionService.GetPageList(where, pageSet, p => p.Orders, true, false);
            if (list.RecordCount == 0)
                code = "000200";
            return ReturnJson(code, list);
        }

        /// <summary>
        /// 获取单个数据字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetOption(int id)
        {
            string code = "000000";
            var model = _sysOptionService.GetEntityById(id);
            if (model == null)
                code = "100020";

            return ReturnJson(code, model);
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddOption([FromBody]FromAddSysOption model)
        {
            string code = "000000";
            var sysOption = AutoMapperExt.MapTo<Sys_Option>(model);
            if (sysOption.Levels == 1)
            {
                sysOption.Orders = 0;
                sysOption.IsHide = 0;
            }

            int result = _sysOptionService.Insert(sysOption);

            return ReturnJson(code);
        }
    }
}