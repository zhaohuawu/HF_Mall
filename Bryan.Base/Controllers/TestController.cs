using Bryan.BaseService.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Bryan.WebApi.Models;
using Microsoft.Extensions.Logging;
using Bryan.MicroService;

namespace Bryan.WebApi.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : BaseController
    {
        private ILog_AdminService _logAdmin;
        public TestController(ILog_AdminService logAdmin, ILogger<TestController> log)
        {
            _logAdmin = logAdmin;
            _log = log;
        }

        /// <summary>
        /// 测试代码sssss
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(TestDto), 200)]
        public IActionResult Get(string code)
        {
            return ReturnJson(code);
        }
        /// <summary>
        /// 测试多参数返回json {"code":"000000","paramStr":"kjk,kllk"}
        /// </summary>
        /// <param name="obj">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostParams([FromBody]JObject obj)
        {
            string code = obj["code"].ToString();
            string paramStr = obj["paramStr"] != null ? obj["paramStr"].ToString() : "";

            if (string.IsNullOrEmpty(paramStr))
                return ReturnJson(code);
            else
                return ReturnJsonByParms(code, null, paramStr.Split(','));
        }

        /// <summary>
        /// 测试多参数返回json {"code":"000000","paramStr":"kjk,kllk"}
        /// </summary>
        /// <param name="obj">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostParamsFromForm([FromForm]JObject obj)
        {
            string code = obj["code"].ToString();
            string paramStr = obj["paramStr"] != null ? obj["paramStr"].ToString() : "";

            if (string.IsNullOrEmpty(paramStr))
                return ReturnJson(code);
            else
                return ReturnJsonByParms(code, null, paramStr.Split(','));
        }

        /// <summary>
        /// 测试多参数返回json {"code":"000000","paramStr":"kjk,kllk"}
        /// </summary>
        /// <param name="obj">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostTest(string obj)
        {
            return ReturnJson("000000", obj);
        }

        /// <summary>
        /// 双FromBody
        /// </summary>
        /// <param name="code"></param>
        /// <param name="paramStr"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostTestTow([FromForm]string code, [FromForm]string paramStr)
        {
            if (string.IsNullOrEmpty(paramStr))
                return ReturnJson(code);
            else
                return ReturnJsonByParms(code, null, paramStr.Split(','));
        }
        
    }
}