using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    [Serializable]
    public class Country
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string IsEU { get; set; }
        public double ShippingCost { get; set; }
       
    }
}
