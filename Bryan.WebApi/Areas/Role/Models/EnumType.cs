using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Areas.Role.Models
{
    /// <summary>
    /// 后台操作日志
    /// </summary>
    public enum EnumLogAdminType
    {
        [Description("新增账号")]
        add_sysUser = 10,
        [Description("修改账号")]
        update_sysUser = 11,
        [Description("新增菜单")]
        add_sysMenu = 15,
        [Description("修改菜单")]
        update_sysMenu = 16,
        [Description("删除菜单")]
        delete_sysMenu = 17,
        [Description("新增角色")]
        add_sysRole = 18,
        [Description("修改角色")]
        update_sysRole = 19,
        [Description("删除角色")]
        delete_sysRole = 20,
    }

    public enum UserRoleStatus
    {
        add = 1,
        delete = 2
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
