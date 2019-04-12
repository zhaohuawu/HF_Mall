using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Bryan.Common.Jwt
{
    public class JwtAuthorizeFilter
    {
        public IConfiguration _configuration;
        public JwtSettings _jwtSettings;

        public JwtAuthorizeFilter(IConfiguration configuration, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            this._configuration = configuration;
            this._jwtSettings = jwtSettings.Value;
        }

        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    try
        //    {
        //        string[] array = context.HttpContext.Request.Headers["Authorization"].Split(' ', StringSplitOptions.None);
        //        string text = array[0];
        //        string token = array[1];
        //        //RSA key = RsaHelper.CreateRsaFromPublicKey(this._jwtSettings.PublicKey);
        //        //JObject jobject = JsonConvert.DeserializeObject<JObject>(JWT.Decode(token, key, JwsAlgorithm.RS256, null));
        //        //List<Claim> list = new List<Claim>();
        //        //foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
        //        //{
        //        //    Claim item = new Claim(keyValuePair.Key.ToString(), keyValuePair.Value.ToString());
        //        //    list.Add(item);
        //        //}
        //        //ClaimsIdentity claimsIdentity = new ClaimsIdentity();
        //        claimsIdentity.AddClaims(list);
        //        context.HttpContext.User.AddIdentity(claimsIdentity);
        //    }
        //    catch
        //    {
        //        context.Result = new UnauthorizedResult();
        //    }
        //}
    }
}
