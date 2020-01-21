<%@ WebHandler Language="C#" Class="Finsoft.CustomReport" %>

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Xml.XPath;
using System.Drawing.Drawing2D;
using Newtonsoft.Json.Linq;
using Finsoft.Data;
using Finsoft.Services;
using Microsoft.Reporting.WebForms;
using ReportLibrary;

using System.Collections;

namespace Finsoft
{


    public class CustomReport : GenericHandlerBase, IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        //public byte[] ReportData { get; set; }
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                ServerReportExecute report = new ServerReportExecute();
                ArrayList arrLstDefaultParam = new ArrayList();
                arrLstDefaultParam.Add(CreateReportParameter("Company", context.Request.QueryString["Company"]));
                arrLstDefaultParam.Add(CreateReportParameter("CustId", context.Request.QueryString["CustId"]));
                arrLstDefaultParam.Add(CreateReportParameter("SalesRepCode", null));
                arrLstDefaultParam.Add(CreateReportParameter("GroupCode", null));
                arrLstDefaultParam.Add(CreateReportParameter("CutOffDate", DateTime.Now.ToString("yyyy-MM-dd")));
                report.ReportParams = arrLstDefaultParam;

                byte[] reportData = report.PrintReport();

                context.Response.Clear();
                context.Response.ContentType = "application/pdf";
                context.Response.AddHeader("Content-Length", reportData.Length.ToString());
                AppendDownloadTokenCookie();
                var fileName = string.Format("{0}.pdf", context.Request.QueryString["FileName"]);

                context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));
                context.Response.OutputStream.Write(reportData, 0, reportData.Length);
                //context.Response.BinaryWrite(reportData);
                //context.Response.Flush();
                //context.Response.Close();
            }
            catch(Exception ex)
            {

            }
        }
        private ReportParameter CreateReportParameter(string paramName, string pramValue)
        {
            ReportParameter aParam = new ReportParameter(paramName, pramValue);
            return aParam;
        }
    }

}
