using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Areas.Role.Models
{
    /// <summary>
    /// 添加角色参数
    /// </summary>
    public class FromAddRole
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Desc:角色名称
        /// </summary>           
        public string RoleName { get; set; }

        /// <summary>
        /// Desc:角色描述
        /// </summary>           
        public string Remark { get; set; }

        /// <summary>
        /// 是否禁用，0：正常，1：禁用
        /// </summary>
        public int IsForbidden { get; set; }

        /// <summary>
        /// 角色权限下的菜单Id列表
        /// </summary>
        public List<RoleMenu> MenuList { get; set; }
    }

    /// <summary>
    /// 更新角色参数
    /// </summary>
    public class FromUpdateRole
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>           
        public string RoleName { get; set; }

        /// <summary>
        /// 是否禁用，0：正常，1：禁用
        /// </summary>
        public int IsForbidden { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>           
        public string Remark { get; set; }

        /// <summary>
        /// 角色权限下的菜单Id列表
        /// </summary>
        public List<RoleMenu> MenuList { get; set; }
    }
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
    }

    public class RoleMenu
    {
        /// <summary>
        /// 角色菜单权限ID（添加时0，删除时大于0）
        /// </summary>
        public int PerId { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 是否添加，0：删除，1：添加，2：更新
        /// </summary>
        public int Status { get; set; } = 1;
        /// <summary>
        /// 菜单按钮json字符串
        /// </summary>
        public string BtnJson { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 选中状态（checkd：选中，half：半选中）
        /// </summary>
        public string CheckStatus { get; set; }

    }

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
        /// 页面中按钮Json[{"code":"btn","type":"button/uri","name":"添加","description":"按钮描述","isForbidden":0,"url":"http://localhost/index"}]
        /// </summary>
        public string BtnJson { get; set; }
    }

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

    #region 枚举
    public enum RoleMenuStatus
    {
        delete = 0,
        add = 1,
        update = 2
    }
    #endregion
}
