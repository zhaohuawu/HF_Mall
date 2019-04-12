using Bryan.Common.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bryan.Common
{
    /// <summary>
    /// API请求结果
    /// </summary>
    public class ReturnResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ReturnResult()
        {
            this.enumcode = ReturnResultEnum.success.GetDesc();
            this.code = (int)ReturnResultEnum.success;
            this.msg = "成功"; //ReturnResultEnum.success.ToString();
            this.data = "";
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_code">返回码</param>
        /// <param name="_msg">返回信息</param>
        public ReturnResult(ReturnResultEnum _code, string _msg)
        {
            this.code = (int)_code;
            this.enumcode = _code.GetDesc();
            this.msg = _msg;
            this.data = "";
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_code">返回码</param>
        /// <param name="_data">返回数据</param>
        public ReturnResult(ReturnResultEnum _code, object _data)
        {
            this.code = (int)_code;
            this.enumcode = _code.GetDesc();
            this.msg = "";
            this.data = _data;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_code">返回码</param>
        /// <param name="_msg">返回信息</param>
        /// <param name="_data">返回数据</param>
        public ReturnResult(ReturnResultEnum _code, string _msg, object _data)
        {
            this.code = (int)_code;
            this.enumcode = _code.GetDesc();
            this.msg = _msg;
            this.data = _data;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_code">返回码</param>
        /// <param name="_msg">返回信息</param>
        public ReturnResult(int _code, string _msg)
        {
            this.code = _code;
            this.enumcode = "";
            this.msg = _msg;
            this.data = "";
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_code">返回码</param>
        /// <param name="_msg">返回信息</param>
        /// <param name="_data">返回数据</param>
        public ReturnResult(int _code, string _msg, object _data)
        {
            this.code = _code;
            this.enumcode = "";
            this.msg = _msg;
            this.data = _data;
        }
        /// <summary>
        /// 返回码（ReturnResultEnum枚举或自定义数据，枚举和自定义的数据不能重合）
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回码枚举描述
        /// </summary>
        public string enumcode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object data { get; set; }
    }

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

    /// <summary>
    /// API请求结果
    /// </summary>
    public class ReturnResult<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ReturnResult()
        {
            this.code = (int)ReturnResultEnum.success;
            this.msg = ReturnResultEnum.success.ToString();
            this.data = default(T);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_code">返回码</param>
        /// <param name="_msg">返回信息</param>
        public ReturnResult(ReturnResultEnum _code, string _msg)
        {
            this.code = (int)_code;
            this.msg = _msg;
            this.data = default(T);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_code">返回码</param>
        /// <param name="_msg">返回信息</param>
        public ReturnResult(int _code, string _msg)
        {
            this.code = _code;
            this.msg = _msg;
            this.data = default(T);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_code">返回码</param>
        /// <param name="_msg">返回信息</param>
        /// <param name="_data">返回数据</param>
        public ReturnResult(ReturnResultEnum _code, string _msg, T _data)
        {
            this.code = (int)_code;
            this.msg = _msg;
            this.data = _data;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_code">返回码</param>
        /// <param name="_msg">返回信息</param>
        /// <param name="_data">返回数据</param>
        public ReturnResult(int _code, string _msg, T _data)
        {
            this.code = _code;
            this.msg = _msg;
            this.data = _data;
        }
        /// <summary>
        /// 返回码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }
    }
}
