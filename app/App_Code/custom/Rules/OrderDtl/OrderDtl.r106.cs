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
	public partial class OrderDtlBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Calculate".
        /// </summary>
        [Rule("r106")]
        public void r106Implementation(OrderDtlModel instance)
        {
            // This is the placeholder for method implementation.
            if(Arguments.CommandArgument=="PartSysRowID")
            {
                //instance.PartNum = null;
                //instance.PartDescription = null;
                instance.ChangeBasePrice = false;
                instance.ChangeSellingPrice = false;
                instance.ProposedBasePrice = null;
                instance.ProposedSellingPrice = null;
                instance.ProposedSellingPricePerBottle = null;
                instance.ProposedBasePricePerBottle = null;

                string priceLstQry = string.Format("SELECT isnull(BasePrice,0) , isnull(Price,0), isnull(SellingPricePerBottle,0), isnull(BasePricePerBottle,0) FROM vwPriceList WHERE Company='{0}' and CustNum={1} and PartNum='{2}' ",
                    instance.OHCompany,instance.OHCustNum,instance.PartNum);

                var priceLst= SqlText.Execute(priceLstQry);
                if(priceLst!=null)
                {
                    instance.BasePrice =(decimal) priceLst[0];
                    instance.SellingPrice = (decimal)priceLst[1];
                    instance.SellingPricePerBottle = (decimal)priceLst[2];
                    instance.BasePricePerBottle = (decimal)priceLst[3];

                    instance.Amount = instance.SellingPrice * instance.OrderQty;
                    instance.ProposedAmount = instance.SellingPrice * instance.OrderQty;
                }
            }
            if (Arguments.CommandArgument == "ChangeSellingPrice")
            {
                if (instance.ChangeSellingPrice==null)
                {
                    instance.ProposedSellingPrice = null;
                    instance.ProposedSellingPricePerBottle = null;
                    instance.ProposedAmount = instance.SellingPrice * instance.OrderQty;
                }

                if (instance.ChangeSellingPrice!=null && !(bool)instance.ChangeSellingPrice)
                {
                    
                    instance.ProposedSellingPrice = null;
                    instance.ProposedSellingPricePerBottle = null;
                    instance.ProposedAmount = instance.SellingPrice * instance.OrderQty;
                }

                
                
            }
            if (Arguments.CommandArgument == "ChangeBasePrice")
            {
                if (instance.ChangeBasePrice != null && !(bool)instance.ChangeBasePrice)
                {
                    instance.ProposedBasePrice = null;
                    instance.ProposedBasePricePerBottle = null;
                }

            }
            if (Arguments.CommandArgument == "OrderQty")
            {
                instance.Amount = instance.SellingPrice * instance.OrderQty;
                //instance.ProposedAmount = instance.SellingPrice * instance.OrderQty;
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
            if (Arguments.CommandArgument == "ProposedBasePrice")
            {
                if (instance.ProposedBasePrice != null && instance.CtnConv != null
                     && instance.ProposedBasePrice > 0 && instance.CtnConv > 0)
                        instance.ProposedBasePricePerBottle = instance.ProposedBasePrice / instance.CtnConv;
                else
                    instance.ProposedBasePricePerBottle = null;
            }
        }
    }
}
