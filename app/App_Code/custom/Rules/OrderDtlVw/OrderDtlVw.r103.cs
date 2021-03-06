﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Finsoft.Data;
using Finsoft.Models;

namespace Finsoft.Rules
{
	public partial class OrderDtlVwBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Insert".
        /// </summary>
        [Rule("r103")]
        public void r103Implementation(OrderDtlVwModel instance)
        {
            // This is the placeholder for method implementation.
            if (instance.ChangeSellingPrice != null && !(bool)instance.ChangeSellingPrice)
            {
                instance.ProposedSellingPrice = null;
                instance.ProposedSellingPricePerBottle = null;

            }
        }
    }
}
