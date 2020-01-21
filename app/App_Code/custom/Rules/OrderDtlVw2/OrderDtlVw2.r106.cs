using System;
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
	public partial class OrderDtlVw2BusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Calculate".
        /// </summary>
        [Rule("r106")]
        public void r106Implementation(OrderDtlVw2Model instance)
        {
            // This is the placeholder for method implementation.
            if (Arguments.CommandArgument == "ProposedSellingPrice")
            {
                if (instance.ProposedSellingPrice != null && instance.CtnConv != null
                   && instance.ProposedSellingPrice > 0 && instance.CtnConv > 0)
                {
                    instance.ProposedSellingPricePerBottle = instance.ProposedSellingPrice / instance.CtnConv;
                    instance.ProposedAmount = instance.ProposedSellingPrice * instance.OrderQty;
                }
                else
                {
                    instance.ProposedSellingPricePerBottle = null;
                    instance.ProposedAmount = instance.SellingPrice * instance.OrderQty;
                }
            }
                
        }
    }
}
