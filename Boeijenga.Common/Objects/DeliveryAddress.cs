using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class DeliveryAddress 
    {
        private string _dhousenr = string.Empty;
        private string _daddress = string.Empty;
        private string _dpostcode = string.Empty;
        private string _dresidence = string.Empty;
        private string _dcountry = string.Empty;

        public string Dhousenr
        {
            get { return _dhousenr; }
            set { _dhousenr = value; }          
        }
        public string Daddress
        {
            get
            {
                return _daddress;
            }
            set { _daddress = value; }
           
        }
        public string Dpostcode { get { return _dpostcode; } set { _dpostcode = value; } }
        public string Dresidence { get { return _dresidence; } set { _dresidence = value; } }
        public string Dcountry { get { return _dcountry;} set {_dcountry = value;} }
    }
}
