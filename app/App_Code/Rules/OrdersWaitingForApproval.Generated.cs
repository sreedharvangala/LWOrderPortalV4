using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Finsoft.Data;

namespace Finsoft.Rules
{
	public partial class OrdersWaitingForApprovalBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        [RowBuilder("OrdersWaitingForApproval", RowKind.New)]
        public void BuildNewOrdersWaitingForApproval()
        {
            UpdateFieldValue("TotalAmount", 0);
            UpdateFieldValue("Balance", 0);
            UpdateFieldValue("CreditLimit", 0);
            UpdateFieldValue("UnPaidInvoices", 0);
            UpdateFieldValue("OutStandingOrders", 0);
            UpdateFieldValue("UnPostedInvoices", 0);
            UpdateFieldValue("AgeCurr", 0);
            UpdateFieldValue("Age30", 0);
            UpdateFieldValue("Age60", 0);
            UpdateFieldValue("Age90", 0);
            UpdateFieldValue("Age120", 0);
            UpdateFieldValue("Age150", 0);
            UpdateFieldValue("TermsExceed", false);
        }
    }
}
