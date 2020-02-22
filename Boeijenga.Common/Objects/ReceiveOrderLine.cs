using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class ReceiveOrderLine
    {
        public Int32 Receiveid { get; set; }
        public Int32 Receiveqty { get; set; }      
        public double Purchaseprice { get; set; }
        public string Articlecode { get; set; }
     
    }
}
