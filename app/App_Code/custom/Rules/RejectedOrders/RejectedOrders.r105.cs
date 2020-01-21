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
	public partial class RejectedOrdersBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Custom" and argument that matches "ApproveOrder".
        /// </summary>
        [Rule("r105")]
        public void r105Implementation(RejectedOrdersModel instance)
        {
            // This is the placeholder for method implementation.
        }
    }
}
