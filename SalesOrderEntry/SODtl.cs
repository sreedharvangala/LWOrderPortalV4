using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderEntry
{
    public class SODtl
    {
        public int COTOrderNum { get; set; }
        public int COTOrderLineNum { get; set; }
        public string PartNum { get; set; }
        public decimal OrderQty { get; set; }
        public decimal FOCQty { get; set; }
        public string UOM { get; set; }
        public decimal BasePrice { get; set; }
        public decimal ProposedBasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal ProposedSellingPrice { get; set; }
        public List<SORel> SORel { get; set; }
    }
}
