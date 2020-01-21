using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Finsoft.Data;
using Finsoft.Models;
using System.IO;
using System.Net;

namespace Finsoft.Rules
{
	public partial class OrderHeaderBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view after an action
        /// with a command name that matches "Calculate" and argument that matches "CustSysRowID".
        /// </summary>
        [Rule("r101")]
        public void r101Implementation(OrderHeaderModel instance)
        {
            // This is the placeholder for method implementation.
            if(instance.OrderHedID!=null)
            {
                int i = (int)SqlText.ExecuteScalar("Select isnull(Count(OrderDtlID),0) from OrderDtl Where OrderHedID=@OrderHedID", instance.OrderHedID);
                if (i > 0)
                {
                    instance.IsOrderLinesCreated = true;
                }
                else
                {
                    instance.IsOrderLinesCreated = false;
                }

                
                
            }

            if(instance.OrderDate!=null)
                instance.TargetReportingDate = Convert.ToDateTime(instance.OrderDate).AddDays(7);

            if (instance.Owner == "YS,LAI")
            {
                instance.Owner = "YS.LAI";
            }
            //if (instance[OrderHeaderDataField.UnPaidInvoices].Modified ||
            //    instance[OrderHeaderDataField.CreditLimit].Modified )
            //{
            //    //Result.ShowModal("CustomerStatement", "editForm1", "Select", null);
            //    if(instance.UnPaidInvoices > instance.CreditLimit)
            //    {
            //        instance.Exception = true;
            //        instance.ExceptionDtl = "#Credit Limit Exceeded";
            //    }
            //}

            //if (instance[OrderHeaderDataField.TermsExceed].Modified)
            //{
            //    //Result.ShowModal("CustomerStatement", "editForm1", "Select", null);
            //    if(instance.TermsExceed !=null && Convert.ToBoolean(instance.TermsExceed))
            //    {
            //        instance.Exception = true;
            //        instance.ExceptionDtl += " #Terms Exceeded";
            //    }
            //}
            //string filePath = @"D:\OldPC\Epicor\Epicor Docs\ConsultantRoadmap.pdf";
            ////FileInfo file = new FileInfo(filePath);
            //WebClient test = new WebClient();
            //byte[] data= test.DownloadData(filePath);
            //instance.ContentType = "application/pdf";
            //instance.FileName = "ConsultantRoadmap.pdf";
            //instance.Length = 489818;
            //UpdateFieldValue("Attachment", filePath);
        }
    }
}
