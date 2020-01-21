using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderEntry
{
    public class EpiResponse
    {
        public string Company { get; set; }
        public string CustId { get; set; }
        public int COTOrderNum { get; set; }
        public int EpiOrderNum { get; set; }
        public string ErrMsg { get; set; }
    }
}
