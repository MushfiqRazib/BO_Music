using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class OrderLine
    {
        public Int32 Orderid { set; get; }
        public string Articlecode { set; get; }
        public double Unitprice { set; get; }
        public double Vatpc { set; get; }
        public Int32 Quantity { set; get; }
        public double Discountpc { set; get; }
        public Int32 Creditedquantity { set; get; }
    }
}
