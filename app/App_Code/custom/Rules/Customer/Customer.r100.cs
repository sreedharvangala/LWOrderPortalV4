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
	public partial class CustomerBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Report" and argument that matches "blank".
        /// </summary>
        [Rule("r100")]
        public void r100Implementation(CustomerModel instance)
        {
            // This is the placeholder for method implementation.
            // Redirect user to another URL
            Result.NavigateUrl = String.Format(
                "~/Pages/Customer.html?CustID={0}&_controller=Customer" +
                "&_commandName=Select&_commandArgument=v100",
                instance.CustID);
        }
    }
}
