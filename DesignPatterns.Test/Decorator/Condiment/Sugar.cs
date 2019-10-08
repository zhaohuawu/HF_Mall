using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Test.Decorator
{
    public class Sugar : CondimentDecorator
    {
        public Sugar(Beverage beverage) : base(beverage)
        {

        }
        public override string GetDescription()
        {
            return $"{_beverage.GetDescription()}，Sugar";
        }
        public override double Cost()
        {
            return 2.5 + _beverage.Cost();
        }
    }
}
