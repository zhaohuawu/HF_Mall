using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.Common.Jwt
{
    /// <summary>
    /// Jwt认证参数
    /// </summary>
    public class JwtSettings
    {
        // Token: 0x04000025 RID: 37
        private string _publicKey = string.Empty;

        // Token: 0x04000026 RID: 38
        private string _privateKey = string.Empty;


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

        /// <summary>
        /// 公钥
        /// </summary>
        public string PublicKey
        {
            get
            {
                if (this._publicKey.Length > 100)
                {
                    return this._publicKey;
                }
                base.GetType();
                if (!string.IsNullOrEmpty(this._publicKey))
                {
                    using (StreamReader streamReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), this._publicKey)))
                    {
                        string publicKey = streamReader.ReadToEnd();
                        this._publicKey = publicKey;
                    }
                }
                return this._publicKey;
            }
            set
            {
                this._publicKey = value;
            }
        }

        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKey
        {
            get
            {
                if (this._privateKey.Length > 100)
                {
                    return this._privateKey;
                }
                base.GetType();
                if (!string.IsNullOrEmpty(this._publicKey))
                {
                    using (StreamReader streamReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), this._privateKey)))
                    {
                        string privateKey = streamReader.ReadToEnd();
                        this._privateKey = privateKey;
                    }
                }
                return this._privateKey;
            }
            set
            {
                this._privateKey = value;
            }
        }


    }
}
