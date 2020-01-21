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
        /// with a command name that matches "Insert|Update".
        /// </summary>
        [Rule("r105")]
        public void r105Implementation(OrderDtlModel instance)
        {
            // This is the placeholder for method implementation.
            if (instance.OrderQty <= 0 && instance.Focqty<=0)
            {
                PreventDefault();
                Result.Focus("OrderQty", "OrderQty Or FOCQty must be greater than 0");
            }

            if (Arguments.CommandName.ToLower()=="insert")
            {

            }
            else if (Arguments.CommandName.ToLower()=="update")
            {
                if(!(bool)instance.OrderHedBulkOrder)
                {
                    if (instance[OrderDtlDataField.OrderQty].Modified)
                    {
                        int i = SqlText.ExecuteNonQuery("UPDATE OrderRel Set OrderRelQty=@OrderQty WHERE OrderDtlID=@OrderDtlID and Seq=1"
                        , instance.OrderQty, instance.OrderDtlID);
                    }

                    if (instance[OrderDtlDataField.Focqty].Modified)
                    {
                        int i = SqlText.ExecuteNonQuery("UPDATE OrderRel Set  FOCRelQty=@Focqty WHERE OrderDtlID=@OrderDtlID and Seq=1"
                        , instance.Focqty, instance.OrderDtlID);
                    }
                }                

            }
            else
            {
                throw new Exception("Command Argument is Wrong!");
            }
            
            //string exDtl = string.Empty;

            //if (instance.ProposedBasePrice < instance.BasePrice)
            //{
            //     exDtl += string.Format(" #Proposed BasePrice Changed for \"{0}\" ", instance.PartNum);
                
            //}

            //if(instance.ProposedSellingPrice < instance.SellingPrice)
            //{
            //     exDtl += string.Format(" #Propsed SellingPrice Changed for \"{0}\" ", instance.PartNum);
                
            //}

            //if(!string.IsNullOrEmpty(exDtl))
            //{
            //    int i = SqlText.ExecuteNonQuery("UPDATE OrderHed SET Exception = 1 " +
            //        ",ExceptionDtl = @exDtl " +
            //        " Where OrderHedID=@OrderHedID "
            //        , new { @OrderHedID = instance.OrderHedID, @exDtl = exDtl });
            //}
            

        }
    }
}
