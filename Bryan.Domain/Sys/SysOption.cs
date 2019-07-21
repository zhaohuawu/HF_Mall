using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Bryan.Domain.Sys
{
    ///<summary>
    ///系统操作参数管理
    ///</summary>
    [Table("Sys_Option")]
    public partial class SysOption
    {
        public SysOption()
        {
            this.EnumCode = 0;
            this.Levels = 1;
            this.Orders = 0;
            this.IsHide = 0;
        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:参数名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GroupName { get; set; }

        /// <summary>
        /// Desc:参数关键词
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GroupKey { get; set; }

        /// <summary>
        /// Desc:参数编码
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int EnumCode { get; set; }

        /// <summary>
        /// Desc:参数编码类型名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string EnumName { get; set; }

        /// <summary>
        /// Desc:参数编码类型描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string EnumLabel { get; set; }

        /// <summary>
        /// Desc:参数编码类型描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Remark { get; set; }

        /// <summary>
        /// Desc:级别
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int Levels { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Orders { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime CrtDate { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public int IsHide { get; set; }

    }
}
