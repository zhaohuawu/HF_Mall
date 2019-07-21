using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Bryan.Domain.Sys
{
    ///<summary>
    ///记录后台登录日志
    ///</summary>
    [Table("Sys_LoginLog")]
    public partial class SysLoginLog
    {
        public SysLoginLog()
        {

            this.typeId = 1;

        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string userName { get; set; }

        /// <summary>
        /// Desc:1：后台,5：用户
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int typeId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? userId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime crtDate { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ip { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string content { get; set; }

    }
}
