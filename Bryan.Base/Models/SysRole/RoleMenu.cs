using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.Base.Models.SysRole
{
    public class RoleMenu
    {
        /// <summary>
        /// 角色菜单权限ID（添加时0，删除时大于0）
        /// </summary>
        public int PerId { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 是否添加，0：删除，1：添加，2：更新
        /// </summary>
        public int Status { get; set; } = 1;
        /// <summary>
        /// 菜单按钮json字符串
        /// </summary>
        public string BtnJson { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 选中状态（checkd：选中，half：半选中）
        /// </summary>
        public string CheckStatus { get; set; }

    }
}
