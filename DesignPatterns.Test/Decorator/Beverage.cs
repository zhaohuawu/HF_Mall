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
        public  string GetDescription()
        {
            return _description;
        }

        public abstract double Cost();
    }
}
