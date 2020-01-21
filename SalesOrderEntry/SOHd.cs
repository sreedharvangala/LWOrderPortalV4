using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderEntry
{
    public class SOHd
    {
        public int EpiOrderNum { get; set; }
        public int COTOrderNum { get; set; }
        public string Company { get; set; }
        public int CustNum { get; set; }
        public string CustId { get; set; }
        public string ShipToNum { get; set; }
        public string SalesRep { get; set; }
        public string Approver { get; set; }
        public string OrderComments { get; set; }
        public string InternalRemarks { get; set; }
        public string PromotionRemarks { get; set; }
        public string ManualSO { get; set; }
        public string Promotion { get; set; }
        public string PONum { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReqShipDate { get; set; }
        public bool BulkOrder { get; set; }
        public List<SODtl> SODtl { get; set; }
    }
}
