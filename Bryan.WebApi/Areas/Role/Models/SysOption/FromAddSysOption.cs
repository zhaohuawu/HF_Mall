using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Areas.Role.Models.SysOption
{
    public class FromAddSysOption
    {
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
        /// Desc:排序
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int Orders { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public int IsHide { get; set; }

        /// <summary>
        /// Desc:级别
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int Levels { get; set; }
    }
}
