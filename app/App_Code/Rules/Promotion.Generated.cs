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
	public partial class PromotionBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        [RowBuilder("Promotion", RowKind.New)]
        public void BuildNewPromotion()
        {
            UpdateFieldValue("Active", true);
        }
    }
}
