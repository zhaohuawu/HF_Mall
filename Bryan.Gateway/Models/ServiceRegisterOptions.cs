using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.Gateway.Models
{
    public class ServiceRegisterOptions
    {
        public bool IsActive { get; set; }
        public string ServiceName { get; set; }
        public Register Register { get; set; }
    }

    public class Register
    {
        public string HttpEndpoint { get; set; }
    }
}
