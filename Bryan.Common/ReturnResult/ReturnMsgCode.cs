using Bryan.Common.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bryan.Common.ReturnResult
{
    /// <summary>
    /// Api请求结果
    /// </summary>
    public class ReturnMsgCode
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_code">返回码</param>
        /// <param name="_msg">返回信息</param>
        public ReturnMsgCode(string _code, string _msg)
        {
            this.code = _code;
            this.msg = _msg;
            this.data = null;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_code">返回码</param>
        /// <param name="_msg">返回信息</param>
        /// <param name="_data">返回数据</param>
        public ReturnMsgCode(string _code, string _msg, object _data)
        {
            this.code = _code;
            this.msg = _msg;
            this.data = _data;
        }

        /// <summary>
        /// 返回码（ReturnResultEnum枚举或自定义数据，枚举和自定义的数据不能重合）
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object data { get; set; }
    }

}
