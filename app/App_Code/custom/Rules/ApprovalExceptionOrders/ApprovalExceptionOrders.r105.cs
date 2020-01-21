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
    //public static int? OrderHedID2;
    public partial class ApprovalExceptionOrdersBusinessRules : Finsoft.Rules.SharedBusinessRules
    {

        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Custom" and argument that matches "ApproveOrder".
        /// </summary>
        /// 

        //public static int? OrderHedID2;
        [Rule("r105")]
        public void r105Implementation(ApprovalExceptionOrdersModel instance)
        {
            
            //if (OrderHedID2 == null)
            //{
            //    OrderHedID2 = 0;
            //}

            // This is the placeholder for method implementation.
            if (instance.OrderHedID != null)
            {
                int ordStatus = 7;
                if (UserIsInRole("SalesManager"))
                {
                    //6 = Lv2 approval when order is exception
                    //7 = Approved when order is normal
                    ordStatus = instance.Exception == true ? 6 : 7;
                }
                else
                {
                    ordStatus = 7;
                }
                
                SqlText.ExecuteNonQuery("Update OrderHed Set OrderStatusID=@OrderStatusID Where OrderHedID=@OrderHedID ",
                    new { @OrderHedID = instance.OrderHedID, @OrderStatusID = ordStatus });

                SqlText.ExecuteNonQuery(
                    "Insert into OrderStatusLog (OrderHedID,UserID,[Comment],[RejectReason],[OtherRemarks],[TranDate]) Values " +
                    "(@OrderHedID,@UserID,@Comment,@RejectReason,@OtherRemarks,@TranDate) "
                    , new
                    {
                        @OrderHedID = instance.OrderHedID,
                        @UserID = UserId,
                        @Comment = "Order Approved",
                        @RejectReason = instance.RejectedReason,
                        @OtherRemarks = instance.RejectedRemarks
                        ,@TranDate=DateTime.Now
                    });


                if(ordStatus==6)
                {

                    //if (OrderHedID2 != instance.OrderHedID)
                    //{
                    //[ControllerAction("", "grid1", "Select", ActionPhase.Before)]
                    //protected void TellTheUserWhatJustHappened()
                    //{

                        //if (!IsTagged("UserIsInformed"))
                        //{
                        //    //Add Result MessageBox
                        //    //Result.ShowAlert("OrderNo : {0} Approved and Sent for Lv2 Approval!", instance.OrderHedID);
                        //    AddTag("UserIsInformed");
                        //    Result.ShowAlert("OrderNo : {0} Approved and Sent for Lv2 Approval!", instance.OrderHedID);
                        //}
                    //}
                    //Result.ShowAlert("OrderNo : {0} Approved and Sent for Lv2 Approval!", instance.OrderHedID);


                    //    OrderHedID2 = instance.OrderHedID;
                    //    //int? @OrderHedID2 = instance.OrderHedID;
                    //}
                }
                else
                {
                    

                    EpiRequest SORequest = new EpiRequest();
                    EpiResponse ObjEpiRes = SORequest.CreateSO((int)instance.OrderHedID);

                    SqlText.ExecuteNonQuery("Update OrderHed Set EpiOrderNum=@EpiOrderNum, EpiError=@EpiError Where OrderHedID=@OrderHedID ",
                    new { @OrderHedID = instance.OrderHedID, @EpiOrderNum = ObjEpiRes.EpiOrderNum, @EpiError=ObjEpiRes.ErrMsg });


                    //int effRec1 = SqlText.ExecuteNonQuery("Update OrderDtl Set BasePrice=ProposedBasePrice Where ChangeBasePrice=1 and OrderHedID=@OrderHedID",
                    //     instance.OrderHedID);

                    //int effRec2 = SqlText.ExecuteNonQuery("Update OrderDtl Set SellingPrice=ProposedSellingPrice Where ChangeSellingPrice=1 and OrderHedID=@OrderHedID",
                    //     instance.OrderHedID);
                    //int effRec3 = SqlText.ExecuteNonQuery("Update OrderDtl Set Amount=(OrderQty*SellingPrice) Where ChangeSellingPrice=1 and OrderHedID=@OrderHedID",
                    //     instance.OrderHedID);
                    //if (effRec2 > 0)
                    //{
                    //    decimal total = (decimal)SqlText.ExecuteScalar("select isnull(Sum(SellingPrice * OrderQty),0) From OrderDtl Where OrderHedID=@OrderHedID ",
                    //                instance.OrderHedID);
                    //    instance.TotalAmount = total;

                    //    int i = SqlText.ExecuteNonQuery("Update OrderHed Set TotalAmount=@total Where OrderHedID=@OrderHedID",
                    //        total, instance.OrderHedID);
                    //}

                    if(Arguments.SelectedValues.Count()>1)
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


                }
                
            }
            else
            {
                Result.ShowAlert("Please Select Row!");
            }
        }
    }
}
