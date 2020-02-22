using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class Person : Name
    {
        public string Email { set; get; }
        public string Website { set; get; }
        public string Telephone { set; get; }
        public string Fax { set; get; }
        public string Companyname { set; get; }
        public Address address;
    }
}
