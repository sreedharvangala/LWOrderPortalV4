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
        /// with a command name that matches "Edit".
        /// </summary>
        [Rule("r108")]
        public void r108Implementation(OrderDtlModel instance)
        {
            // This is the placeholder for method implementation.
            //if (UserIsInRole("SalesPerson"))
            //{
            //    instance.OTFOC = "FOC";
            //}
            //else if (UserIsInRole("SalesClerk"))
            //{
            //    instance.OTFOC = "99";
            //}

        }
    }
}
