using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Bryan.Common.Entity
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

    }
}
