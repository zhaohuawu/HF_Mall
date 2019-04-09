using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BryanWu.Domain.Interface;
using Bryan.WebApi.Controllers;
using Common;
using Common.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Bryan.WebApi.Models.AppSettings;
using BryanWu.Domain.Model;

namespace Bryan.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private ISys_UserService _sysUserService;
        private JwtSettings _jwtSettings;
        public LoginController(ISys_UserService sysUserService, IOptions<JwtSettings> jwtSettings, ILog_AdminService logAdmin, ILog log)
        {
            _logAdmin = logAdmin;
            _log = log;
            this._sysUserService = sysUserService;
            this._jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("createtoken")]
        public async Task<IActionResult> CreateToken([FromForm]string username, [FromForm]string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username)) return ReturnJson("000101");
                else if (string.IsNullOrEmpty(password)) return ReturnJson("000102");

                string psw = AESUtil.EncryptPsw(password);
                var sysuser = await Task.Run(() => { return _sysUserService.GetEntity(p => p.UserName == username && p.Password == psw); });
                if (sysuser != null)
                {
                    if (sysuser.Status != (int)SysUserStatusEnum.Normal) return ReturnJson("000103");//1为正常使用账号
                    var now = DateTime.Now;
                    var exp = now.AddHours(_jwtSettings.Expires);
                    var claims = new Claim[]
                    {
                        new Claim("userId",sysuser.Id.ToString()),
                        new Claim("name",username),
                        new Claim("source","1")
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secretkey));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        claims: claims,
                        issuer: _jwtSettings.Issuer,
                        audience: _jwtSettings.Audience,
                        notBefore: now,
                        expires: exp,
                        signingCredentials: creds
                        );

                    return ReturnJson("000000", new { expires_in = exp, token_type = JwtBearerDefaults.AuthenticationScheme, access_token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                else
                    return ToJson(ReturnResultEnum.user_psw, "");
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return ReturnJson("000105");
            }
        }
    }
}