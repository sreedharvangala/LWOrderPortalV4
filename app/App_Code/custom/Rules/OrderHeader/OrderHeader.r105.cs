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
	public partial class OrderHeaderBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view before an action
        /// with a command name that matches "Custom" and argument that matches "SubmitForApproval".
        /// </summary>
        [Rule("r105")]
        public void r105Implementation(OrderHeaderModel instance)
        {
            // This is the placeholder for method implementation.
            
            if (instance.OrderHedID != null)
            {
                if (instance.OrderDate != null)
                    instance.TargetReportingDate = Convert.ToDateTime(instance.OrderDate).AddDays(7);

                //Atleast One OrderDtl Line should be there else dont submit
                int LnCnt = (int)SqlText.ExecuteScalar("Select Count(OrderdtlID) from OrderDtl Where OrderHedID=@OrderHedID"
                            , instance.OrderHedID);
                if (LnCnt <= 0)
                {
                    Result.ShowAlert("Requrie minimum one OrderDtl Line to Submit for Approval.");
                    return;
                }

                //OrderQty should match with Sum(OrderRelQty) else dont Submit
                int UnMatchQtyCnt = (int)SqlText.ExecuteScalar("Select Count(UnMatchQty) as UnMatchQtyCnt from ( select OrderDtlID,a.OrderQty,isnull((Select Sum(x.OrderRelQty) from OrderRel x Where x.OrderDtlID=a.OrderDtlID),0) as TotalRelQty,(a.OrderQty - isnull((Select Sum(x.OrderRelQty) from OrderRel x Where x.OrderDtlID=a.OrderDtlID),0)) as UnMatchQty  from OrderDtl a where  a.OrderHedID=@OrderHedID ) as Y Where Y.UnMatchQty <> 0 "
                            , instance.OrderHedID);
                if (UnMatchQtyCnt > 0)
                {
                    Result.ShowAlert("OrderQty is not matching OrderRel Qty.");
                    return;
                }
                //OrderFOCQty should match with Sum(OrderRelFOCQty) else dont Submit
                int UnMatchFOCQtyCnt = (int)SqlText.ExecuteScalar("Select Count(UnMatchQty) as UnMatchQtyCnt from ( select OrderDtlID,a.Focqty,isnull((Select Sum(x.FocrelQty) from OrderRel x Where x.OrderDtlID=a.OrderDtlID),0) as TotalRelQty,(a.Focqty - isnull((Select Sum(x.FocrelQty) from OrderRel x Where x.OrderDtlID=a.OrderDtlID),0)) as UnMatchQty  from OrderDtl a where  a.OrderHedID=@OrderHedID ) as Y Where Y.UnMatchQty <> 0 "
                            , instance.OrderHedID);
                if (UnMatchFOCQtyCnt > 0)
                {
                    Result.ShowAlert("Order FOCQty is not matching OrderRel FOCQty.");
                    return;
                }

                instance.Exception = false;
                instance.ExceptionDtl = string.Empty;
                //1. Credit Limit Exception
                int creditEx=(int)SqlText.ExecuteScalar("select isnull(Case When a.UnPaidInvoices > a.CreditLimit2 Then 1 Else 0 End,0) From vwCustomer a inner join OrderHed b on a.SysRowID=b.CustSysRowID Where b.OrderHedID=@OrderHedID"
                            , instance.OrderHedID);
                if(creditEx>0)
                {
                    instance.Exception = true;
                    instance.ExceptionDtl = "CreditLimit";
                    //int i = SqlText.ExecuteNonQuery("Insert into OrderException values (@OrderHedID,@ExCodeID)", instance.OrderHedID, creditEx);
                }
                //2. Terms Exception
                int termsEx = (int)SqlText.ExecuteScalar("select isnull(Case When a.TermsExceed=1 Then 2 Else 0 End,0) From vwCustomer a inner join OrderHed b on a.SysRowID=b.CustSysRowID Where b.OrderHedID=@OrderHedID"
                            , instance.OrderHedID);
                if (termsEx > 0)
                {
                    instance.Exception = true;
                    instance.ExceptionDtl += string.IsNullOrEmpty(instance.ExceptionDtl)?"Terms":", Terms";
                    //int i=SqlText.ExecuteNonQuery("Insert into OrderException values (@OrderHedID,@ExCodeID)", instance.OrderHedID, termsEx);
                }
                //3. BasePrice Exception
                int basePriceEx = (int)SqlText.ExecuteScalar("select isnull(Case When Count(OrderDtlID)>0 Then 3 Else 0 End,0) from OrderDtl where ProposedBasePrice < BasePrice and OrderHedID=@OrderHedID"
                            , instance.OrderHedID);
                if (basePriceEx > 0)
                {
                    instance.Exception = true;
                    instance.ExceptionDtl += string.IsNullOrEmpty(instance.ExceptionDtl) ? "BasePrice" : ", BasePrice";
                    //int i = SqlText.ExecuteNonQuery("Insert into OrderException values (@OrderHedID,@ExCodeID)", instance.OrderHedID, basePriceEx);
                }
                //4. SellingPrice Exception
                int sellingPriceEx = (int)SqlText.ExecuteScalar("select isnull(Case When Count(OrderDtlID)>0 Then 4 Else 0 End,0) from OrderDtl where ProposedSellingPrice < SellingPrice and OrderHedID=@OrderHedID"
                            , instance.OrderHedID);
                if (sellingPriceEx > 0)
                {
                    instance.Exception = true;
                    instance.ExceptionDtl += string.IsNullOrEmpty(instance.ExceptionDtl) ? "SellingPrice" : ", SellingPrice";
                    //int i = SqlText.ExecuteNonQuery("Insert into OrderException values (@OrderHedID,@ExCodeID)", instance.OrderHedID, sellingPriceEx);
                }

                //5. FOC Qty Exception
                int focQtyEx = (int)SqlText.ExecuteScalar("select isnull(Case When Count(OrderDtlID)>0 Then 5 Else 0 End,0) from OrderDtl where FOCQty>0 and OrderHedID=@OrderHedID"
                            , instance.OrderHedID);
                if (focQtyEx > 0)
                {
                    instance.Exception = true;
                    instance.ExceptionDtl += string.IsNullOrEmpty(instance.ExceptionDtl) ? "FOCQty" : ", FOCQty";
                    //int i = SqlText.ExecuteNonQuery("Insert into OrderException values (@OrderHedID,@ExCodeID)", instance.OrderHedID, sellingPriceEx);
                }
                //6. FOC Part Item Exception
                int focPartItemEx = (int)SqlText.ExecuteScalar("select isnull(Case When Count(OrderDtlID)>0 Then 6 Else 0 End,0) from OrderDtl where (BasePrice<=1 Or SellingPrice <=1) and OrderHedID=@OrderHedID"
                            , instance.OrderHedID);
                if (focPartItemEx > 0)
                {
                    //instance.Exception = true;
                    instance.ExceptionDtl += string.IsNullOrEmpty(instance.ExceptionDtl) ? "FOCPartItem" : ", FOCPartItem";
                    
                }
                //Approval
                //2 - Waiting For Price Change , 5 - Waiting For Lv1 Approval

                int ordStatus = (basePriceEx>0 || sellingPriceEx>0 || focQtyEx > 0 || focPartItemEx > 0) ? 2 : 5;
                 string comm= (basePriceEx > 0 || sellingPriceEx > 0 || focQtyEx > 0 || focPartItemEx >0) ? "Submitted For Price Change" : "Submitted For Approval";

                SqlText.ExecuteNonQuery("Update OrderHed Set Exception=@Exception,ExceptionDtl=@ExceptionDtl,OrderStatusID=@OrderStatusID Where OrderHedID=@OrderHedID ",
                    new { instance.Exception,instance.ExceptionDtl,@OrderHedID = instance.OrderHedID, @OrderStatusID = ordStatus });

                SqlText.ExecuteNonQuery(
                    "Insert into OrderStatusLog (OrderHedID,UserID,[Comment],[RejectReason],[OtherRemarks],[TranDate]) Values " +
                    "(@OrderHedID,@UserID,@Comment,@RejectReason,@OtherRemarks,@TranDate) "
                    , new
                    {
                        @OrderHedID = instance.OrderHedID,
                        @UserID = UserId,
                        @Comment = comm,
                        @RejectReason = "",
                        @OtherRemarks = "",
                        @TranDate = DateTime.Now
                    });

                Result.ShowAlert("OrderNo : {0} {1}",instance.OrderHedID, comm);
            }
            else
            {
                Result.ShowAlert("Please Select Row!");
            }

            Result.Refresh();
        }
    }
}
