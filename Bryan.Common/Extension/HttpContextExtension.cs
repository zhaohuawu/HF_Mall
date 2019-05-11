using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Bryan.Common.Extension
{
    public static class HttpContextExtension
    {
        public static string GetIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(ip))
                ip = context.Connection.RemoteIpAddress.ToString();
            return ip;
        }

        public static string GetLocalIP()
        {
            string result;
            try
            {
                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect("www.qq.com", 80);
                string text = ((IPEndPoint)tcpClient.Client.LocalEndPoint).Address.ToString();
                tcpClient.Close();
                result = text;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public static string GetAbsoluteUri(this HttpRequest request)
        {
            return new StringBuilder()
                .Append(request.Scheme)
                .Append("://")
                .Append(request.Host)
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
        }

        public static string GetQueryStr(this HttpRequest request)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                stringBuilder.AppendLine();
                int num = 0;
                foreach (string text in request.Query.Keys)
                {
                    if (num == 0)
                    {
                        stringBuilder.Append(string.Format("?{0}={1}", text, request.Query[text]));
                    }
                    else
                    {
                        stringBuilder.Append(string.Format("&{0}={1}", text, request.Query[text]));
                    }
                    num++;
                }
            }
            catch (Exception)
            {
            }
            return stringBuilder.ToString();
        }
        
        public static string GetFormStr(this HttpRequest request)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                stringBuilder.AppendLine();
                foreach (KeyValuePair<string, StringValues> keyValuePair in request.Form)
                {
                    stringBuilder.Append(string.Format("{0} : {1}\r\n", keyValuePair.Key, keyValuePair.Value));
                }
            }
            catch (Exception)
            {
            }
            return stringBuilder.ToString();
        }
        
        public static string GetHeaderStr(this HttpRequest request)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                foreach (KeyValuePair<string, StringValues> keyValuePair in request.Headers)
                {
                    stringBuilder.Append(string.Format("{0} : {1}\r\n", keyValuePair.Key, keyValuePair.Value));
                }
            }
            catch (Exception)
            {
            }
            return stringBuilder.ToString();
        }
    }
}
