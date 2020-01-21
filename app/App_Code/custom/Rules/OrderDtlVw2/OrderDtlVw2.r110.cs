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
        /// with a command name that matches "Select".
        /// </summary>
        [Rule("r110")]
        public void r110Implementation(OrderDtlVw2Model instance)
        {
            // This is the placeholder for method implementation.
            if (instance.SellingPrice != null && instance.CtnConv != null)
            {
                if (Convert.ToDecimal(instance.CtnConv) > 0 && Convert.ToDecimal(instance.SellingPrice) > 0)
                    instance.SellingPricePerBottle = instance.SellingPrice / instance.CtnConv;
            }
            if (instance.BasePrice != null && instance.CtnConv != null)
            {
                if (Convert.ToDecimal(instance.CtnConv) > 0 && Convert.ToDecimal(instance.BasePrice) > 0)
                    instance.BasePricePerBottle = instance.BasePrice / instance.CtnConv;
            }
            if (instance.ProposedSellingPrice != null && instance.CtnConv != null
                    && instance.ProposedSellingPrice > 0 && instance.CtnConv > 0)
                instance.ProposedSellingPricePerBottle = instance.ProposedSellingPrice / instance.CtnConv;
            else
                instance.ProposedSellingPricePerBottle = null;

            if (instance.ProposedBasePrice != null && instance.CtnConv != null
                     && instance.ProposedBasePrice > 0 && instance.CtnConv > 0)
                instance.ProposedBasePricePerBottle = instance.ProposedBasePrice / instance.CtnConv;
            else
                instance.ProposedBasePricePerBottle = null;

        }
    }
}
