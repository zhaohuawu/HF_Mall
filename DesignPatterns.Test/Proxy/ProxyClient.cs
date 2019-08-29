using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Test.Proxy
{
    public class ProxyClient
    {
        public static void ProxyRequst()
        {
            // 创建一个代理对象并发出请求
            Person proxy = new Friend();
            proxy.BuyProduct();
        }
    }
}
