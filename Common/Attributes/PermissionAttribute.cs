using System;
using System.Collections.Generic;
using System.Text;

namespace Bryan.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class PermissionAttribute : Attribute
    {
        private string PermissonStr { get; set; }

        public PermissionAttribute()
        {

        }
        public PermissionAttribute(string permissonStr)
        {
            this.PermissonStr = permissonStr;
        }


    }
}
