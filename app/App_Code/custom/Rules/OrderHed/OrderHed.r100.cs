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
	public partial class OrderHedBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "New".
        /// </summary>
        [Rule("r100")]
        public void r100Implementation(OrderHedModel instance)
        {
            // This is the placeholder for method implementation.
            string CompQry = string.Format("select isnull(Count(a.CompanyName),0) from Company a inner join UsersCompany b on a.CompanyID=b.CompanyID inner join Users c on b.UserID=c.UserID Where c.UserName='{0}'", UserName);
            var CompCnt = (int)SqlText.ExecuteScalar(CompQry);
            if (CompCnt==1)
            {
                string GetCompQry = string.Format("select a.CompanyID,a.CompanyName from Company a inner join UsersCompany b on a.CompanyID=b.CompanyID inner join Users c on b.UserID=c.UserID Where c.UserName='{0}'", UserName);
                var CompInfo = SqlText.Execute(GetCompQry);
                if(CompInfo!=null)
                {
                    instance.CompanyID = (int)CompInfo[0];
                    instance.CompanyName = (string)CompInfo[1];
                }
                
            }                
            
            instance.OrderDate = DateTime.Now;
            instance.ReqShipDate = DateTime.Now;
            instance.BulkOrder = false;
            instance.OrderType = "Normal";
            instance.OrderStatusID = 1;
            instance.OrderStatusName = "Open";
        }
    }
}
