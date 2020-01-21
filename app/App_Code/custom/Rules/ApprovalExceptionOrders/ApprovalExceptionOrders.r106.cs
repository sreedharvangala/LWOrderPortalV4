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
	public partial class ApprovalExceptionOrdersBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Custom" and argument that matches "RejectOrder".
        /// </summary>
        [Rule("r106")]
        public void r106Implementation(ApprovalExceptionOrdersModel instance)
        {
            // This is the placeholder for method implementation.
            if (instance.OrderHedID != null)
            {
                //8 is reject the Order
                int ordStatus = 8;

                SqlText.ExecuteNonQuery("Update OrderHed Set OrderStatusID=@OrderStatusID Where OrderHedID=@OrderHedID ",
                    new { @OrderHedID = instance.OrderHedID, @OrderStatusID = ordStatus });

                SqlText.ExecuteNonQuery(
                    "Insert into OrderStatusLog (OrderHedID,UserID,[Comment],[RejectReason],[OtherRemarks],[TranDate]) Values " +
                    "(@OrderHedID,@UserID,@Comment,@RejectReason,@OtherRemarks,@TranDate) "
                    , new
                    {
                        @OrderHedID = instance.OrderHedID,
                        @UserID = UserId,
                        @Comment = "Order Rejected",
                        @RejectReason = instance.RejectedReason,
                        @OtherRemarks = instance.RejectedRemarks,
                        @TranDate = DateTime.Now
                    });

                if (Arguments.SelectedValues.Count() > 1)
                {
                    var lastValue = Convert.ToInt32(Arguments.SelectedValues.ToList().LastOrDefault());
                    if (lastValue.Equals(instance.OrderHedID))
                        Result.ShowAlert("Orders Reject Process Done! ");
                }
                else
                {
                    Result.ShowAlert("OrderNo : {0} Rejected!", instance.OrderHedID);
                }

                
            }
            else
            {
                Result.ShowAlert("Please Select Row!");
            }
        }
    }
}
