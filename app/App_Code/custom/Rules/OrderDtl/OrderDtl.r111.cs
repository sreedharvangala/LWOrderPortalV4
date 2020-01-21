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

        /// <summary>This method will execute in any view before an action
        /// with a command name that matches "Update" and argument that matches "Save".
        /// </summary>
        [Rule("r111")]
        public void r111Implementation(OrderDtlModel instance)
        {

            if (instance.OrderQty == null)
            {
                instance.OrderQty = 0;
            }

            if (instance.Focqty == null)
            {
                instance.Focqty = 0;
            }
           

            // This is the placeholder for method implementation.
            if (instance.OrderQty <= 0 && instance.Focqty <= 0)
            {
                PreventDefault();
                Result.Focus("OrderQty", "FocQty or OrderQty must be greater than 0");
            }

  
         }
    }
}
