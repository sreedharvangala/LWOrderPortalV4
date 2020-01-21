using Dapper;
using SQLTool;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderEntry
{
    public class EpiRequest
    {
        public EpiResponse CreateSO(int COTOrderNo)
        {
            EpiResponse objEpiRes = null;
            SqlClass.Conn = ConfigurationManager.ConnectionStrings["Finsoft"].ToString();
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine("Select a.OrderHedID as COTOrderNum,b.CompanyName as Company,a.CustNum,a.CustID,c.ShipToNum ");
            strBuilder.AppendLine(" ,a.[Owner] as SalesRep ,a.OrderComments,a.InternalRemarks,a.PromotionRemarks,a.ManualSO,d.Title as Promotion,a.OrderType as PONum,a.OrderDate,a.ReqShipDate,a.BulkOrder,isnull(a.EpiOrderNum,0) as EpiOrderNum from OrderHed a ");
            strBuilder.AppendLine(" left join Company b on a.CompanyID=b.CompanyID left join vwShipTo c on a.ShipToSysRowID=c.SysRowID left join Promotion d on a.PromotionID=d.PromotionID ");
            strBuilder.AppendLine($" Where a.OrderHedID={COTOrderNo} ");
                
            string errMsg = string.Empty;
            SOHd SOHd= SqlClass.SelectQry<SOHd>(strBuilder.ToString(), ref errMsg).FirstOrDefault();
            strBuilder.Clear();

            strBuilder.AppendLine(" select a.[Description] + ', ' AS 'data()' ");
            strBuilder.AppendLine(" from Promotion a inner join OrderPromotions b ");
            strBuilder.AppendLine($" on a.PromotionID=b.PromotionID where b.OrderHedID = {COTOrderNo} FOR XML PATH('') "); 
            object promoRem= SqlClass.ExecutScalarQry(strBuilder.ToString(), ref errMsg);
            if(promoRem!=null)
            {
                SOHd.Promotion = promoRem.ToString();
                SOHd.Promotion.Trim(' ',',');
            }
            strBuilder.Clear();
            strBuilder.AppendLine("Select a.OrderHedID as COTOrderNum,a.OrderDtlID as COTOrderLineNum ");
            strBuilder.AppendLine(" ,a.PartNum,a.OrderQty,a.FOCQty,a.UOM,a.BasePrice,a.SellingPrice,a.ProposedBasePrice,a.ProposedSellingPrice ");
            strBuilder.AppendLine($" from OrderDtl a Where a.OrderHedID={COTOrderNo} ");

            IEnumerable<SODtl> SODtl = SqlClass.SelectQry<SODtl>(strBuilder.ToString(), ref errMsg);
            

            foreach (var dtl in SODtl)
            {
                strBuilder.Clear();
                strBuilder.AppendLine("Select a.OrderDtlID as COTOrderLineNum,a.OrderRelID as COTOrderRelNum,b.ShipToNum ");
                strBuilder.AppendLine(" ,a.OrderRelQty,a.FOCRelQty,a.ShipByDate ");
                strBuilder.AppendLine(" from OrderRel a left join vwShipTo b on a.ShipToSysRowID=b.SysRowID ");
                strBuilder.AppendLine($" Where a.OrderDtlID={dtl.COTOrderLineNum} ");
                if(SOHd.BulkOrder)
                    strBuilder.AppendLine($" Order by a.ShipByDate desc ");

                IEnumerable<SORel> SORel = SqlClass.SelectQry<SORel>(strBuilder.ToString(), ref errMsg);
                dtl.SORel = SORel.ToList();
            }
            SOHd.SODtl = SODtl.ToList();

            //Call Epicor BO
            if(SOHd!=null)
            {
                Update upd = new Update();
                objEpiRes = upd.Process(SOHd);

                //
                if(string.IsNullOrEmpty(objEpiRes.ErrMsg) && objEpiRes.EpiOrderNum>0)
                {
                    int maxCount = GetMaxApprovalQueuesID();

                    maxCount += 1;

                    ApprovalQueue approvalQueue = new ApprovalQueue();

                    approvalQueue.CompanyID = SOHd.Company;
                    approvalQueue.ApprovalQueueID = maxCount.ToString();
                    approvalQueue.ModuleID = "SO";
                    approvalQueue.DocumentNo = objEpiRes.EpiOrderNum.ToString();
                    approvalQueue.ApprovalStatus = "Approved";
                    approvalQueue.ApprovalStage = "Queue";
                    approvalQueue.SupplierID = "";
                    approvalQueue.Remarks = "COT Order Approved";
                    approvalQueue.SubmitDate = DateTime.Now;
                    approvalQueue.ProceedDate = Convert.ToDateTime("01/01/1900");

                    string ErMsg= InsertApprovalQueue(approvalQueue);
                    if (!string.IsNullOrEmpty(ErMsg))
                        objEpiRes.ErrMsg = string.Format("Not Inserted to Approval Queue due to {0}",ErMsg);
                }

                
            }

            return objEpiRes;
        }
        public string InsertApprovalQueue(ApprovalQueue approvalQueue)
        {
            string environment= ConfigurationManager.AppSettings["dbname"].ToString();
            StringBuilder sql = new StringBuilder();
            //sql.Append(" IF NOT EXISTS(SELECT Company FROM Ice.UD24 WHERE Company = " + Escape(approvalQueue.CompanyID) + " AND ShortChar02 = " + Escape(approvalQueue.DocumentNo) + " AND Key1 = " + Escape(approvalQueue.ApprovalQueueID) + ") ");
            sql.Append($" IF NOT EXISTS(SELECT Company FROM {environment}.Ice.UD24 WHERE Company = " + Escape(approvalQueue.CompanyID) + " AND ShortChar02 = " + Escape(approvalQueue.DocumentNo) + " AND Character01 = " + Escape(approvalQueue.Remarks) + ") ");
            sql.Append(" BEGIN ");
            sql.Append($" INSERT INTO {environment}.Ice.UD24(Company, Key1, ShortChar01, ShortChar02, ShortChar03, ShortChar04, ShortChar05, Character01, Date01, Date03) ");
            sql.Append(" VALUES( " + Escape(approvalQueue.CompanyID) + ", ");
            sql.Append("         " + Escape(approvalQueue.ApprovalQueueID) + ", ");
            sql.Append("         " + Escape(approvalQueue.ModuleID) + ", ");
            sql.Append("         " + Escape(approvalQueue.DocumentNo) + ", ");
            sql.Append("         " + Escape(approvalQueue.ApprovalStatus) + ", ");
            sql.Append("         " + Escape(approvalQueue.ApprovalStage) + ", ");
            sql.Append("         " + Escape(approvalQueue.SupplierID) + ", ");
            sql.Append("         " + Escape(approvalQueue.Remarks) + ", ");
            sql.Append("         " + Escape(CommonClasses.ConvertDateToYMD(approvalQueue.SubmitDate.ToString())) + ", ");
            sql.Append("         " + Escape(CommonClasses.ConvertDateToYMD(approvalQueue.ProceedDate.ToString())) + ") ");
            sql.Append(" END ");

            SqlClass.Conn = ConfigurationManager.ConnectionStrings["Finsoft"].ToString();

            string errMsg = null;
            int count = (int)SqlClass.ExecuteCmdQry(sql.ToString(), ref errMsg);

            return errMsg;
        }
        public int GetMaxApprovalQueuesID()
        {
            StringBuilder sql = new StringBuilder();
            //sql.Append(" SELECT COUNT(Company) ");
            sql.Append(" Select MaxQueId ");
            sql.Append(" FROM vwGetMaxApprovalQueId ");

            SqlClass.Conn = ConfigurationManager.ConnectionStrings["Finsoft"].ToString();

            string errMsg = null;
            int count = (int)SqlClass.ExecutScalarQry(sql.ToString(), ref errMsg);
            
            return count;
        }
        public static string Escape(string s, int maxLength)
        {
            if (String.IsNullOrEmpty(s))
                return "''";
            else
            {
                s = s.Trim();
                if (s.Length > maxLength) s = s.Substring(0, maxLength - 1);
                return "'" + s.Trim().Replace("'", "''") + "'";
            }
        }
        public static string Escape(string s)
        {
            if (String.IsNullOrEmpty(s))
                return "''";
            else
                return "'" + s.Trim().Replace("'", "''") + "'";
        }
        //public void MapMultipleObject()
        //{

        //    using (IDbConnection cnn = new SqlConnection(GetConString()))
        //    {
        //        string sql = "";
        //        cnn.Query<SOHd, SODtl, SOHd>(sql, (SOHd, SODtl) => { SOHd.COTOrderNum = SODtl; return SOHd; });
        //    }
        //}
        public string GetConString()
        {
            return ConfigurationManager.ConnectionStrings["Finsoft"].ToString();
        }
    }
}
