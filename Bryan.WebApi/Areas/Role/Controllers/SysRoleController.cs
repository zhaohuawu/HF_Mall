using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BryanWu.Domain.Interface;
using BryanWu.Domain.Model;
using Bryan.WebApi.Areas.Role.Models;
using Bryan.WebApi.Controllers;
using Common;
using Common.Interface;
using Common.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Common.Autofac;

namespace Bryan.WebApi.Areas.Role.Controllers
{
    [Authorize]
    [Route("api/Role/[controller]/[action]")]
    [ApiController]
    public class SysRoleController : BaseController
    {
        private ISys_AdminRoleService _sysAdminRoleService;
        private ISys_AdminMenuService _sysAdminMenuService;
        private ISys_AdminPermissionService _sysAdminPermissionService;
        private ISys_AdminMenuBtnService _sysAdminMenuBtnService;
        public SysRoleController(ISys_AdminRoleService adminRole, ISys_AdminMenuService sysAdminMenuService, ISys_AdminPermissionService adminPermissionService, ISys_AdminMenuBtnService sysAdminMenuBtnService, ILog_AdminService logAdmin, ILog log)
        {
            _logAdmin = logAdmin;
            _log = log;
            this._sysAdminRoleService = adminRole;
            this._sysAdminMenuService = sysAdminMenuService;
            this._sysAdminPermissionService = adminPermissionService;
            this._sysAdminMenuBtnService = sysAdminMenuBtnService;
        }

        #region 角色
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="page">分页第几页</param>
        /// <param name="pageSize">每页的数量</param>
        /// <param name="roleName">角色名称（可以为空，但是必须传该参数到后台）</param>
        /// <param name="isForbidden">是否禁用，-1：全部，0：正常，1：禁用</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAdminRolesList(int page, int pageSize, int isForbidden, string roleName)
        {
            string code = "000000";
            var pageSet = new PageSet();
            pageSet.PageIndex = page > 0 ? page : 1;
            pageSet.PageSize = pageSize > 0 ? pageSize : 30;
            var where = PredicateBuilder.True<Sys_AdminRole>();
            if (!string.IsNullOrEmpty(roleName))
                where = where.And(p => p.RoleName.Contains(roleName));
            if (isForbidden > 0)
                where = where.And(p => p.IsForbidden == isForbidden);
            var list = _sysAdminRoleService.GetPageList(where, pageSet, p => p.CrtDate, true, true);
            if (list.RecordCount > 0)
                code = "000200";
            return ReturnJson(code, list);
        }

        /// <summary>
        /// 添加角色并且配置权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRole([FromBody]FromAddRole model)
        {
            string code = "000000";
            var role = AutoMapperExt.MapTo<Sys_AdminRole>(model);
            if (!_sysAdminRoleService.IsAny(p => p.RoleName == role.RoleName))
            {
                role.CrtDate = DateTime.Now;
                role.UserId = _userId;
                role.Id = _sysAdminRoleService.Insert(role, true);

                if (role.Id > 0 && model.MenuList.Count > 0)
                {
                    //异步执行角色权限配置
                    Task.Run(() =>
                    {
                        var perList = new List<Sys_AdminPermission>();
                        foreach (var item in model.MenuList)
                        {
                            var perModel = new Sys_AdminPermission();
                            perModel.BtnJson = item.BtnJson;
                            perModel.CrtDate = role.CrtDate;
                            perModel.MenuId = item.MenuId;
                            perModel.RoleId = role.Id;
                            perModel.UserId = _userId;
                            perList.Add(perModel);
                        }
                        _sysAdminPermissionService.InsertList(perList);
                    });
                }
                else if (role.Id == 0)
                    code = "000001";
            }
            else
            {
                code = "100006";
            }

            return ReturnJson(code);
        }

        /// <summary>
        /// 禁用角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ForbiddenRole(int roleId)
        {
            string code = "000000";
            var role = _sysAdminRoleService.GetEntityById(roleId);
            if (role != null)
            {
                if (role.IsForbidden == 0)
                {
                    role.IsForbidden = 1;
                }
                else
                    role.IsForbidden = 0;
                _sysAdminRoleService.UpdateColumns(p => new { p.IsForbidden }, role, true);
            }
            else
                code = "100007";
            return ReturnJson(code);
        }

