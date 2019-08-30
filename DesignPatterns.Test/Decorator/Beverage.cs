using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Test.Decorator
{
    /// <summary>
    /// 抽象构件角色（饮料）
    /// </summary>
    public abstract class Beverage
    {
        public string _description = "";
        public abstract string GetDescription();

        public abstract double Cost();
    }
}
