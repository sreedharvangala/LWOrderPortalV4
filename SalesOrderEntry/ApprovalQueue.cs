using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderEntry
{
    public class ApprovalQueue
    {
        public string CompanyID { get; set; }

        public string ApprovalQueueID { get; set; }

        public string ModuleID { get; set; }

        public string DocumentNo { get; set; }

        public string ApprovalStatus { get; set; }

        public string ApprovalStage { get; set; }

        public string SupplierID { get; set; }

        public string Remarks { get; set; }

        public DateTime SubmitDate { get; set; }

        public DateTime ProceedDate { get; set; }
    }
}
