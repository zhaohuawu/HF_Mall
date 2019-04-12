using Bryan.Common;
using Bryan.Common.Enums;
using Bryan.Common.Extension;
using Bryan.Common.Jwt;
using Bryan.WebApi.Models;
using BryanWu.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Common
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
                    var jwtEntity = JwtEntity.GetJwtIEntity(hearder);
                    if (jwtEntity != null)
                    {
                        bool isPermission = false;//是否有权限
                                                  //查询账号所有的角色
                        var roleList = RedisHelper.HGet<List<string>>(RedisKeysEnum.AdminRoleHash.GetHFMallKey(), jwtEntity.UserId.ToString());
                        if (roleList.Count > 0)
                        {
                            //查询角色下面的菜单和按钮权限
                            var menuStrList = RedisHelper.HMGet<string>(RedisKeysEnum.RoleMenuHash.GetHFMallKey(), roleList.ToArray());
                            var menuList = new List<RoleToPermissionDto>();
                            Parallel.ForEach(menuStrList, item =>
                            {
                                var rtopList = JSONHelper.ToList<RoleToPermissionDto>(item);
                                menuList.AddRange(rtopList);
                            });
                            //判断接口所需要的权限是否在角色的权限中
                            if (menuList.Where(s => perArray.Contains(s.Tag)).ToList().Count > 0)
                            {
                                isPermission = true;
                            }
                        }

                        if (!isPermission)
                        {
                            string code = "000050";
                            filterContext.Result = new JsonResult(new ReturnMsgCode(code, RedisOptHelper.GetMsgCode(code)));
                            return;
                        }
                    }
                    else
                    {
                        string code = "000051";
                        filterContext.Result = new JsonResult(new ReturnMsgCode(code, RedisOptHelper.GetMsgCode(code)));
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
