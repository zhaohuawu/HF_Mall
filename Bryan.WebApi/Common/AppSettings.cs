using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Common
{
    /// <summary>
    /// 基本参数
    /// </summary>
    public class AppSettings
    {
        public string isLocal { get; set; }
        public string imgHost { get; set; }
    }
    /// <summary>
    /// Jwt认证参数
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// 加密字符串(至少16位)
        /// </summary>
        public string Secretkey { get; set; }
        /// <summary>
        /// 证书颁发者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 允许使用的角色
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// token过期时间（单位：小时）
        /// </summary>
        public double Expires { get; set; }
    }
}
