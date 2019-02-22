using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 用户角色枚举
    /// </summary>
    public enum EnumAppId
    {
        /// <summary>
        ///PC
        /// </summary>
        [Description("PC")]
        WebAdmin = 101,
        /// <summary>
        ///Android
        /// </summary>
        [Description("Android")]
        Android = 102,
        /// <summary>
        /// iPhone
        /// </summary>
        [Description("iPhone")]
        iPhone = 103,
        /// <summary>
        /// ESB
        /// </summary>
        [Description("ESB")]
        ESB = 104

    }

    /// <summary>
    /// session名枚举
    /// </summary>
    public enum EnumSession
    {
        /// <summary>
        /// 系统用户名
        /// </summary>
        [Description("系统用户名")]
        userName = 1,//
        /// <summary>
        /// 系统用户Id
        /// </summary>
        [Description("系统用户Id")]
        userId = 5,
        /// <summary>
        /// 系统用户权限Id
        /// </summary>
        [Description("系统用户权限Id")]
        roleId = 7,
        /// <summary>
        /// 操作的结果展示
        /// </summary>
        [Description("操作的结果展示")]
        msgShow = 10,//
        /// <summary>
        /// 系统用户Id
        /// </summary>
        [Description("操作错误")]
        errorShow = 11,//

    }

    /// <summary>
    /// cookie名枚举
    /// </summary>
    public enum EnumCookie
    {
        /// <summary>
        /// 系统用户名
        /// </summary>
        [Description("系统用户名")]
        userName = 1,//
        /// <summary>
        /// 系统用户Id
        /// </summary>
        [Description("系统用户Id")]
        userId = 5//
    }

    public enum EnumLogger
    {
        /// <summary>
        /// 公共记录名
        /// </summary>
        [Description("commonLogger")]
        commonLogger = 1

    }

    public enum EnumLoggerReository
    {
        /// <summary>
        /// 公共记录名
        /// </summary>
        [Description("NETCoreRepository")]
        NETCoreRepository = 1

    }

    /// <summary>
    /// 升序或降序
    /// </summary>
    public enum EnumOrderByType
    {
        /// <summary>
        ///升序
        /// </summary>
        [Description("升序")]
        ASC = 1,
        /// <summary>
        ///降序
        /// </summary>
        [Description("降序")]
        DESC = 2
    }

    /// <summary>
    /// 是否添加分页html
    /// </summary>
    public enum EnumIsPageNavStr
    {
        [Description("添加分页Html")]
        Yes = 1,
        [Description("不添加分页Html")]
        No = 2
    }

    /// <summary>
    /// 账号状态
    /// </summary>
    public enum EnumUserStatus
    {
        [Description("账号正常")]
        正常 = 1,
        [Description("账号已注销")]
        已注销 = 5
    }

    public enum EnumUploadFileIsDelete
    {
        [Description("正常")]
        正常 = 0,
        [Description("删除状态")]
        删除状态 = 1,
        [Description("原图删除")]
        原图删除 = 5
    }

    public enum EnumNewsOrderTypeId
    {
        [Description("热门资讯")]
        热门资讯 = 1,
        [Description("楼盘导购")]
        楼盘导购 = 5,
        [Description("人气楼盘")]
        人气楼盘 = 10,
    }

    public enum EnumArticleType
    {
        [Description("热门资讯")]
        普通文章 = 1,
        [Description("楼盘导购")]
        楼盘动态 = 5,
    }
}
