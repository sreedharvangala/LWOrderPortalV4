using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Finsoft.Data;
using Finsoft.Models;
using ReportLibrary;
using System.Collections;
using Microsoft.Reporting.WebForms;
using System.IO;
using Finsoft.Handlers;
using Finsoft.Services;
using System.Text;
using System.Web.UI;

namespace Finsoft.Rules
{
	public partial class OrderHeaderBusinessRules : Finsoft.Rules.SharedBusinessRules
    {

        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Report" and argument that matches "_blank".
        /// </summary>
        [Rule("r109")]
        public void r109Implementation(OrderHeaderModel instance)
        {
            // This is the placeholder for method implementation.
            //Result.NavigateUrl = String.Format(
            //    "~/Pages/CustomerReport.html?CustId={0}&_controller=CustomerReport" +
            //    "&_commandName=Select&_commandArgument=SSRS",
            //    "C1000034");

            try
            {
                //if (instance.OrderHedID==null)
                //{
                //    Result.ShowAlert("Please Select Order!");
                //}
                if(string.IsNullOrEmpty(instance.CompanyName))
                {
                    Result.ShowAlert("Company is Empty!");
                }
                else if(string.IsNullOrEmpty(instance.CustID))
                {
                    Result.ShowAlert("Customer is Empty!");
                }
                else
                {
                    string custid = string.Format("{0},{1}", instance.CustID, instance.CustID);
                    Result.NavigateUrl = string.Format("~/CustomReport.ashx?Company={0}&CustId={1}&FileName={2}", instance.CompanyName, custid, instance.CustID);
                }

                

                //string dt = DateTime.Now.ToString("yyyy-MM-dd");
                //string fileLoc = string.Format(@"D:\OldPC\Epicor\Customers Docs\Lube World\Git\{0}.pdf", instance.CustID);
                //ServerReportExecute report = new ServerReportExecute();
                //report.userName = userName;
                //report.passWord = passWord;
                //report.domainName = domainName;
                //report.reportPath = "";
                //report.url = reportUrl;
                //report.file = fileLoc;


                //ArrayList arrLstDefaultParam = new ArrayList();
                //arrLstDefaultParam.Add(CreateReportParameter("Company", instance.CompanyName));
                //arrLstDefaultParam.Add(CreateReportParameter("CustId", custid));
                //arrLstDefaultParam.Add(CreateReportParameter("SalesRepCode", null));
                //arrLstDefaultParam.Add(CreateReportParameter("GroupCode", null));
                //arrLstDefaultParam.Add(CreateReportParameter("CutOffDate", dt));
                //report.ReportParams = arrLstDefaultParam;

                //byte[] reportData = report.PrintReport();
                //var reportHandler = ((CustomReport)(ApplicationServices.CreateInstance("Finsoft.Handlers.CustomReport")));
                //creport.ReportData = reportData;
                //creport.ProcessRequest(this.Context);
                //reportHandler.ProcessRequest(HttpContext.Current);
                //StringBuilder sb = new StringBuilder();

                //sb.Append("<script type = 'text/javascript'>");

                //sb.Append("window.open('");

                //sb.Append(url);

                //sb.Append("');");

                //sb.Append("</script>");
                //this.Context.page
                //Page.ClientScript.RegisterStartupScript(GetType(), "Redirect", string.Format("window.location.replace(\'{0}?_link={1}\');", permalink[0], HttpUtility.UrlEncode(link)), true);
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(),

                //        "script", sb.ToString());
                //this.Context.Response.Redirect(string.Format("~/CustomReport.ashx?Company={0}&CustId={1}", instance.CompanyName,custid), false);

                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.ContentType = "application/pdf";
                //HttpContext.Current.Response.AddHeader("Content-Length", reportData.Length.ToString());
                //AppendDownloadTokenCookie();
                //var fileName = "Filename.pdf";

                //HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));
                //HttpContext.Current.Response.OutputStream.Write(reportData, 0, reportData.Length);


                //FileInfo file = new FileInfo(filePath);
                //Context.Response.Clear();
                //Context.Response.ContentType = "application/pdf";
                ////Context.Response.AddHeader("Content-Disposition",
                ////    String.Format("attachment;filename={0}.pdf", instance.CustID));
                ////byte[] reportData =
                ////    File.ReadAllBytes(@"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg");
                //Context.Response.AddHeader("Content-Length", reportData.Length.ToString());
                ////Context.Response.OutputStream.Write(reportData, 0, reportData.Length);
                //Context.Response.BinaryWrite(reportData);

                //var cache = Context.Response.Cache;
                //cache.SetCacheability(HttpCacheability.Public);
                //cache.SetOmitVaryStar(true);
                //cache.SetExpires(DateTime.Now.AddDays(365));
                //cache.SetValidUntilExpires(true);
                //cache.SetLastModifiedFromFileDependencies();
                //Context.Response.ContentType = "application/pdf";
                //Context.Response.AddHeader("Content-Length", Convert.ToString(reportData.Length));
                //Context.Response.AddHeader("File-Name", "Test");
                //Context.Response.BinaryWrite(reportData);
                //Context.Response.OutputStream.Flush();
            }
            catch (Exception ex)
            {

            }

        }
        protected virtual void AppendDownloadTokenCookie()
        {
            var context = HttpContext.Current;
            var downloadToken = "APPFACTORYDOWNLOADTOKEN";
            var tokenCookie = context.Request.Cookies[downloadToken];
            if (tokenCookie != null)
            {
                tokenCookie.Value = string.Format("{0},{1}", tokenCookie.Value, Guid.NewGuid());
                context.Response.AppendCookie(tokenCookie);
            }
        }
        private ReportParameter CreateReportParameter(string paramName, string pramValue)
        {
            ReportParameter aParam = new ReportParameter(paramName, pramValue);
            return aParam;
        }
    }
}
