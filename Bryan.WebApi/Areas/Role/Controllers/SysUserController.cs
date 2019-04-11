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
using System.IO;
using Microsoft.Extensions.Options;
using Bryan.WebApi.Models.AppSettings;
using BryanWu.Domain;
using Bryan.WebApi.Common;
using Common.Enums;
using Bryan.WebApi.Models;
using Common.Extension;

namespace Bryan.WebApi.Areas.Role.Controllers
{
    /// <summary>
    /// 用户api
    /// </summary>
    [Authorize]
    [Route("api/Role/[controller]/[action]")]
    [ApiController]
    [PermissionFilter]
    public class SysUserController : BaseController
    {
        private ISys_UserService _sysUserService { get; set; }
        private ISys_UploadFileService _sysUploadService { get; set; }
        public SysUserController(ISys_UserService sysUserService, ISys_UploadFileService sysUploadService, ILog_AdminService logAdmin, ILog log)
        {
            _logAdmin = logAdmin;
            _log = log;
            this._sysUserService = sysUserService;
            this._sysUploadService = sysUploadService;
        }

        /// <summary>
        /// 查询账号（根据用户ID）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Permission("sysiuii")]
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            string code = "000000";
            var data = _sysUserService.GetUserById(id);
            if (data == null)
                code = "000200";
            else
                data.Password = null;

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
            PageSet pageSet = new PageSet(model.PageIndex, model.PageSize);
            var where = PredicateBuilder.True<Sys_User>();
            if (!string.IsNullOrEmpty(model.RealName))
                where = where.And(n => model.RealName.Contains(n.RealName));
            if (!string.IsNullOrEmpty(model.UserName))
                where = where.And(n => model.UserName.Contains(n.UserName));
            if (model.Status > 0)
                where = where.And(n => n.Status == model.Status);
            var pageList = _sysUserService.GetPageList(where, pageSet, p => new { p.Id, p.UserName, p.RealName, p.Sex, p.LastIp, p.Status, p.LastLogDate, p.HeadImgUrl, p.Mobile, p.Email }, p => p.LastLogDate, true);

            if (pageList.RecordCount == 0)
                code = "000200";

