using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Bryan.Common
{
    /// <summary>
    /// API请求结果代码枚举
    /// </summary>
    public enum ReturnResultEnum
    {
        /// <summary>
        /// 操作失败
        /// </summary>
        [Description("失败")]
        fail = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        success = 1,
        /// <summary>
        /// 系统错误
        /// </summary>
        [Description("系统错误")]
        system_error = 4,
        /// <summary>
        /// _appid为空
        /// </summary>
        [Description("_appid为空")]
        appid_isnull = 6,
        /// <summary>
        /// _code为空
        /// </summary>
        [Description("_code为空")]
        code_isnull = 8,
        /// <summary>
        /// 数据格式不正确
        /// </summary>
        [Description("数据格式不正确")]
        dataformat_error = 10,
        /// <summary>
        /// 提示
        /// </summary>
        [Description("提示")]
        information = 12,
        /// <summary>
        /// 没有数据
        /// </summary>
        [Description("没有数据")]
        nodata = 14,
        /// <summary>
        /// 未付费
        /// </summary>
        [Description("未付费")]
        nopay = 16,

        /// <summary>
        /// 获取token失败
        /// </summary>
        [Description("获取token失败")]
        token_fail = 50,
        /// <summary>
        /// 户账号或密码错误
        /// </summary>
        [Description("户账号或密码错误")]
        user_psw = 52,

        /// <summary>
        /// http错误
        /// </summary>
        [Description("http错误")]
        http_error = 301,

        /// <summary>
        /// 自定义错误
        /// </summary>
        [Description("自定义错误")]
        self_error = 101,
        /// <summary>
        /// 自定义提示
        /// </summary>
        [Description("自定义提示")]
        self_warn = 102,
        /// <summary>
        /// 第三方账号为空
        /// </summary>
        [Description("第三方账号为空")]
        third_account_isnull = 401,
        /// <summary>
        /// 第三方账号未绑定任何用户
        /// </summary>
        [Description("第三方账号未绑定任何用户")]
        third_account_unbind = 402,


    }
}
