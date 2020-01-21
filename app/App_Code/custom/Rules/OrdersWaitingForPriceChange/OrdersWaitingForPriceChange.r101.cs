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
	public partial class OrdersWaitingForPriceChangeBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view after an action
        /// with a command name that matches "Calculate" and argument that matches "".
        /// </summary>
        [Rule("r101")]
        public void r101Implementation(OrdersWaitingForPriceChangeModel instance)
        {
            // This is the placeholder for method implementation.
            if (Arguments.CommandArgument == "OrderDtlVw2")
            {
                //decimal total = (decimal)SqlText.ExecuteScalar("select isnull(Sum(SellingPrice * OrderQty),0) From OrderDtl Where OrderHedID=@OrderHedID ",
                //                    instance.OrderHedID);
                //instance.TotalAmount = total;

                //int i = SqlText.ExecuteNonQuery("Update OrderHed Set TotalAmount=@total Where OrderHedID=@OrderHedID",
                //    total, instance.OrderHedID);

                decimal total = (decimal)SqlText.ExecuteScalar("select isnull(Sum(SellingPrice * OrderQty),0) From OrderDtl Where OrderHedID=@OrderHedID ",
                                   instance.OrderHedID);

                decimal proposedTotal = (decimal)SqlText.ExecuteScalar("select isnull(Sum(IIF(ProposedSellingPrice is null, SellingPrice,ProposedSellingPrice) * OrderQty),0) From OrderDtl Where OrderHedID=@OrderHedID ",
                                    instance.OrderHedID);

                instance.TotalAmount = proposedTotal > 0 ? proposedTotal : total;

                string totalAmount = instance.TotalAmount.ToString();

                int i = SqlText.ExecuteNonQuery("Update OrderHed Set TotalAmount=@totalAmount Where OrderHedID=@OrderHedID",
                    instance.TotalAmount, instance.OrderHedID);
            }
        }
    }
}
