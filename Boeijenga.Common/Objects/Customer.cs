using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class Customer : Person
    {
        public string Initialname { set; get; }
        public Int32 Customerid { set; get; }
        public string Password { set; get; }
        public double Discountpc { set; get; }
        public string Vatnr { set; get; }
        public string Dfirstname { set; get; }
        public string Dmiddlename { set; get; }
        public string Dlastname { set; get; }
        public string Dinitialname { set; get; }
        public Int32 Role { set; get; }
        public DeliveryAddress DeliveryAddress;
        public string Address { get; set; }

        //for multisafepay
        public string locale { get; set; }
        public string ipaddress { get; set; }
        public string forwardedip { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string Country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        public string Residence
        {
            get;
            set ;
        }
    }

}

