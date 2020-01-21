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
	public partial class ApprovalBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Select".
        /// </summary>
        [Rule("r102")]
        public void r102Implementation(ApprovalModel instance)
        {
            // This is the placeholder for method implementation.
        }
    }
}
