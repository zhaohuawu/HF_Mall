using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Bryan.Domain.Sys
{
    ///<summary>
    ///后台系统菜单
    ///</summary>
    [Table("Sys_AdminMenu")]
    public partial class SysAdminMenu
    {
        public SysAdminMenu()
        {
            this.Level = 1;
            this.ChildNum = 0;
            this.IsShow = 1;
            this.Status = 1;
        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:父菜单ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Pid { get; set; }

        /// <summary>
        /// Desc:图标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Tag { get; set; }

        /// <summary>
        /// Desc:页面名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MenuName { get; set; }

        /// <summary>
        /// Desc:icon标识
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Icon { get; set; }

        /// <summary>
        /// Desc:等级
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int Level { get; set; }

        /// <summary>
        /// Desc:子菜单数量
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int ChildNum { get; set; }

        /// <summary>
        /// Desc:左移量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Orders { get; set; }

        /// <summary>
        /// Desc:是否显示（0:显示,1:隐藏）
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int IsShow { get; set; }

        /// <summary>
        /// Desc:页面链接
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Url { get; set; }

        /// <summary>
        /// Desc:创建人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CrtUser { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime CrtDate { get; set; }

        /// <summary>
        /// Desc:页面中按钮Json
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BtnJson { get; set; }

        /// <summary>
        /// 是否禁用，0:启用，1:禁用
        /// </summary>
        public int Status { get; set; }

    }
}
