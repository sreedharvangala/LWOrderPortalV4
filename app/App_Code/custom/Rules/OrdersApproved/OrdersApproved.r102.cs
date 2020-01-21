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
	public partial class OrdersApprovedBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Select".
        /// </summary>
        [Rule("r102")]
        public void r102Implementation(OrdersApprovedModel instance)
        {
            // This is the placeholder for method implementation.
            Finsoft.Data.PageRequest page = (Finsoft.Data.PageRequest)Context.Items["PageRequest_Current"];
            if (page.View == "editForm1")
            {
                if (instance.OrderHedID != null)
                {
                    var custInfo = SqlText.Execute("select * from vwCustomer a inner join OrderHed b On a.SysRowID=b.CustSysRowID where b.OrderHedID=@OrderHedID", new { OrderHedID = instance.OrderHedID });

                    if (custInfo != null)
                    {
                        instance.CAddress1 = custInfo[5].ToString();
                        instance.CAddress2 = custInfo[6].ToString();
                        instance.CAddress3 = custInfo[7].ToString();
                        instance.CCity = custInfo[8].ToString();
                        instance.CState = custInfo[9].ToString();
                        instance.CZip = custInfo[10].ToString();
                        instance.CCountry = custInfo[11].ToString();
                        instance.CreditLimit = Convert.ToDecimal(custInfo[14]);
                        instance.Balance = Convert.ToDecimal(custInfo[13]);
                        instance.UnPaidInvoices = Convert.ToDecimal(custInfo[15]);
                        instance.OutStandingOrders = Convert.ToDecimal(custInfo[16]);
                        instance.UnPostedInvoices = Convert.ToDecimal(custInfo[17]);
                        instance.AgeCurr = Convert.ToDecimal(custInfo[18]);
                        instance.Age30 = Convert.ToDecimal(custInfo[19]);
                        instance.Age60 = Convert.ToDecimal(custInfo[20]);
                        instance.Age90 = Convert.ToDecimal(custInfo[21]);
                        instance.Age120 = Convert.ToDecimal(custInfo[22]);
                        instance.Age150 = Convert.ToDecimal(custInfo[23]);
                        instance.TermsExceed = Convert.ToBoolean(custInfo[24]);
                        instance.PhoneNum = custInfo[26].ToString();
                        instance.FaxNum = custInfo[27].ToString();
                    }
                }

                if (instance.OrderHedID != null)
                {
                    var shipInfo = SqlText.Execute("select * from vwShipTo a inner join OrderHed b On a.SysRowID=b.ShipToSysRowID where b.OrderHedID=@OrderHedID", new { OrderHedID = instance.OrderHedID });
                    if (shipInfo != null)
                    {
                        instance.SAddress1 = shipInfo[6].ToString();
                        instance.SAddress2 = shipInfo[7].ToString();
                        instance.SAddress3 = shipInfo[8].ToString();
                        instance.SCity = shipInfo[9].ToString();
                        instance.SState = shipInfo[10].ToString();
                        instance.SZip = shipInfo[11].ToString();
                        instance.SCountry = shipInfo[12].ToString();
                        instance.SPhoneNum = shipInfo[14].ToString();
                        instance.SFaxNum = shipInfo[15].ToString();
                    }
                }
            }
            
        }
    }
}
