using DesignPatterns.Test.Proxy;
using System;

namespace DesignPatterns.Test
{
    // 客户端调用
    class Program
    {
        static void Main(string[] args)
        {
            // 创建一个代理对象并发出请求
            Person proxy = new Friend();
            proxy.BuyProduct();
            Console.Read();
        }
    }
}
