using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Finsoft.Data;
using Finsoft.Models;
using SalesOrderEntry;

namespace Finsoft.Rules
{
	public partial class ApprovalNormalOrdersBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Custom" and argument that matches "ApproveOrder".
        /// </summary>
        [Rule("r105")]
        public void r105Implementation(ApprovalNormalOrdersModel instance)
        {
            // This is the placeholder for method implementation.
            if (instance.OrderHedID != null)
            {
                int ordStatus = 7;
                //if (UserIsInRole("SalesManager"))
                //{
                //    //6 = Lv2 approval when order is exception
                //    //7 = Approved when order is normal
                //    ordStatus = instance.Exception == true ? 6 : 7;
                //}               
                
                SqlText.ExecuteNonQuery("Update OrderHed Set OrderStatusID=@OrderStatusID Where OrderHedID=@OrderHedID ",
                    new { @OrderHedID=instance.OrderHedID, @OrderStatusID = ordStatus });

                SqlText.ExecuteNonQuery(
                    "Insert into OrderStatusLog (OrderHedID,UserID,[Comment],[RejectReason],[OtherRemarks],[TranDate]) Values " +
                    "(@OrderHedID,@UserID,@Comment,@RejectReason,@OtherRemarks,@TranDate) "
                    , new
                    {
                        @OrderHedID = instance.OrderHedID, 
                        @UserID = UserId,
                        @Comment = "Order Approved",
                        @RejectReason =instance.RejectedReason,
                        @OtherRemarks=instance.RejectedRemarks,
                        @TranDate=DateTime.Now
                    });



                EpiRequest SORequest = new EpiRequest();
                EpiResponse ObjEpiRes = SORequest.CreateSO((int)instance.OrderHedID);

                SqlText.ExecuteNonQuery("Update OrderHed Set EpiOrderNum=@EpiOrderNum, EpiError=@EpiError Where OrderHedID=@OrderHedID ",
                new { @OrderHedID = instance.OrderHedID, @EpiOrderNum = ObjEpiRes.EpiOrderNum, @EpiError = ObjEpiRes.ErrMsg });

                if (Arguments.SelectedValues.Count() > 1)
                {
                    var lastValue = Convert.ToInt32(Arguments.SelectedValues.ToList().LastOrDefault());
                    //if (lastValue.Equals(instance.OrderHedID))
                    //    Result.ShowAlert("Orders Approval Process Done! ");
                }
                else
                {
                    //Result.ShowAlert("OrderNo : {0} Approved and Created in Epicor EpiOrderNum {1}! "
                    //        , instance.OrderHedID, ObjEpiRes.EpiOrderNum);
                }
                //Result.ShowAlert("OrderNo : {0} Approved and Created in Epicor EpiOrderNum {1}! "
                //    , instance.OrderHedID, ObjEpiRes.EpiOrderNum);
            }
            else
            {
                Result.ShowAlert("Please Select Row!");
            }
        }
    }
}
