using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Test.Decorator
{
    /// <summary>
    /// 具体装饰角色（脑弱）
    /// </summary>
    public class Milk : CondimentDecorator
    {

        public Milk(Beverage beverage) : base(beverage)
        {

        }
        public override string GetDescription()
        {
            return $"{_beverage.GetDescription()}，Milk";
        }
        public override double Cost()
        {
            return 3.5 + _beverage.Cost();
        }
    }
}
