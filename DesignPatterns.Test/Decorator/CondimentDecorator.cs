using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Test.Decorator
{
    /// <summary>
    /// 抽象装饰角色（调料）
    /// </summary>
    public abstract class CondimentDecorator : Beverage
    {
        public Beverage _beverage;

        public CondimentDecorator(Beverage beverage)
        {
            this._beverage = beverage;
        }
        public override string GetDescription()
        {
            return _beverage.GetDescription();
        }
        public override double Cost()
        {
            return _beverage.Cost();
        }
    }
}
