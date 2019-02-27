using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Areas.Role.Models.SysUser
{
    /// <summary>
    /// 更新用户及用户权限参数
    /// </summary>
    public class FromUpdateSysUser
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 账号名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Password { get; set; }

        /// <summary>
        /// 角色列表
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public List<UserRole> RoleList { get; set; }
    }
}
