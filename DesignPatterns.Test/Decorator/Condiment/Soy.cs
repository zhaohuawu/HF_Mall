using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Test.Decorator
{
    /// <summary>
    /// 具体装饰角色（大豆）
    /// </summary>
    public class Soy : CondimentDecorator
    {

        public Soy(Beverage beverage) : base(beverage)
        {

        }
        public override string GetDescription()
        {
            return $"{_beverage.GetDescription()}，Soy";
        }
        public override double Cost()
        {
            return 2.5 + _beverage.Cost();
        }
    }
}
