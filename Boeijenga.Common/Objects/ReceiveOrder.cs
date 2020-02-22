using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class ReceiveOrder
    {
        public Int32 Receiveid { get; set; }       
        public Int32 Supplyorderid { get; set; }       
        public DateTime Receivedate { get; set; }
        public double Shippingcost { get; set; }    
        public string Remarks { get; set; }
        public string Received_by { get; set; }
    
    }
}
