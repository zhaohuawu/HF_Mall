using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Test.Decorator
{
    /// <summary>
    ///  具体装饰角色（摩卡）
    /// </summary>
    public class Mocha : CondimentDecorator
    {
        public Mocha(Beverage beverage) : base(beverage)
        {

        }
        public override string GetDescription()
        {
            return $"{_beverage.GetDescription()}，Mocha";
        }
        public override double Cost()
        {
            return 5.5 + _beverage.Cost();
        }
    }
}
