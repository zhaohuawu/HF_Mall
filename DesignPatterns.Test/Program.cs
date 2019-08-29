using DesignPatterns.Test.Decorator;
using DesignPatterns.Test.Proxy;
using System;

namespace DesignPatterns.Test
{
    // 客户端调用
    class Program
    {
        static void Main(string[] args)
        {
            //ProxyClient.ProxyRequst();
            DecoratorClient.Order();

            Console.Read();
        }
    }
}
