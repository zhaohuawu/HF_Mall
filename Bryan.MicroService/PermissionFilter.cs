using Bryan.Common;
using Bryan.Common.Enums;
using Bryan.MicroService.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bryan.MicroService
{
    public class PermissionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //获取属性
            PermissionAttribute actionAttr = filterContext.ActionDescriptor.FilterDescriptors
                .Where(a => a.Filter is PermissionAttribute)
                .Select(a => a.Filter).FirstOrDefault() as PermissionAttribute;
            string strNavName = string.Empty;
            string strActionType = string.Empty;
            try
            {
                if (actionAttr != null)
                {
                    var perArray = actionAttr.PermissionArray;//接口需要的权限Tag
                    var hearder = filterContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                    var jwtEntity = JwtEntity.GetJwtEntity(hearder);
                    if (jwtEntity != null)
                    {
                        bool isPermission = false;//是否有权限
                        if (jwtEntity.UserId == 1)
                        {
                            //admin账号有所有权限
                            isPermission = true;
                        }
                        else if (jwtEntity.UserId == 2 && filterContext.HttpContext.Request.Method.ToUpper() != "GET")
                        {
                            //测试账号只有get权限
                            isPermission = false;
                            filterContext.Result = new JsonResult(new ReturnMsgCode("000052", "测试账号没有操作数据权限，只有查询权限"));
                            return;
                        }
                        else
                        {
                            //查询账号所有的角色
                            var roleList = RedisHelper.HGet<List<string>>(RedisKeysEnum.AdminRoleHash.GetHFMallKey(), jwtEntity.UserId.ToString());
                            if (roleList != null)
                            {
                                //查询角色下面的菜单和按钮权限
                                var menuStrList = RedisHelper.HMGet<string>(RedisKeysEnum.RoleMenuHash.GetHFMallKey(), roleList.ToArray());
                                var menuList = new List<PermissionDto>();
                                foreach (var item in menuStrList)
                                {
                                    var rtopList = JSONHelper.ToList<PermissionDto>(item);
                                    menuList.AddRange(rtopList);
                                }
                                //判断接口所需要的权限是否在角色的权限中
                                if (menuList.Where(s => perArray.Contains(s.Tag)).ToList().Count > 0)
                                {
                                    isPermission = true;
                                }
                            }
                        }
                        if (!isPermission)
                        {
                            filterContext.Result = new JsonResult(new ReturnMsgCode("000050", "账号没有操作权限"));
                            return;
                        }
                    }
                    else
                    {
                        filterContext.Result = new JsonResult(new ReturnMsgCode("000051", "无法识别的Authorization类型"));
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
