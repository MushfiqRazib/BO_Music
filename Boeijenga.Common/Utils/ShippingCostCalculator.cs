using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Boeijenga.Common.Objects;

namespace Boeijenga.Common.Utils
{
    public static  class ShippingCostCalculator
    {

        public static double GetShippingCostVat( Country country)
        {
          return   country.ShippingCost;

        }


    }
}
