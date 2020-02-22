using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class Invoice: Address
    {
        public Int32 Invoiceid { get; set; }
        public DateTime Invoicedate { get; set; }
        public Int32 Customer { get; set; }
        public string Customerbtwnr { get; set; }
        public DateTime Transferedon { get; set; }
        public string Remarks { get; set; }
        public string Invoicestatus { get; set; }
        public Int32 Credit { get; set; }

        
    }
}
