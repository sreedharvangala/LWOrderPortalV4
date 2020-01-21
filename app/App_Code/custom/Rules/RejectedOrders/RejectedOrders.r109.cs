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
        /// with a command name that matches "Custom" and argument that matches "CustStReport".
        /// </summary>
        [Rule("r109")]
        public void r109Implementation(RejectedOrdersModel instance)
        {
            // This is the placeholder for method implementation.
            if (instance.OrderHedID == null)
            {
                Result.ShowAlert("Please Select Order!");
            }
            else if (string.IsNullOrEmpty(instance.CompanyName))
            {
                Result.ShowAlert("Company is Empty!");
            }
            else if (string.IsNullOrEmpty(instance.CustID))
            {
                Result.ShowAlert("Customer is Empty!");
            }
            else
            {
                string custid = string.Format("{0},{1}", instance.CustID, instance.CustID);
                Result.NavigateUrl = string.Format("~/CustomReport.ashx?Company={0}&CustId={1}&FileName={2}", instance.CompanyName, custid, instance.CustID);
            }
        }
    }
}
