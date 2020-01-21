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
	public partial class OrderDtlVwBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        [RowBuilder("OrderDtlVw", RowKind.New)]
        public void BuildNewOrderDtlVw()
        {
            UpdateFieldValue("SellingPrice", 0);
            UpdateFieldValue("BasePrice", 0);
            UpdateFieldValue("ChangeSellingPrice", false);
            UpdateFieldValue("ChangeBasePrice", false);
            UpdateFieldValue("Amount", 0);
        }
    }
}
