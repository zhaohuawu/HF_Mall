using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bryan.WebApi.Common
{
    public class TokenValidate : ISecurityTokenValidator
    {
        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; }

        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            ClaimsPrincipal principal;
            try
            {
                validatedToken = null;
                //这里需要验证生成的Token
                var token = new JwtSecurityToken(securityToken);
                //获取到Token的一切信息
                var payload = token.Payload;
                var name = (from t in payload where t.Key == ClaimTypes.Name select t.Value).FirstOrDefault();
                var Id = (from t in payload where t.Key == ClaimTypes.NameIdentifier select t.Value).FirstOrDefault();
                var issuer = token.Issuer;
                var key = token.SecurityKey;
                var audience = token.Audiences;
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, name.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Id.ToString()));
                principal = new ClaimsPrincipal(identity);
            }
            catch
            {
                validatedToken = null; principal = null;
            }
            return principal;
        }
    }
}
