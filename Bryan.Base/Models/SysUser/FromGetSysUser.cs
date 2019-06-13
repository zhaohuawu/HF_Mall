using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.Base.Models.SysUser
{
    public class FromGetSysUser
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string RealName { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
    }
}
