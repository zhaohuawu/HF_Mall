using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Test.Decorator
{
    /// <summary>
    /// 下订单逻辑
    /// </summary>
    public class DecoratorClient
    {
        public static void Order()
        {
            Beverage beverage = new Espresso();
            Console.WriteLine($"饮料材料：{beverage.GetDescription()}，价格：{beverage.Cost()}");

            Beverage beverage1 = new Decaf();
            beverage1 = new Mocha(beverage1);
            beverage1 = new Mocha(beverage1);
            beverage1 = new Milk(beverage1);
            beverage1 = new Soy(beverage1);
            beverage1 = new Sugar(beverage1);
            Console.WriteLine($"饮料材料：{beverage1.GetDescription()}，价格：{beverage1.Cost()}");

        }
    }
}
