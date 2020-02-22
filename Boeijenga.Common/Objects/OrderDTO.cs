using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{  
    public class OrderDTO : DeliveryAddress
    {
        public Int32 Orderid { set; get; }
        public DateTime Orderdate { set; get; }
        public Int32 Customer { set; get; }
        public double Shippingcost { set; get; }
        public string Orderstatus { set; get; }
        public DateTime Invoicedate { set; get; }
        public string Remarks { set; get; }       
    }
}
