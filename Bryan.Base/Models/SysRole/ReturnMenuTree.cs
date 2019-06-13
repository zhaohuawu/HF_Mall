using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.Base.Models.SysRole
{
    /// <summary>
    /// 菜单管理中的菜单树
    /// </summary>
    public class ReturnMenuTree
    {
        public ReturnMenuTree() { }
        public ReturnMenuTree(int id, string menuName, List<ReturnMenuTree> children)
        {
            this.Id = id;
            this.MenuName = menuName;
            this.Children = children;
        }
        /// <summary>
        /// 菜单主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜单名
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<ReturnMenuTree> Children { get; set; }
    }
}
