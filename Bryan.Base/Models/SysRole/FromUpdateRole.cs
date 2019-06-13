using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.Base.Models.SysRole
{
    /// <summary>
    /// 更新角色参数
    /// </summary>
    public class FromUpdateRole
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>           
        public string RoleName { get; set; }

        /// <summary>
        /// 是否禁用，0：正常，1：禁用
        /// </summary>
        public int IsForbidden { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>           
        public string Remark { get; set; }

        /// <summary>
        /// 角色权限下的菜单Id列表
        /// </summary>
        public List<RoleMenu> MenuList { get; set; }
    }
}
