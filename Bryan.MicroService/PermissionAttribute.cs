using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Bryan.Common.Extension;

namespace Bryan.MicroService
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute : Attribute, IFilterMetadata
    {
        public string PermissionStr { get; set; }
        public string[] PermissionArray
        {
            get
            {
                return PermissionStr.ToSafeStrArray('|');
            }
        }

        public PermissionAttribute(string permissionStr)
        {
            this.PermissionStr = permissionStr;
        }
    }
}
