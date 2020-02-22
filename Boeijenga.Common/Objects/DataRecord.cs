using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DataRecord
/// </summary>
namespace Boeijenga.Common.Objects
{
    public class DataRecord
    {
        public System.Data.DataTable Table { get; set; }
        public long Count { get; set; }
    }
}
