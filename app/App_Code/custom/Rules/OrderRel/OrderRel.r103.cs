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
        
        /// <summary>This method will execute in any view before an action
        /// with a command name that matches "Insert".
        /// </summary>
        [Rule("r103")]
        public void r103Implementation(OrderRelModel instance)
        {
            // This is the placeholder for method implementation.
            
                if(instance.OrderRelQty<=0 && instance.FocrelQty<=0)
                {
                    
                    //
                    //instance.OrderRelQty = Convert.ToDecimal(instance[OrderRelDataField.OrderRelQty].OldValue);
                    this.PreventDefault();
                    Result.Focus("OrderRelQty", "The OrderRelQty Or FOCRelQty must be greater than zero.");
                }
                if(instance.OrderRelQty > 0)
                {
                    decimal RemainRelQty = (decimal)SqlText.ExecuteScalar("select a.OrderQty - isnull((Select Sum(b.OrderRelQty) from OrderRel b Where b.OrderDtlID=a.OrderDtlID),0)  as RemainRelQty from OrderDtl a where a.OrderDtlID=@OrderDtlID ", new { @OrderDtlID = instance.OrderDtlID });
                    if(RemainRelQty<=0)
                    {
                        this.PreventDefault();
                        Result.Focus("OrderRelQty", "Available OrderRelQty is {0}", RemainRelQty);
                    }
                    else if (instance.OrderRelQty > RemainRelQty)
                    {
                        this.PreventDefault();
                        Result.Focus("OrderRelQty", "RelQty must be between 1 and {0}", RemainRelQty);
                    }
                    
                }
                if(instance.FocrelQty>0)
                {
                    decimal RemainFocQty = (decimal)SqlText.ExecuteScalar("select a.FOCQty - isnull((Select Sum(b.FOCRelQty) from OrderRel b Where b.OrderDtlID=a.OrderDtlID ),0)  as RemainRelQty from OrderDtl a where a.OrderDtlID=@OrderDtlID ", new { @OrderDtlID = instance.OrderDtlID });
                    if (RemainFocQty <= 0)
                    {
                        this.PreventDefault();
                        Result.Focus("FocrelQty", "Available FOCQty is {0}", RemainFocQty);
                    }
                    else if (instance.FocrelQty > RemainFocQty)
                    {
                        this.PreventDefault();
                        Result.Focus("FocrelQty", "FOCQty must be between 1 and {0}", RemainFocQty);
                    }
                }
            if(instance.Seq==null)
            {
                int i = (int)SqlText.ExecuteScalar("Select Count(OrderRelID)+1  from OrderRel Where OrderDtlID=@OrderDtlID ", new { @OrderDtlID = instance.OrderDtlID });

                instance.Seq = i;
            }

           
            
        }
    }
}
