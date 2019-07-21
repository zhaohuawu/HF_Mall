using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Bryan.Domain.Sys
{
    ///<summary>
    ///后台用户权限组
    ///</summary>
    [Table("Sys_AdminRole")]
    public partial class SysAdminRole
    {
        public SysAdminRole()
        {
            this.IsForbidden = 1;
        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:角色名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string RoleName { get; set; }

        /// <summary>
        /// Desc:角色描述
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Remark { get; set; }

        /// <summary>
        /// Desc:创建用户ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int UserId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:True
        /// </summary>           
        public DateTime CrtDate { get; set; }

        /// <summary>
        /// 是否禁用，0：正常，1：禁用
        /// </summary>
        public int IsForbidden { get; set; }
        /// <summary>
        /// Desc:修改日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ModifyDate { get; set; }

    }
}
