using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 返回结果异常
    /// </summary>
    public class ReturnResultException : Exception
    {
        public ReturnResultException(ReturnResultEnum code, string msg, HttpStatusCode status = HttpStatusCode.OK)
        {
            Code = (int)code;
            Msg = msg;
            HttpStatus = status;
        }

        public ReturnResultException(ReturnResultEnum code, string msg, HttpStatusCode status, Exception exception)
            : base(msg, exception)
        {
            Code = (int)code;
            Msg = msg;
            HttpStatus = status;
        }

        public ReturnResultException(int code, string msg, HttpStatusCode status = HttpStatusCode.OK)
        {
            Code = code;
            Msg = msg;
            HttpStatus = status;
        }

        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// Http状态码
        /// </summary>
        public HttpStatusCode HttpStatus { get; set; }
    }
}
