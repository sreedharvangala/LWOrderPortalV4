using Microsoft.Reporting.WebForms;
using ReportLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PDF_ReportViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ServerReportExecute rpt = new ServerReportExecute();
        //ArrayList arrLstDefaultParam = new ArrayList();
        //arrLstDefaultParam.Add(CreateReportParameter("Company", "qpmsp"));
        //arrLstDefaultParam.Add(CreateReportParameter("CustId", "C1000051,C1000051"));
        //arrLstDefaultParam.Add(CreateReportParameter("SalesRepCode", null));
        //arrLstDefaultParam.Add(CreateReportParameter("GroupCode", null));
        //arrLstDefaultParam.Add(CreateReportParameter("CutOffDate", "2019-07-16"));
        //rpt.ReportParams = arrLstDefaultParam;
        //rptViewer.Controls.Add(rpt.GetReportViewer());
    }
    private ReportParameter CreateReportParameter(string paramName, string pramValue)
    {
        ReportParameter aParam = new ReportParameter(paramName, pramValue);
        return aParam;
    }
}