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
        public abstract new string GetDescription();

    }
}
