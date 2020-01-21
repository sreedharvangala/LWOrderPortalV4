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
	public partial class OrderDtlVw2BusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        [RowBuilder("OrderDtlVw2", RowKind.New)]
        public void BuildNewOrderDtlVw2()
        {
            UpdateFieldValue("SellingPrice", 0);
            UpdateFieldValue("BasePrice", 0);
            UpdateFieldValue("ChangeSellingPrice", false);
            UpdateFieldValue("ChangeBasePrice", false);
            UpdateFieldValue("Amount", 0);
        }
    }
}
