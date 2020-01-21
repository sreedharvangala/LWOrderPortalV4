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
	public partial class OrderStatusLogV2Lv1BusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "New".
        /// </summary>
        [Rule("r100")]
        public void r100Implementation(OrderStatusLogV2Lv1Model instance)
        {
            // This is the placeholder for method implementation.
        }
    }
}
