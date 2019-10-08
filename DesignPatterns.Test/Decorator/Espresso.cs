using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Test.Decorator
{
    /// <summary>
    /// 具体构件角色（浓缩咖啡）
    /// </summary>
    public class Espresso : Beverage
    {
        public Espresso()
        {
            _description = "Espresso";
        }
        public override double Cost()
        {
            return 20.99;
        }

        public override string GetDescription()
        {
            return _description;
        }
    }


}
