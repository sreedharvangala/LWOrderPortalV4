using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderEntry
{
    public class SORel
    {
        public int COTOrderLineNum { get; set; }
        public int COTOrderRelNum { get; set; }
        public string ShipToNum { get; set; }
        public decimal OrderRelQty { get; set; }
        public decimal FOCRelQty { get; set; }
        public DateTime ShipByDate { get; set; }
    }
}
