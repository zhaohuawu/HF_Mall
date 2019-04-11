using System;
using System.ComponentModel;
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
        /// 菜单类型
        /// </summary>
        public string Type { get; set; }

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

        /// <summary>
        /// 选中状态（checkd：选中，half：半选中）
        /// </summary>
        public string CheckStatus { get; set; }
    }

    public enum MenuTypeEnum
    {
        [Description("URL")]
        url,
        [Description("按钮")]
        btn,
    }
}
