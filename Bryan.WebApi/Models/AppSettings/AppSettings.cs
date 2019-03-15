using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Models.AppSettings
{
    /// <summary>
    /// 基本参数
    /// </summary>
    public class AppSettings
    {
        public bool IsLocal { get; set; }
        public string ImgHost { get; set; }
    }

}
