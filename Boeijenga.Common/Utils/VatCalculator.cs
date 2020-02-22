using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Utils
{
   public static  class VatCalculator
    {


       public static double GetVatIncludedPrice (double price , double vatPc)
       {
         //  (price + parseFloat(parseFloat((price * vatPc / 100).toFixed(2)).toFixed(2)));


           return Math.Round(price + (price * vatPc / 100),2);



       }


    }
}
