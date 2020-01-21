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
        /// with a command name that matches "New".
        /// </summary>
        [Rule("r105")]
        public void r105Implementation(OrderRelModel instance)
        {
            // This is the placeholder for method implementation.
            decimal RemainRelQty = (decimal)SqlText.ExecuteScalar("select a.OrderQty - (Select Sum(isnull(b.OrderRelQty,0)) from OrderRel b Where b.OrderDtlID=a.OrderDtlID)  as RemainRelQty from OrderDtl a where a.OrderDtlID=@OrderDtlID ", new { @OrderDtlID = instance.OrderDtlID });
            decimal RemainFocQty = (decimal)SqlText.ExecuteScalar("select a.FOCQty - isnull((Select Sum(b.FOCRelQty) from OrderRel b Where b.OrderDtlID=a.OrderDtlID ),0)  as RemainRelQty from OrderDtl a where a.OrderDtlID=@OrderDtlID ", new { @OrderDtlID = instance.OrderDtlID });
            //if (RemainRelQty <= 0)
            //{
            //    PreventDefault();
            //throw new Exception(string.Format("Available OrderRelQty is {0}", RemainRelQty));
            instance.OrderRelQty = RemainRelQty;
            instance.FocrelQty = RemainFocQty;
            Result.ShowMessage("Available OrderRelQty is {0} And FOCRelQty is {1}", RemainRelQty,RemainFocQty);   
            //}
        }
    }
}
