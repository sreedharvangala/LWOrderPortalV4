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
	public partial class ApprovedOrdersBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Custom" and argument that matches "RejectOrder".
        /// </summary>
        [Rule("r106")]
        public void r106Implementation(ApprovedOrdersModel instance)
        {
            // This is the placeholder for method implementation.
        }
    }
}
