using Bryan.Common;
using Bryan.Common.Extension;
using Bryan.Gateway.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

                    if (!string.IsNullOrEmpty(authHeader))
                    {
                        var token = authHeader.Split(' ');
                        if (token.Length > 1)
                        {
                            if (token[0].ToLower() == "bearer")
                            {
                                var jwtToken = new JwtSecurityToken(token[1]);
                                var paloadStr = JSONHelper.Seriallize(jwtToken.Payload);
                                JObject objs = JsonConvert.DeserializeObject<JObject>(paloadStr);
                                //颁发机构
                                if (_jwt.Issuer != objs["iss"].ToString())
                                {
                                    throw new Exception("Token颁发机构异常");
                                }

                                //过期
                                if (DateTimeExtension.ConvertToCsharpTime(objs["exp"].ToSafeLong()) <= DateTime.Now)
                                {
                                    throw new Exception("授权已过期");
                                }
                                
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
