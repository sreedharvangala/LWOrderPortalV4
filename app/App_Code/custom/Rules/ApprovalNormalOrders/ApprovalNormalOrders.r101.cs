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
	public partial class ApprovalNormalOrdersBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view after an action
        /// with a command name that matches "Calculate" and argument that matches "CustSysRowID".
        /// </summary>
        [Rule("r101")]
        public void r101Implementation(ApprovalNormalOrdersModel instance)
        {
            // This is the placeholder for method implementation.
        }
    }
}
