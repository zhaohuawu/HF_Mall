using Bryan.Common;
using Bryan.Gateway.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bryan.Gateway.Middleware
{
    public class JwtMiddleware
    {
        private readonly IHostingEnvironment env;
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwt;

        public JwtMiddleware(RequestDelegate next, IHostingEnvironment env, ILogger<JwtMiddleware> logger, IOptions<JwtSettings> jwt)
        {
            _next = next;
            this.env = env;
            _logger = logger;
            _jwt = jwt.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    await _next(context);
                }
                else
                {
                    string authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

                    var jwtEntity = JwtEntity.GetJwtEntity(authHeader);

                    //颁发机构
                    if (_jwt.Issuer != jwtEntity.Iss)
                    {
                        throw new Exception("Token颁发机构异常");
                    }

                    //过期
                    if (DateTimeExtension.ConvertToCsharpTime(jwtEntity.Exp) <= DateTime.Now)
                    {
                        throw new Exception("授权已过期");
                    }

                    JObject objs = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(jwtEntity));
                    List<Claim> claims = new List<Claim>();
                    foreach (var property in objs)
                    {
                        var claim = new Claim(property.Key.ToString(), property.Value.ToString());
                        claims.Add(claim);
                    }

                    var ci = new ClaimsIdentity();
                    ci.AddClaims(claims);
                    context.User.AddIdentity(ci);


                    await _next(context);
                }
            }
            catch (Exception exp)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(exp.Message, Encoding.UTF8);
            }
        }
    }
}
