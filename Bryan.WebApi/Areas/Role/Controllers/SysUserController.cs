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
using Common.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Repository;
using Bryan.WebApi.Areas.Role.Models.SysUser;

namespace Bryan.WebApi.Areas.Role.Controllers
{
    /// <summary>
    /// 用户api
    /// </summary>
    [Authorize]
    [Route("api/Role/[controller]/[action]")]
    [ApiController]
    public class SysUserController : BaseController
    {
        private ISys_UserService _sysUserService { get; set; }

        public SysUserController(ISys_UserService sysUserService, ILog_AdminService logAdmin, ILog log)
        {
            _logAdmin = logAdmin;
            _log = log;
            this._sysUserService = sysUserService;
        }

        /// <summary>
        /// 查询账号（根据用户ID）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            string code = "000000";
            var data = _sysUserService.GetUserById(id);
            if (data == null)
                code = "000200";
            return ReturnJson(code, data);
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllUser()
        {
            string code = "000000";
            var list = _sysUserService.GetAllUser();
            if (list.Count == 0)
                code = "000200";
            return ReturnJson(code, list);
        }

        /// <summary>
        /// 分页获取账号列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPageList([FromQuery]FromGetSysUser model)
        {
            string code = "000000";
            PageSet pageSet = new PageSet();
            pageSet.PageIndex = model.PageIndex;
            pageSet.PageSize = model.PageSize;
            var where = PredicateBuilder.True<Sys_User>();
            if (!string.IsNullOrEmpty(model.RealName))
                where = where.And(n => model.RealName.Contains(n.RealName));
            if (!string.IsNullOrEmpty(model.UserName))
                where = where.And(n => model.UserName.Contains(n.UserName));
            if (model.Status > 0)
                where = where.And(n => n.Status == model.Status);

            var list = _sysUserService.GetPageList(where, pageSet, p => p.Status);
            if (list.RecordCount == 0)
                code = "000200";
            return ReturnJson(code, list);
        }


        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddSysUser([FromBody]FromAddSysUser model)
        {
            string code = "000000";
            var user = AutoMapperExt.MapTo<Sys_User>(model);
            user.Password = AESUtil.EncryptPsw(user.Password);
            var tt = await Task.Run(() =>
             {
                 Log_Admin logAdmin = new Log_Admin();

                 var now = DateTime.Now;
                 if (_sysUserService.IsAny(p => p.UserName == user.UserName))
                 {
                     return ReturnJson("100001");
                 }
                 else
                 {
                     user.LastIp = HttpContextExtension.GetIp(HttpContext);
                     user.LastLogDate = now;
                     user.CrtUser = _userName;
                     user.CrtDate = now;
                     user.Id = _sysUserService.Insert(user, false);

                     if (user.Id > 0 && model.RoleList.Count > 0)
                     {
                         //异步执行角色权限配置
                         Task.Run(() =>
                         {
                             var perList = new List<Sys_UserRole>();
                             foreach (var item in model.RoleList)
                             {
                                 var perModel = new Sys_UserRole();
                                 perModel.RoleId = item.RoleId;
                                 perModel.UserId = user.Id;
                                 perList.Add(perModel);
                             }
                             _sysUserService.InsertList(perList);
                         });
                     }
                     else if (user.Id == 0)
                         code = "000001";

                     logAdmin.Remark = "账号：" + user.UserName + "注册成功";

                     logAdmin.TypeId = (int)EnumLogAdminType.add_sysUser;//添加用户时的类型Id
                 }

                 logAdmin.OtherId = user.Id.ToString();
                 logAdmin.CrtUserId = _userId;
                 logAdmin.CrtUserName = _userName;
                 _logAdmin.LogAdmin(logAdmin, HttpContext);
                 return ReturnJson(code);
             });
            return tt;
        }

        /// <summary>
        /// 修改账号状态
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult FrobiddenSysUser(int userId)
        {
            string code = "000000";
            var role = _sysUserService.GetUserById(userId);
            if (role != null)
            {
                if (role.Status == 1)
                {
                    role.Status = 5;
                }
                else
                    role.Status = 1;
                _sysUserService.UpdateColumns(p => new { p.Status }, role, true);
            }
            else
                code = "000104";
            return ReturnJson(code);
        }

        /// <summary>
        /// 更新角色和账号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateUserRole([FromBody]FromUpdateSysUser model)
        {
            string code = "000000";
            if (_sysUserService.IsAny(p => p.Id == model.UserId))
            {
                try
                {
                    var now = DateTime.Now;
                    //异步更新用户角色相关
                    Task.Run(() =>
                    {
                        var user = new Sys_User();
                        user.Id = model.UserId;
                        user.UserName = model.UserName;
                        user.RealName = model.RealName;
                        user.Password = AESUtil.EncryptPsw(model.Password);
                        _sysUserService.UpdateColumns(p => new { p.UserName, p.RealName, p.Password }, user, true);
                    });
                    //异步添加用户角色权限
                    if (model.RoleList.Where(p => p.Status == (int)RoleMenuStatus.add).Count() > 0)
                    {
                        Task.Run(() =>
                        {
                            var perList = new List<Sys_UserRole>();
                            var menuList = model.RoleList.Where(p => p.Status == (int)RoleMenuStatus.add);

                            foreach (var item in model.RoleList)
                            {
                                var perModel = new Sys_UserRole();
                                perModel.RoleId = item.RoleId;
                                perModel.UserId = model.UserId;
                                perList.Add(perModel);
                            }
                            _sysUserService.InsertList(perList);
                        });
                    }
                    //异步删除用户角色权限
                    if (model.RoleList.Where(p => p.Status == (int)RoleMenuStatus.delete).Count() > 0)
                    {
                        Task.Run(() =>
                        {
                            var perList = model.RoleList.Where(p => p.Status == (int)RoleMenuStatus.delete).Select(p => p.UserRoleId).ToArray();
                            _sysUserService.DeleteByIdArray(perList);
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

    }
}