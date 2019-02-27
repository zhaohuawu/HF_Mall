using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Areas.Role.Models.SysRole
{
    public class FromAddMenuBtn
    {
        public int Id { get; set; }
        /// <summary>
        /// Desc:菜单Id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int MenuId { get; set; }

        /// <summary>
        /// Desc:按钮或资源编码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Code { get; set; }

        /// <summary>
        /// Desc:按钮或资源类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Type { get; set; }

        /// <summary>
        /// Desc:按钮或资源名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Description { get; set; }

        /// <summary>
        /// Desc:url或其他
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Url { get; set; }

        /// <summary>
        /// Desc:是否禁用，0:启用，1:禁用
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int IsForbidden { get; set; }
    }
}
