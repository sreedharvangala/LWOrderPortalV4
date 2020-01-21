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
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Select".
        /// </summary>
        [Rule("r106")]
        public void r106Implementation(OrderHeaderModel instance)
        {
            // This is the placeholder for method implementation.
            //int i = (int)SqlText.ExecuteScalar("Select isnull(Count(OrderDtlID),0) from OrderDtl Where OrderHedID=@OrderHedID", instance.OrderHedID);
            //if (i > 0)
            //{
            //    NodeSet().SelectViews("grid1", "editForm1").SelectDataFields("CustSysRowID").SetTextMode("Static");
            //    NodeSet().SelectViews("grid1", "editForm1").SelectDataFields("ShipToSysRowID").SetTextMode("Static");
            //}
            //else
            //{
            //    //NodeSet().SelectViews("grid1", "editForm1").SelectDataFields("CustSysRowID").SetTextMode("Auto");
            //    //NodeSet().SelectViews("grid1", "editForm1").SelectDataFields("ShipToSysRowID").SetTextMode("Auto");
            //}
        }
    }
}
