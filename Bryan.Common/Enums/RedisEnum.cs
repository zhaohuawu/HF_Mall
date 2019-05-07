using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bryan.Common.Enums
{
    public enum RedisKeysEnum
    {
        [Description("接口结果返回码")]
        ReturnCodeHash,
        [Description("账号角色列表")]
        AdminRoleHash,
        [Description("角色菜单列表")]
        RoleMenuHash,
    }
}
