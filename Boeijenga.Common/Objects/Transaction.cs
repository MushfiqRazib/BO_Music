using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class Transaction
    {
        public string id { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
        public string description { get; set; }
        public string var1 { get; set; }
        public string var2 { get; set; }
        public string var3 { get; set; }
        public string items { get; set; }
        public bool manual { get; set; }
        public string gateway { get; set; }
        public string daysactive { get; set; }

    }
}
