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
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Custom" and argument that matches "PriceChangeRejected".
        /// </summary>
        [Rule("r106")]
        public void r106Implementation(OrdersWaitingForPriceChangeModel instance)
        {
            // This is the placeholder for method implementation.
            int ordStatus = 8; // sent for reject 
            if (UserIsInRole("SalesClerk"))
            {
                SqlText.ExecuteNonQuery("Update OrderHed Set OrderStatusID=@OrderStatusID Where OrderHedID=@OrderHedID ",
                new { @OrderHedID = instance.OrderHedID, @OrderStatusID = ordStatus });

                SqlText.ExecuteNonQuery(
                    "Insert into OrderStatusLog (OrderHedID,UserID,[Comment],[RejectReason],[OtherRemarks],[TranDate]) Values " +
                    "(@OrderHedID,@UserID,@Comment,@RejectReason,@OtherRemarks,@TranDate) "
                    , new
                    {
                        @OrderHedID = instance.OrderHedID,
                        @UserID = UserId,
                        @Comment = "Price Change Request Rejected",
                        @RejectReason = "",
                        @OtherRemarks = "",
                        @TranDate=DateTime.Now
                    });
                Result.ShowAlert("OrderNo : {0} Price Change Request Rejected",instance.OrderHedID);
            }
        }
    }
}
