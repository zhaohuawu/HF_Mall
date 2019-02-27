using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Areas.Role.Models.SysUser
{
    public class UserRole
    {
        /// <summary>
        /// 账号角色绑定ID（默认为0，>0为修改或删除）
        /// </summary>
        public int UserRoleId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 状态（1：添加，2：删除）
        /// </summary>
        public int Status { get; set; }
    }
}
