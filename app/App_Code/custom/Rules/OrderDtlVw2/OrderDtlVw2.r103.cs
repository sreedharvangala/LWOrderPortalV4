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
	public partial class OrderDtlVw2BusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Insert".
        /// </summary>
        [Rule("r103")]
        public void r103Implementation(OrderDtlVw2Model instance)
        {
            // This is the placeholder for method implementation.
        }
    }
}