        /// <summary>
        /// 更新角色和菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateRole([FromBody]FromUpdateRole model)
        {
            string code = "000000";
            if (_sysAdminRoleService.IsAny(p => p.Id == model.RoleId))
            {
                try
                {
                    var now = DateTime.Now;
                    //异步更新角色相关
                    Task.Run(() =>
                    {
                        var role = new Sys_AdminRole();
                        role.Id = model.RoleId;
                        role.RoleName = model.RoleName;
                        role.Remark = model.Remark;
                        _sysAdminRoleService.UpdateColumns(p => new { p.RoleName, p.Remark }, role, true);
                    });
                    //异步更新角色权限
                    //if (model.MenuList.Where(p => p.Status == (int)RoleMenuStatus.update).Count() > 0)
                    //{
                    //    Task.Run(() =>
                    //    {
                    //        var perList = new List<Sys_AdminPermission>();
                    //        var menuList = model.MenuList.Where(p => p.Status == (int)RoleMenuStatus.update);

                    //        foreach (var item in menuList)
                    //        {
                    //            var perModel = new Sys_AdminPermission();
                    //            perModel.Id = item.PerId;
                    //            perModel.MenuId = item.MenuId;
                    //            perModel.BtnJson = item.BtnJson;
                    //            perModel.CrtDate = now;
                    //            perModel.RoleId = model.RoleId;
                    //            perModel.UserId = _userId;
                    //            perList.Add(perModel);
                    //        }
                    //        _sysAdminPermissionService.UpdateList(perList);
                    //    });
                    //}
                    //异步添加角色权限
                    if (model.MenuList.Where(p => p.Status == (int)RoleMenuStatus.add).Count() > 0)
                    {
                        Task.Run(() =>
                        {
                            var perList = new List<Sys_AdminPermission>();
                            var menuList = model.MenuList.Where(p => p.Status == (int)RoleMenuStatus.add);

                            foreach (var item in menuList)
                            {
                                var perModel = new Sys_AdminPermission();
                                //perModel.Id = item.PerId;
                                perModel.MenuId = item.MenuId;
                                perModel.BtnJson = item.BtnJson;
                                perModel.CrtDate = now;
                                perModel.RoleId = model.RoleId;
                                perModel.UserId = _userId;
                                perList.Add(perModel);
                            }
                            _sysAdminPermissionService.InsertList(perList);
                        });
                    }
                    //异步删除角色权限
                    if (model.MenuList.Where(p => p.Status == (int)RoleMenuStatus.delete).Count() > 0)
                    {
                        Task.Run(() =>
                        {
                            var perList = model.MenuList.Where(p => p.Status == (int)RoleMenuStatus.add).Select(p => p.PerId).ToArray();
                            _sysAdminPermissionService.DeleteByIdArray(perList);
                        });
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex.ToString());
                    code = "000100";
                }
            }
            else
                code = "100007";
            return ReturnJson(code);
        }

        #endregion

        #region 菜单
        /// <summary>
        /// 获取全部菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMenuList()
        {
            string code = "000000";
            var list = _sysAdminMenuService.GetList(p => true, p => p.Pid);
            var returnList = new List<ReturnMenuTree>();
            if (list.Count == 0)
                code = "000001";
            else
            {
                var lel1List = list.Where(p => p.Level == 1).ToList();
                var menu = new ReturnMenuTree();
                foreach (var item1 in lel1List)
                {
                    var returnMenuTree = new ReturnMenuTree(item1.Id, item1.MenuName, new List<ReturnMenuTree>());
                    Common.Util.MenuTree(returnMenuTree, list, item1.Id);
                    returnList.Add(returnMenuTree);
                }

            }
            return ReturnJson(code, returnList);
        }

