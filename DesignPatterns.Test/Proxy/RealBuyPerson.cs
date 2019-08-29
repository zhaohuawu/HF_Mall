using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Test.Proxy
{
    //真实主题角色
    public class RealBuyPerson : Person
    {
        public override void BuyProduct()
        {
            Console.WriteLine("帮我买一个IPhone和一台苹果电脑");
        }
    }
}
