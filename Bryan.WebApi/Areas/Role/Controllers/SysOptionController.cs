using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bryan.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BryanWu.Domain.Interface;
using Bryan.Common.Interface;
using Bryan.Common.Repository;
using Bryan.Common;
using BryanWu.Domain.Model;
using Bryan.WebApi.Areas.Role.Models.SysOption;

namespace Bryan.WebApi.Areas.Role.Controllers
{
    [Authorize]
    [Route("api/Sys/[controller]/[action]")]
    [ApiController]
    public class SysOptionController : BaseController
    {
        private ISys_OptionService _sysOptionService;
        private ILog_AdminService _logAdmin;
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
            sysOption.CrtDate = DateTime.Now;

            if (sysOption.Levels == 1)
            {
                if (_sysOptionService.IsAny(p => p.GroupKey == sysOption.GroupKey))
                    return ReturnJson("100021");
                if (_sysOptionService.Insert(sysOption) <= 0)
                    return ReturnJson("000011");
            }
            else
            {
                if (_sysOptionService.IsAny(p => p.GroupKey == sysOption.GroupKey && p.EnumCode == sysOption.EnumCode))
                    return ReturnJson("100021");
                if (!_sysOptionService.AddOption(sysOption))
                    return ReturnJson("000011");
            }

            return ReturnJson(code);
        }

        /// <summary>
        /// 修改字典名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateOptionName([FromBody]FromUpdateSysOption model)
        {
            string code = "000000";
            var sysOption = AutoMapperExt.MapTo<Sys_Option>(model);
            var optionEntity = _sysOptionService.GetEntityById(sysOption.Id);
            if (optionEntity != null)
            {
                //修改其他字段
                if (optionEntity.GroupKey != sysOption.GroupKey)
                {
                    if (_sysOptionService.IsAny(p => p.GroupKey == sysOption.GroupKey && p.Levels > 1))
                        return ReturnJson("100021");
                }
                if (!_sysOptionService.UpdateColumns(p => new { p.GroupKey, p.GroupName, p.IsHide, p.Remark }, sysOption))
                    return ReturnJson("000001");
            }
            else
                code = "100020";
            return ReturnJson(code);
        }

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateOption([FromBody]FromAddSysOption model)
        {
            string code = "000000";
            var sysOption = AutoMapperExt.MapTo<Sys_Option>(model);
            var optionEntity = _sysOptionService.GetEntityById(sysOption.Id);
            if (optionEntity != null)
            {
                if (_sysOptionService.IsAny(p => p.GroupKey == sysOption.GroupKey && p.EnumCode == sysOption.EnumCode))
                    return ReturnJson("100021");
                if (optionEntity.Orders != optionEntity.Orders)
                {
                    //修改排序
                    Task.Run(() =>
                    {
                        _sysOptionService.UpdateOptionOrders(optionEntity.Orders, sysOption);
                    });
                }
                //修改其他字段
                if (!_sysOptionService.UpdateColumns(p => new { p.EnumCode, p.EnumLabel, p.EnumName, p.IsHide, p.Remark }, sysOption))
                    return ReturnJson("000001");
            }
            else
                code = "100020";
            return ReturnJson(code);
        }

        /// <summary>
        /// 改变字典排序
        /// </summary>
        /// <param name="mid">字典ID</param>
        /// <param name="orders">将移动到的排序位置</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateOptionOrders([FromForm]int mid, [FromForm]int orders)
        {
            string code = "000000";
            if (mid > 0)
            {
                var adminMenu = _sysOptionService.GetEntityById(mid);
                if (adminMenu != null)
                {
                    bool reuslt = _sysOptionService.UpdateOptionOrders(orders, adminMenu);
                    if (!reuslt)
                        code = "0000001";
                }
                else
                    code = "100020";
            }
            else
                code = "100020";
            return ReturnJson(code);
        }
    }
}