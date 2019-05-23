using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bryan.MicroService
{
    public class PermissionMiddleware
    {
        private readonly IHostingEnvironment env;
        //private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            this._next = next;
            this.env = env;
            //this._logger = logger;
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
                
                throw;
            }
        }

    }
}
