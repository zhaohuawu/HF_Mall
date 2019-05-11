using System;
using System.Collections.Generic;
using System.Text;

namespace Bryan.MicroService
{
    public class PermissionDto
    {
        /// <summary>
        /// Desc:菜单ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int MenuId { get; set; }

        /// <summary>
        /// Desc:角色ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int RoleId { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public string Tag { get; set; }
    }
}
