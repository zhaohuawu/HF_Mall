using Bryan.MicroService.Jwt;
using System;
using System.Text.RegularExpressions;

namespace Bryan.MicroService
{
    public class SysConfig
    {
        /// <summary>
        /// 服务编码
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// SwaggerXml名称
        /// </summary>
        public string XmlName { get; set; }

        private string _version;
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get
            {
                return this._version;
            }
            set
            {
                if (!new Regex("\\d+(\\.\\d+){0,2}").IsMatch(value))
                {
                    throw new Exception("版本号不符合规范.");
                }
                this._version = value;
            }
        }

        public string LocalAddress { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceDiscoveryAddress { get; set; }

        /// <summary>
        /// Redis链接字符串
        /// </summary>
        public string RedisConnectionString { get; set; }

        /// <summary>
        /// Redis默认关键字
        /// </summary>
        public string RedisDefaultKey { get; set; }

        /// <summary>
        /// JWT认证参数
        /// </summary>
        public JwtSettings JwtSettings { get; set; }

    }
}
