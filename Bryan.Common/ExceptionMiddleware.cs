using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bryan.Common.Extension;
using Microsoft.AspNetCore.Builder;

namespace Bryan.Common
{
    public class RequestInfo
    {
        public static string ServerUrl { get; set; }
        public string Path { get; set; }
        public string Query { get; set; }
        public string FormData { get; set; }
        public string Header { get; set; }
        public Exception Exception { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly IHostingEnvironment env;
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, IHostingEnvironment env, ILogger<ExceptionMiddleware> logger)
        {
            this._next = next;
            this.env = env;
            this._logger = logger;
        }
        
        private void AddLog(RequestInfo ri, Exception ex, int level)
        {
            string exceptionStr = this.GetExceptionStr(ri, ex);
            this._logger.LogError(exceptionStr, Array.Empty<object>());
        }
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                var innerEx = ex.GetBaseException();
                HttpRequest request = context.Request;
                RequestInfo ri = new RequestInfo
                {
                    Path = string.Format("{0}{1}", RequestInfo.ServerUrl, request.Path),
                    Query = request.GetQueryStr(),
                    FormData = request.GetFormStr(),
                    Header = request.GetHeaderStr()
                };
                this.AddLog(ri, innerEx, 3);
                throw;
            }
        }
        
        private string GetExceptionStr(RequestInfo ri, Exception ex)
        {
            string newLine = Environment.NewLine;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("//****************************************************\r\n");
            stringBuilder.Append("异常时间：(" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + ")\r\n");
            stringBuilder.Append("出错页面：(" + ri.Path + ")\r\n");
            stringBuilder.Append("form参数信息：(" + ri.Query + ")\r\n");
            stringBuilder.Append("form参数信息：(" + ri.FormData + ")\r\n");
            stringBuilder.Append("header参数信息：(" + ri.Header + ")\r\n");
            string text = ex.Message;
            stringBuilder.Append("异常信息：(" + text + ")\r\n");
            stringBuilder.Append(string.Format("异常方法：({0})\r\n", ex.TargetSite));
            stringBuilder.Append("异常来源：(" + ex.Source + ")\r\n");
            stringBuilder.Append("异常处理：" + newLine + ex.StackTrace.Replace("   ", "\r\n   ").Replace("--- ", "\r\n--- ") + "\r\n");
            stringBuilder.Append(string.Concat(new object[]
            {
                "异常实例：",
                newLine,
                ex.InnerException,
                "\r\n"
            }));
            stringBuilder.Append("//*******************************************************\r\n");
            return stringBuilder.ToString();
        }
    }

    /// <summary>
    ///     异常中间件扩展
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

