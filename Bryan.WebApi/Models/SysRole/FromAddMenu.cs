using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Models.SysRole
{
    /// <summary>
    /// 添加菜单参数
    /// </summary>
    public class FromAddMenu
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 父菜单ID
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 标识字符串
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>           
        public string MenuName { get; set; }

        /// <summary>
        /// icon图标
        /// </summary>           
        public string Icon { get; set; }

        /// <summary>
        /// 菜单级别
        /// </summary>           
        public int Level { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Orders { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// 是否禁用，0:启用，1:禁用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Desc:是否显示（0:显示,1:隐藏）
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public int IsShow { get; set; }
    }
}
