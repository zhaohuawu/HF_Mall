using Bryan.Domain.Interface;
using Bryan.Domain.Model;
using Common.Infrastructure.Net;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bryan.App
{
    public class LogAdminApp
    {
        protected ILog_AdminService _logAdmin { get; set; }
        public LogAdminApp(ILog_AdminService logAdmin)
        {
            _logAdmin = logAdmin;
        }
        
    }
}
