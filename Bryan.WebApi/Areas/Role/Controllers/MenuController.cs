using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BryanWu.Domain.Interface;
using BryanWu.Domain.Model;
using Bryan.WebApi.Controllers;
using Bryan.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bryan.WebApi.Areas.Role.Models.SysRole;

namespace Bryan.WebApi.Areas.Role.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : BaseController
    {
        private ISys_AdminMenuService _sysAdminMenuService;
        public MenuController(ISys_AdminMenuService sysAdminMenuService)
        {
            this._sysAdminMenuService = sysAdminMenuService;
        }

        /// <summary>
        /// 获取全部菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("getmenulist")]
        public IActionResult GetMenuList()
        {
            string code = "000000";
            var list = _sysAdminMenuService.GetList(p => true, p => p.Pid);
            if (list.Count == 0)
                code = "000001";
            return ReturnJson(code, list);
        }

        /// <summary>
        /// 菜单id获取单个菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getmenu")]
        public IActionResult GetMenu(int id)
        {
            string code = "000000";
            var entity = _sysAdminMenuService.GetEntityById(id);
            if (entity == null)
                code = "000001";
            return ReturnJson(code, entity);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("addmemu")]
        public IActionResult AddMenu([FromBody]FromAddMenu model)
        {
            var sysAdminMenu = AutoMapperExt.MapTo<Sys_AdminMenu>(model);
            string code = "000000";

            if (sysAdminMenu.Id == 0)
            {
                //新增
                var maxMenu = _sysAdminMenuService.GetEntity(p => p.Pid == sysAdminMenu.Pid, p => p.Id, true);
                if (maxMenu == null)
                {
                    if (_sysAdminMenuService.IsAny(p => p.Id == sysAdminMenu.Pid))
                        sysAdminMenu.Orders = 1;
                    else
                        return ReturnJson("100004");
                }
                else
                {
                    sysAdminMenu.Orders = maxMenu.Orders + 1;
                }
                sysAdminMenu.CrtUser = GetJwtIEntity().Name;
                sysAdminMenu.CrtDate = DateTime.Now;
                _sysAdminMenuService.Insert(sysAdminMenu);
            }
            else
            {
                //修改
                if (!_sysAdminMenuService.UpdateColumns(p => new { p.BtnJson, p.Icon, p.IsShow, p.MenuName, p.Url }, sysAdminMenu))
                    return ReturnJson("000001");
            }

            return ReturnJson(code);
        }

        /// <summary>
        /// 改变菜单排序
        /// </summary>
        /// <param name="mid">菜单ID</param>
        /// <param name="orders">将移动到的排序位置</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("updatemenuorders")]
        public IActionResult UpdateMenuOrders(int mid, int orders)
        {
            string code = "000000";
            var adminMenu = _sysAdminMenuService.GetEntityById(mid);
            if (adminMenu != null)
            {
                bool reuslt = _sysAdminMenuService.UpdateMenuOrders(orders, adminMenu);
                if (!reuslt)
                    code = "0000001";
            }
            else
                code = "100005";
            return ReturnJson(code);
        }
    }
}