using Bryan.Common;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Bryan.Gateway.Models
{
    public class JwtEntity
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public long Exp { get; set; }
        public string Source { get; set; }
        public long Nbf { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }

        public static JwtEntity GetJwtEntity(string header)
        {
            JwtEntity jwtEntity = new JwtEntity();

            if (!string.IsNullOrEmpty(header))
            {
                var token = header.Split(' ');
                if (token.Length > 1)
                {
                    if (token[0].ToLower() == "bearer")
                    {
                        var jwtToken = new JwtSecurityToken(token[1]);
                        var paloadStr = JSONHelper.Seriallize(jwtToken.Payload);
                        jwtEntity = JSONHelper.Dseriallize<JwtEntity>(paloadStr);
                        return jwtEntity;
                    }
                    else
                    {
                        throw new Exception("无法识别的Authorization类型");
                    }
                }
                else
                {
                    throw new Exception("Authorization值不符合规范");
                }
            }
            return null;
        }

    }
}
