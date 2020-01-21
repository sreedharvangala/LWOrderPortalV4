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
	public partial class OrderStatusLogV2BusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        [RowBuilder("OrderStatusLogV2", RowKind.New)]
        public void BuildNewOrderStatusLogV2()
        {
            UpdateFieldValue("OrderHedID", SelectFieldValue("Context_OrderHedID"));
            UpdateFieldValue("OrderStatusID", 2);
        }
    }
}
