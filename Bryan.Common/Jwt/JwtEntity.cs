using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Bryan.Common.Jwt
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

        public static JwtEntity GetJwtIEntity(string header)
        {
            JwtEntity jwtEntity = new JwtEntity();

            if (!string.IsNullOrEmpty(header))
            {
                try
                {
                    var token = header.Split(' ');
                    if (token.Length > 1)
                    {
                        var jwtToken = new JwtSecurityToken(token[1]);
                        var paloadStr = JSONHelper.Seriallize(jwtToken.Payload);
                        jwtEntity = JSONHelper.Dseriallize<Bryan.Common.Jwt.JwtEntity>(paloadStr);
                        return jwtEntity;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

    }
}
