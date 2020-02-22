using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boeijenga.Common.Objects
{
    public class Merchant
    {
        public string account { get; set; }
        public string site_id { get; set; }
        public string site_secure_code { get; set; }
        public string notification_url { get; set; }
        public string redirect_url { get; set; }
        public string cancel_url { get; set; }
        public string close_window { get; set; }
    }
}