        /// <summary>
        /// 菜单id获取单个菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
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
        [HttpPost]
        public IActionResult AddMenu([FromBody]FromAddMenu model)
        {
            var sysAdminMenu = AutoMapperExt.MapTo<Sys_AdminMenu>(model);
            string code = "000000";
            Log_Admin logAdmin = new Log_Admin();
            if (sysAdminMenu.Id == 0)
            {
                //新增
                var maxMenu = _sysAdminMenuService.GetEntity(p => p.Pid == sysAdminMenu.Pid, p => p.Id, true);
                if (maxMenu == null)
                {
                    var parentMenu = _sysAdminMenuService.GetEntityById(sysAdminMenu.Pid);
                    if (parentMenu != null)
                    {
                        sysAdminMenu.Orders = 1;
                        sysAdminMenu.Level = parentMenu.Level + 1;
                    }
                    else
                        return ReturnJson("100004");
                }
                else
                {
                    sysAdminMenu.Orders = maxMenu.Orders + 1;
                    sysAdminMenu.Level = maxMenu.Level;
                }
                sysAdminMenu.CrtUser = _userName;
                sysAdminMenu.CrtDate = DateTime.Now;
                logAdmin.OtherId = _sysAdminMenuService.Insert(sysAdminMenu).ToString();
            }
            else
            {
                var adminMenu = _sysAdminMenuService.GetEntityById(sysAdminMenu.Id);
                if (adminMenu != null)
                {
                    if (adminMenu.Orders != sysAdminMenu.Orders)
                    {
                        //修改排序
                        bool reuslt = _sysAdminMenuService.UpdateMenuOrders(sysAdminMenu.Orders, adminMenu);
                        if (!reuslt)
                            code = "000001";
                    }
                    //修改其他字段
                    if (!_sysAdminMenuService.UpdateColumns(p => new { p.Tag, p.Icon, p.IsShow, p.MenuName, p.Url }, sysAdminMenu))
                        return ReturnJson("000001");

                    logAdmin.OtherId = sysAdminMenu.Id.ToString();
                }
                else
                    code = "100005";


            }

            logAdmin.CrtUserId = _userId;
            logAdmin.CrtUserName = _userName;
            _logAdmin.LogAdmin(logAdmin, HttpContext);
            return ReturnJson(code);
        }

        /// <summary>
        /// 改变菜单排序
        /// </summary>
        /// <param name="mid">菜单ID</param>
        /// <param name="orders">将移动到的排序位置</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateMenuOrders([FromForm]int mid, [FromForm]int orders)
        {
            string code = "000000";
            if (mid > 0)
            {
                var adminMenu = _sysAdminMenuService.GetEntityById(mid);
                if (adminMenu != null)
                {
                    bool reuslt = _sysAdminMenuService.UpdateMenuOrders(orders, adminMenu);
                    if (!reuslt)
                        code = "0000001";
                }
                else
                    code = "100005";
            }
            else
                code = "100005";
            return ReturnJson(code);
        }

        /// <summary>
        /// 获取菜单中的按钮或资源
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMenuBtnList(int menuId)
        {
            string code = "000000";
            var list = _sysAdminMenuBtnService.GetList(p => p.MenuId == menuId, p => p.Id);
            if (list.Count == 0)
                return ReturnJson("000001");

            return ReturnJson(code, list);
        }

        /// <summary>
        /// 通过ID获取菜单中的单个资源或按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMenuBtnById(int id)
        {
            string code = "000000";
            var menuBtn = _sysAdminMenuBtnService.GetEntityById(id);
            if (menuBtn == null)
                return ReturnJson("100009");

            return ReturnJson(code, menuBtn);
        }

        /// <summary>
        /// 添加菜单按钮或资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddMenuBtn([FromBody]FromAddMenuBtn model)
        {
            string code = "000000";
            var menuBtn = AutoMapperExt.MapTo<Sys_AdminMenuBtn>(model);

            if (menuBtn.Id == 0)
            {
                if (_sysAdminMenuBtnService.Insert(menuBtn) < 1)
                    return ReturnJson("000011");
            }
            else
            {
                //修改
                if (!_sysAdminMenuBtnService.UpdateIgnoreColumns(p => new { p.MenuId }, menuBtn, true))
                    return ReturnJson("000013");
            }
            return ReturnJson(code);
        }

        /// <summary>
        /// 根据Id删除菜单按钮或资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteMenuBtn(int id)
        {
            string code = "000000";
            if (!_sysAdminMenuBtnService.DeleteById(id))
            {
                code = "000014";
                if (!_sysAdminMenuBtnService.IsAny(p => p.Id == id))
                    code = "100009";
            }

            return ReturnJson(code);
        }
        #endregion

    }
}