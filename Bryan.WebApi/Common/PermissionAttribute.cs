﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute : Attribute, IFilterMetadata
    {
        public string PermissionStr { get; set; }

        public PermissionAttribute(string permissionStr)
        {
            this.PermissionStr = permissionStr;
        }
    }
}
