using System;
using System.Linq;
using System.Text;

namespace BryanWu.Domain.Model
{
    ///<summary>
    ///后台系统权限
    ///</summary>
    public partial class Sys_AdminPermission
    {
        public Sys_AdminPermission()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

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
        /// Desc:
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime CrtDate { get; set; }

        /// <summary>
        /// Desc:创建用户ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int UserId { get; set; }

        /// <summary>
        /// Desc:菜单按钮Json
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BtnJson { get; set; }

    }
}
