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
	public partial class OrderRelBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view after an action
        /// with a command name that matches "Delete".
        /// </summary>
        [Rule("r104")]
        public void r104Implementation(OrderRelModel instance)
        {
            // This is the placeholder for method implementation.
            //if (Convert.ToBoolean(instance.OrderHedBulkOrder))
            //{
            //    int i = SqlText.ExecuteNonQuery("Update OrderDtl set OrderQty=(OrderQty - @Qty)  Where OrderDtlID=@OrderDtlID"
            //    , new { @Qty = instance.OrderRelQty, @OrderDtlID = instance.OrderDtlID });
            //}
            if(instance.Seq==1)
            {
                PreventDefault();
                Result.ShowAlert("First Release Can't Delete!");
            }
        }
    }
}
