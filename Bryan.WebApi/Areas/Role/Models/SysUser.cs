using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Areas.Role.Models
{
    /// <summary>
    /// 添加用户参数
    /// </summary>
    public class FromAddSysUser
    {
        /// <summary>
        /// 账号名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Password { get; set; }

        /// <summary>
        /// 角色列表
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public List<UserRole> RoleList { get; set; }
    }

    /// <summary>
    /// 更新用户及用户权限参数
    /// </summary>
    public class FromUpdateSysUser
    {
        /// <summary>
        /// 账号ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 账号名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Password { get; set; }

        /// <summary>
        /// 角色列表
        /// Default:1
        /// Nullable:False
        /// </summary>           
        public List<UserRole> RoleList { get; set; }
    }

    public class FromGetSysUser
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string RealName { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
    }

    public class UserRole
    {
        /// <summary>
        /// 账号角色绑定ID（默认为0，>0为修改或删除）
        /// </summary>
        public int UserRoleId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 状态（1：添加，2：删除）
        /// </summary>
        public int Status { get; set; }
    }

    #region 枚举
    public enum UserRoleStatus
    {
        add = 1,
        delete = 2
    }
    #endregion
}
