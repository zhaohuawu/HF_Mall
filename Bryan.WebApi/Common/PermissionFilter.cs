using Common;
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

            if (actionAttr != null)
            {
                strNavName = actionAttr.PermissionStr;
                List<string> permissionList = new List<string>();
                permissionList.Add("sysiuii");
                if (!permissionList.Contains(strNavName))
                {
                    string code = "000050";
                    filterContext.Result = new JsonResult(new ReturnMsgCode(code, RedisOptHelper.GetMsgCode(code)));
                    return;
                }
            }

        }

    }
}