            return ReturnJson(code, pageList);
        }


        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddSysUser([FromBody]FromAddSysUser model)
        {
            string code = "000000";
            var user = AutoMapperExt.MapTo<Sys_User>(model);
            user.Password = AESUtil.EncryptPsw(user.Password);

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

                if (user.Id > 0 && !string.IsNullOrEmpty(user.HeadImgUrl))
                {
                    //更改图片状态
                    _sysUploadService.UpdateUploadStatusAsync(UploadTypeEnum.image, user.HeadImgUrl, UploadStatusEnum.使用中);
                }

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
                if (role.Status == (int)SysUserStatusEnum.Normal)
                {
                    role.Status = (int)SysUserStatusEnum.Logout;
                }
                else
                    role.Status = (int)SysUserStatusEnum.Normal;
                _sysUserService.UpdateColumns(p => new { p.Status }, role, true);
            }
            else
                code = "000104";
            return ReturnJson(code);
        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUserRolesList(int userId)
        {
            string code = "000000";
            var list = _sysUserService.GetList(p => p.UserId == userId, p => new { p.Id, p.RoleId }, p => p.Id);
            var roleIdList = new List<int>();
            if (list.Count == 0)
                code = "000200";
            else
                roleIdList = list.Select(p => p.RoleId).ToList();
            return ReturnJson(code, new { list = list, roleList = roleIdList });
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
            var userEntity = _sysUserService.GetUserById(model.UserId);
            if (userEntity != null)
            {
                try
                {
                    var now = DateTime.Now;
                    //异步更新用户角色相关
                    Task.Run(() =>
                    {
                        var user = AutoMapperExt.MapTo<Sys_User>(model);
                        user.Id = model.UserId;
                        user.ModifyDate = now;
                        if (string.IsNullOrEmpty(model.Password))
                        {
                            _sysUserService.UpdateColumns(p => new { p.UserName, p.RealName, p.Email, p.HeadImgUrl, p.Mobile, p.Sex, p.ModifyDate }, user, true);
                        }
                        else
                        {
                            user.Password = AESUtil.EncryptPsw(model.Password);
                            _sysUserService.UpdateColumns(p => new { p.UserName, p.RealName, p.Password, p.Email, p.HeadImgUrl, p.Mobile, p.Sex, p.ModifyDate }, user, true);
                        }

                    });
                    //异步更新图片状态
                    if (userEntity.HeadImgUrl != model.HeadImgUrl)
                    {
                        _sysUploadService.UpdateUploadStatusAsync(UploadTypeEnum.image, model.HeadImgUrl, UploadStatusEnum.使用中);
                        if (!string.IsNullOrEmpty(userEntity.HeadImgUrl))
                            _sysUploadService.UpdateUploadStatusAsync(UploadTypeEnum.image, userEntity.HeadImgUrl, UploadStatusEnum.可删除);
                    }
                    //异步添加用户角色权限
                    if (model.RoleList.Where(p => p.Status == (int)RoleMenuStatus.add).Count() > 0 && model.UserId > 0)
                    {
                        Task.Run(() =>
                        {
                            var perList = new List<Sys_UserRole>();
                            var menuList = model.RoleList.Where(p => p.Status == (int)RoleMenuStatus.add);

                            foreach (var item in menuList)
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
                    if (model.RoleList.Where(p => p.Status == (int)RoleMenuStatus.delete).Count() > 0 && model.UserId > 0)
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

        /// <summary>
        /// 添加或修改账号角色列表Redis
        /// </summary>
        /// <param name="isAdd">1:增加，0：停用</param>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateUserRoleToRedis([FromForm]int isAdd, [FromForm]int userId, [FromForm]int roleId)
        {
            var result = await Task.Run(() =>
            {
                var list = new List<Sys_UserRole>();
                var dicList = new Dictionary<int, List<int>>();
                if (userId > 0 && isAdd == 1)
                {
                    //给账号修改角色
                    if (RedisHelper.HExists(RedisKeysEnum.AdminRoleHash.GetHFMallKey(), userId.ToString()))
                        RedisHelper.HDel(RedisKeysEnum.AdminRoleHash.GetHFMallKey(), userId.ToString());
                    list = _sysUserService.GetUserRoleList(userId, 0);
                    var arr = list.Select(n => n.RoleId).Distinct().ToList();
                    RedisHelper.HSet(RedisKeysEnum.AdminRoleHash.GetHFMallKey(), userId.ToString(), arr);
                    dicList.Add(userId, arr);
                }
                else if (userId > 0 && isAdd == 0)
                {
                    //停用账号
                    if (RedisHelper.HExists(RedisKeysEnum.AdminRoleHash.GetHFMallKey(), userId.ToString()))
                        RedisHelper.HDel(RedisKeysEnum.AdminRoleHash.GetHFMallKey(), userId.ToString());
                }
                else if (roleId > 0)
                {
                    var hDic = RedisHelper.HGetAll<List<int>>(RedisKeysEnum.AdminRoleHash.GetHFMallKey());
                    Parallel.ForEach(hDic, item =>
                    {
                        var arr = item.Value;
                        if (arr.Contains(roleId))
                        {
                            if (RedisHelper.HExists(RedisKeysEnum.AdminRoleHash.GetHFMallKey(), item.Key))
                                RedisHelper.HDel(RedisKeysEnum.AdminRoleHash.GetHFMallKey(), item.Key);
                            arr.Remove(roleId);
                            RedisHelper.HSet(RedisKeysEnum.AdminRoleHash.GetHFMallKey(), item.Key, arr);
                            dicList.Add(Convert.ToInt32(item.Key), arr);
                        }
                    });
                }
                else
                {
                    //全部账号角色数据覆盖
                    list = _sysUserService.GetUserRoleList(0, 0);
                    var userList = list.Select(p => p.UserId).Distinct().ToList();
                    RedisHelper.HDel(RedisKeysEnum.AdminRoleHash.GetHFMallKey());
                    Parallel.ForEach(userList, item =>
                    {
                        var arr = list.Where(p => p.UserId == item).Select(n => n.RoleId).Distinct().ToList();
                        RedisHelper.HSet(RedisKeysEnum.AdminRoleHash.GetHFMallKey(), item.ToString(), arr);
                        dicList.Add(item, arr);
                    });
                }

                return ("000030", dicList).ToTuple();
            });
            return ReturnJson(result.Item1, result.Item2);
        }
    }
}