﻿using System;
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
        /// with a command name that matches "New".
        /// </summary>
        [Rule("r100")]
        public void r100Implementation(OrderDtlModel instance)
        {
            // This is the placeholder for method implementation.
            //var ordhedid = instance.OrderHedID;

            //int i = (int)SqlText.ExecuteScalar("Select Count(OrderHedID) from OrderHed where OrderHedID=@OrderHedID and BulkOrder=1 "
            //                , new { @OrderHedID = ordhedid });
            //if (i >= 1)
            //{
            //    NodeSet().SelectViews("editForm1", "createForm1")
            //        .SelectDataFields("OrderQty")
            //        .SetTextMode("Static");
            //}
        }
    }
}
