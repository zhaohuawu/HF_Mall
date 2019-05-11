using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Models.SysRole
{
    /// <summary>
    /// 添加角色中的授权页面菜单树
    /// </summary>
    public class ReturnRoleMenuTree
    {
        public ReturnRoleMenuTree() { }
        public ReturnRoleMenuTree(string typeId, int id, string menuName, string type, List<ReturnRoleMenuTree> children)
        {
            this.TypeId = typeId;
            this.Id = id;
            this.MenuName = menuName;
            this.Type = type;
            this.Children = children;
            this.Disabled = false;
        }
        public string TypeId { get; set; }
        /// <summary>
        /// 菜单主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜单名
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<ReturnRoleMenuTree> Children { get; set; }
        /// <summary>
        /// 是否禁止点击
        /// </summary>
        public bool Disabled { get; set; }
    }
}
