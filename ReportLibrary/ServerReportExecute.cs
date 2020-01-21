//using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.Reporting.WebForms;
//using Microsoft.Reporting.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportLibrary
{
    public class ServerReportExecute
    {
        string userName = ConfigurationManager.AppSettings["UserName"].ToString();
        string passWord = ConfigurationManager.AppSettings["Password"].ToString();
        string domainName = ConfigurationManager.AppSettings["DomainName"].ToString();
        string reportUrl = ConfigurationManager.AppSettings["ReportServerURL"].ToString();
        string reportPath= ConfigurationManager.AppSettings["ReportPath"].ToString();
        ///public string reportPath { get; set; }
        public ArrayList ReportParams { get; set; }
        public string file { get; set; }
        //public System.Net.NetworkCredential NetworkCredentials
        //{
        //    get
        //    {
        //        return new System.Net.NetworkCredential(userName, passWord, domainName);
        //    }
        //}

        public byte[] PrintReport()
        {
            try
            {
                ReportViewer rptViewer = new ReportViewer();
                string urlReportServer = reportUrl;
                rptViewer.ProcessingMode = ProcessingMode.Remote; // ProcessingMode will be Either Remote or Local
                rptViewer.ServerReport.ReportServerUrl = new Uri(urlReportServer); //Set the ReportServer Url
                rptViewer.ServerReport.ReportPath = reportPath; //Passing the Report Path                

                //Creating an ArrayList for combine the Parameters which will be passed into SSRS Report
                ArrayList reportParam = new ArrayList();
                reportParam = ReportParams;//ReportDefaultPatam();

                ReportParameter[] param = new ReportParameter[reportParam.Count];
                for (int k = 0; k < reportParam.Count; k++)
                {
                    param[k] = (ReportParameter)reportParam[k];
                }
                // pass crendentitilas

                //rptViewer.Credentials = NetworkCredentials;
                //IReportServerCredentials rsCredentials = rptViewer.ServerReport.ReportServerCredentials;


                // Set the credentials for the server report  
                //rsCredentials.NetworkCredentials = new System.Net.NetworkCredential(userName, passWord, domainName);

                IReportServerCredentials irsc = new CustomReportCredentials(userName, passWord, domainName);


                rptViewer.ServerReport.ReportServerCredentials = irsc;

                //pass parmeters to report
                rptViewer.ServerReport.SetParameters(param); //Set Report Parameters
                //rptViewer.ServerReport.Refresh();

                string SaveLocation = file;
                string mimeType;
                string encoding;
                string extension;
                string[] streams;
                Warning[] warnings;
                byte[] Bytes = rptViewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streams, out warnings);
                rptViewer.ShowExportControls = true;
               
                return Bytes;
                //if (File.Exists(SaveLocation))
                //{
                //    System.IO.File.Delete(SaveLocation);
                //}

                //System.IO.FileStream fs = new System.IO.FileStream(SaveLocation, System.IO.FileMode.Create);
                //fs.Write(Bytes, 0, Bytes.Length);
                //fs.Close();
                //fs.Dispose();

                //return SaveLocation;

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //this.rptViewer.RefreshReport();
        }
        public ReportViewer GetReportViewer()
        {
            try
            {
                ReportViewer rptViewer = new ReportViewer();
                string urlReportServer = reportUrl;
                rptViewer.ProcessingMode = ProcessingMode.Remote; // ProcessingMode will be Either Remote or Local
                rptViewer.ServerReport.ReportServerUrl = new Uri(urlReportServer); //Set the ReportServer Url
                rptViewer.ServerReport.ReportPath = reportPath; //Passing the Report Path                

                //Creating an ArrayList for combine the Parameters which will be passed into SSRS Report
                ArrayList reportParam = new ArrayList();
                reportParam = ReportParams;//ReportDefaultPatam();

                ReportParameter[] param = new ReportParameter[reportParam.Count];
                for (int k = 0; k < reportParam.Count; k++)
                {
                    param[k] = (ReportParameter)reportParam[k];
                }
                // pass crendentitilas

                //rptViewer.Credentials = NetworkCredentials;
                //IReportServerCredentials rsCredentials = rptViewer.ServerReport.ReportServerCredentials;


                // Set the credentials for the server report  
                //rsCredentials.NetworkCredentials = new System.Net.NetworkCredential(userName, passWord, domainName);

                IReportServerCredentials irsc = new CustomReportCredentials(userName, passWord, domainName);


                rptViewer.ServerReport.ReportServerCredentials = irsc;

                //pass parmeters to report
                rptViewer.ServerReport.SetParameters(param); //Set Report Parameters
                //rptViewer.ServerReport.Refresh();

                //string SaveLocation = file;
                //string mimeType;
                //string encoding;
                //string extension;
                //string[] streams;
                //Warning[] warnings;
                //byte[] Bytes = rptViewer.ServerReport.Render("PDF", null, out mimeType, out encoding, out extension, out streams, out warnings);
                rptViewer.ShowExportControls = true;

                return rptViewer;
                //if (File.Exists(SaveLocation))
                //{
                //    System.IO.File.Delete(SaveLocation);
                //}

                //System.IO.FileStream fs = new System.IO.FileStream(SaveLocation, System.IO.FileMode.Create);
                //fs.Write(Bytes, 0, Bytes.Length);
                //fs.Close();
                //fs.Dispose();

                //return SaveLocation;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            //this.rptViewer.RefreshReport();
        }
    }
}
