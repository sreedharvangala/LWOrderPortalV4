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
        /// with a command name that matches "Execute".
        /// </summary>
        [Rule("r107")]
        public void r107Implementation(OrderDtlModel instance)
        {
            // This is the placeholder for method implementation.
            //if(UserIsInRole("SalesClerk"))
            //{
            //    instance.OTEACH = "EACH";
            //    instance.OTFOC = "99";
            //}

            //if(UserIsInRole("SalesPerson"))
            //{
            //    instance.OTFOC = "FOC";
            //}
            

        }
    }
}
