using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bryan.WebApi.Models
{
    public class ReturnUpload
    {
        /// <summary>
        /// 上传文件路径
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 上传文件Id
        /// </summary>
        public int uploadId { get; set; }
    }
}
