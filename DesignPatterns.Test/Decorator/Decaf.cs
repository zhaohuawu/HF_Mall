using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Test.Decorator
{
    /// <summary>
    /// 具体构件角色（脱因咖啡）
    /// </summary>
    public class Decaf : Beverage
    {
        public Decaf()
        {
            _description = "Decaf";
        }
        public override double Cost()
        {
            return 20.99;
        }
    }
}
