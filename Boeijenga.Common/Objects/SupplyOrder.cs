using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class SupplyOrder : DeliveryAddress
    {
        public Int32 Supplyorderid { get; set; }
        public DateTime Supplyorderdate { get; set; }
        public Int32 Supplierid { get; set; }
        public DateTime Deliverydate { get; set; }
        public string Supplyorder_by { get; set; }
        public string Receivingstatus { get; set; }
        public string Paymentstatus { get; set; }
    }
}
