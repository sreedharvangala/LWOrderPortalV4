using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderEntry
{
    public class Update
    {
        public Update()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => { return true; };
        }
        private static WSHttpBinding GetWsHttpBinding()
        {
            var binding = new WSHttpBinding();
            const int maxBindingSize = Int32.MaxValue;
            binding.MaxReceivedMessageSize = maxBindingSize;
            binding.ReaderQuotas.MaxDepth = maxBindingSize;
            binding.ReaderQuotas.MaxStringContentLength = maxBindingSize;
            binding.ReaderQuotas.MaxArrayLength = maxBindingSize;
            binding.ReaderQuotas.MaxBytesPerRead = maxBindingSize;
            binding.ReaderQuotas.MaxNameTableCharCount = maxBindingSize;
            binding.Security.Mode = SecurityMode.Message;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            return binding;
        }
        public static BasicHttpBinding GetBasicHttpBinding()
        {
            var binding = new BasicHttpBinding();
            const int maxBindingSize = Int32.MaxValue;
            binding.MaxReceivedMessageSize = maxBindingSize;
            binding.ReaderQuotas.MaxDepth = maxBindingSize;
            binding.ReaderQuotas.MaxStringContentLength = maxBindingSize;
            binding.ReaderQuotas.MaxArrayLength = maxBindingSize;
            binding.ReaderQuotas.MaxBytesPerRead = maxBindingSize;
            binding.ReaderQuotas.MaxNameTableCharCount = maxBindingSize;
            binding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;
            binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            return binding;
        }
        private static TClient GetClient<TClient, TInterface>(
                                                                string url,
                                                                string username,
                                                                string password,
                                                                EndpointBindingType bindingType
                                                              )
                                                            where TClient : ClientBase<TInterface>
                                                            where TInterface : class
        {
            System.ServiceModel.Channels.Binding binding = null;
            TClient client;
            //var endpointAddress = new EndpointAddress(url);//LUCKYE101DB.luckyfoods.com.my
            DnsEndpointIdentity endpointIdentity = new DnsEndpointIdentity(Config.DnsIdentity);
            var endpointAddress = new EndpointAddress(new Uri(url), endpointIdentity);
            switch (bindingType)
            {
                case EndpointBindingType.BasicHttp:
                    binding = GetBasicHttpBinding();
                    break;
                case EndpointBindingType.SOAPHttp:
                    binding = GetWsHttpBinding();
                    break;
            }

            TimeSpan operationTimeout = new TimeSpan(0, 12, 0);
            binding.CloseTimeout = operationTimeout;
            binding.ReceiveTimeout = operationTimeout;
            binding.SendTimeout = operationTimeout;
            binding.OpenTimeout = operationTimeout;

            client = (TClient)Activator.CreateInstance(typeof(TClient), binding, endpointAddress);
            if (!string.IsNullOrEmpty(username) && (client.ClientCredentials != null))
            {
                client.ClientCredentials.UserName.UserName = username;
                client.ClientCredentials.UserName.Password = password;
            }
            return client;
        }
        private enum EndpointBindingType
        {
            SOAPHttp,
            BasicHttp
        }

        public EpiResponse Process(SOHd objSOHd)
        {
            EndpointBindingType bindingType = EndpointBindingType.BasicHttp;
            string epicorUserID = Config.epicorUserID;//"manager";
            string epiorUserPassword = Config.epiorUserPassword;//"finlucky08";
            Guid sessionId = Guid.Empty;
            svcSession.SessionModSvcContractClient sessionModClient = null;

            string scheme = "https";
            if (bindingType == EndpointBindingType.SOAPHttp)
            {
                scheme = "http";
            }
            UriBuilder builder = new UriBuilder(scheme, Config.hostName);

            builder.Path = $"{Config.environment}/Ice/Lib/SessionMod.svc";

            EpiResponse objEpiRes = null;

            try
            {
                sessionModClient = GetClient<svcSession.SessionModSvcContractClient, svcSession.SessionModSvcContract>
                                            (
                                            builder.Uri.ToString(),
                                            epicorUserID,
                                            epiorUserPassword,
                                            bindingType
                                            );



                sessionId = sessionModClient.Login();

                builder.Path = $"{Config.environment}/Ice/Lib/SessionMod.svc";
                sessionModClient = GetClient<svcSession.SessionModSvcContractClient, svcSession.SessionModSvcContract>(builder.Uri.ToString(), epicorUserID, epiorUserPassword, bindingType);

                sessionModClient.Endpoint.EndpointBehaviors.Add(new HookServiceBehavior(sessionId, epicorUserID));



                string newCompany = string.Empty;
                string plant = string.Empty;
                string siteID = string.Empty;
                string siteName = string.Empty;
                string workstationID = string.Empty;
                string workstationDescription = string.Empty;
                string employeeID = string.Empty;
                string countryGroupCode = string.Empty;
                string countryCode = string.Empty;
                string tenantID = string.Empty;

               
                sessionModClient.SetCompany(objSOHd.Company, out siteID, out siteName, out workstationID, out workstationDescription, out employeeID, out countryGroupCode, out countryCode, out tenantID);
                sessionModClient.SetPlant(Config.Plant);

                if (sessionId != Guid.Empty)
                {

                    //Call the Require function
                    objEpiRes =SOUpdate(epicorUserID, epiorUserPassword, builder, sessionId, objSOHd);

                }
                else
                {

                    throw new Exception($"Error : Sessionid is Empty!");
                }
            }
            catch (Exception ex)
            {
                objEpiRes = new EpiResponse();
                objEpiRes.ErrMsg=$"Epicor (SalesOrderEntry) Error : {ex.Message.ToString()}";
            }
            finally
            {
                if (sessionId != Guid.Empty)
                {
                    sessionModClient.Logout();
                }
            }
            return objEpiRes;
        }
        public EpiResponse SOUpdate(string epicorUserID, string epiorUserPassword, UriBuilder builder, Guid sessionId, SOHd objSOHd)
        {

            EndpointBindingType bindingType = EndpointBindingType.BasicHttp;
            EpiResponse objEpiRes = new EpiResponse()
            {
                Company = objSOHd.Company
                ,COTOrderNum=objSOHd.COTOrderNum
                ,CustId=objSOHd.CustId
                
            };

            try
            {
                builder.Path = $"{Config.environment}/Erp/BO/SalesOrder.svc";
                svcSalesOrder.SalesOrderSvcContractClient SOClient = GetClient<svcSalesOrder.SalesOrderSvcContractClient, svcSalesOrder.SalesOrderSvcContract>(
                builder.Uri.ToString(),
                epicorUserID,
                epiorUserPassword,
                bindingType);

                SOClient.Endpoint.EndpointBehaviors.Add(new HookServiceBehavior(sessionId, epicorUserID));


                var ds = new svcSalesOrder.SalesOrderTableset();
                if(objSOHd.EpiOrderNum>0)
                {
                    ds= SOClient.GetByID(objSOHd.EpiOrderNum);

                    ds.OrderDtl.ForEach(f => f.RowMod = "D");

                    SOClient.Update(ref ds);
                }
                else
                {
                    SOClient.GetNewOrderHed(ref ds);
                    bool IContinue;
                    SOClient.OnChangeofSoldToCreditCheck(0, objSOHd.CustId, ref ds, out IContinue);
                    SOClient.ChangeSoldToID(ref ds);
                    SOClient.ChangeCustomer(ref ds);

                    int custNum = ds.OrderHed[0].CustNum;
                    if (!string.IsNullOrEmpty(objSOHd.ShipToNum))
                    {
                        ds.OrderHed[0].ShipToNum = objSOHd.ShipToNum;
                        SOClient.ChangeShipToID(ref ds);
                    }
                    ds.OrderHed[0].OrderDate = objSOHd.OrderDate;
                    ds.OrderHed[0].NeedByDate = objSOHd.ReqShipDate;
                    ds.OrderHed[0].OrderComment = objSOHd.OrderComments == null ? "" : objSOHd.OrderComments;
                    ds.OrderHed[0].ShipComment = objSOHd.OrderComments == null ? "" : objSOHd.OrderComments;
                    ds.OrderHed[0].InvoiceComment = objSOHd.OrderComments == null ? "" : objSOHd.OrderComments;
                    ds.OrderHed[0].PONum = objSOHd.PONum == "P" ? "P" : "";
                    ds.OrderHed[0].OrderHeld = true;

                    ds.OrderHed[0].UserDefinedColumns["WEMOrdNum_c"] = objSOHd.COTOrderNum;
                    ds.OrderHed[0].UserDefinedColumns["IntenalInstructionRemarks_c"] = objSOHd.InternalRemarks == null ? "" : objSOHd.InternalRemarks;
                    ds.OrderHed[0].UserDefinedColumns["InvRemarks_c"] = objSOHd.OrderComments == null ? "" : objSOHd.OrderComments;
                    ds.OrderHed[0].UserDefinedColumns["PromotionRemarks_c"] = objSOHd.PromotionRemarks == null ? "" : objSOHd.PromotionRemarks;
                    ds.OrderHed[0].UserDefinedColumns["ManualSONum_c"] = objSOHd.ManualSO;
                    ds.OrderHed[0].UserDefinedColumns["Promotion_c"] = objSOHd.Promotion;

                    ds.OrderHed[0].UserDefinedColumns["CheckBox02"] = objSOHd.BulkOrder; // BulkOrder

                    ds.OrderHed[0].UserDefinedColumns["ShortChar10"] = objSOHd.COTOrderNum.ToString();
                    ds.OrderHed[0].UserDefinedColumns["Character01"] = objSOHd.InternalRemarks == null ? "" : objSOHd.InternalRemarks;

                    SOClient.Update(ref ds);
                }
                
                int iOrderNum = ds.OrderHed[0].OrderNum;
                objEpiRes.EpiOrderNum = iOrderNum;
                string PartNum = ""; decimal Qty = 0; string UOM = ""; decimal UnitPrice = 0;
                int totLnRows = objSOHd.SODtl.Count();
                int currLnRow = 0;
                foreach (var rowSODtl in objSOHd.SODtl)
                {
                    int loopCnt = 1;
                    int rowFOC = 0;
                    if (rowSODtl.OrderQty>0 && rowSODtl.FOCQty > 0)
                        loopCnt = 2;

                    if (loopCnt==2)
                    {
                        totLnRows++;
                    }
                    while (rowFOC < loopCnt)
                    {
                        SOClient.GetNewOrderDtl(ref ds, iOrderNum);
                        int currentIndex = ds.OrderDtl.Count - 1;
                        currLnRow++;
                        //Not require as per Chuah's mail
                        if (currLnRow == totLnRows)
                            ds.OrderDtl[currentIndex].OrderComment = objSOHd.OrderComments == null ? "" : objSOHd.OrderComments;
                        bool Sub1 = false;
                        bool Phantom = false;
                        string UOMCode = string.Empty;
                        string rowType = string.Empty;
                        bool KitType = false;
                        Guid guid = new Guid("00000000-0000-0000-0000-000000000000");
                        bool removekitcomponent = false;
                        bool suppressprompt = false;
                        bool getPartXInfo = true;
                        bool checkPartRev = true;
                        bool checkChangeKitParent = true;
                        string questionstring = string.Empty;
                        string warningmsg = string.Empty;
                        bool multiplematch = false;
                        bool promttoexplode = false;
                        string configpartmessage = string.Empty;
                        string subPartMessage = string.Empty;
                        string explodeBOMErrMsg = string.Empty;
                        string msgType = string.Empty;
                        bool multiSubAvail = false;
                        bool runQtyAvail = false;
                        PartNum = rowSODtl.PartNum;

                        SOClient.ChangePartNumMaster(ref PartNum, ref Sub1, ref Phantom, ref UOMCode,
                               guid, rowType, KitType, removekitcomponent, suppressprompt, getPartXInfo, checkPartRev,
                               checkChangeKitParent, ref ds, out questionstring, out warningmsg,
                               out multiplematch, out promttoexplode, out configpartmessage, out subPartMessage,
                               out explodeBOMErrMsg, out msgType, out multiSubAvail, out runQtyAvail);

                        if (string.IsNullOrEmpty(rowSODtl.UOM))
                        {
                            UOM = ds.OrderDtl[currentIndex].SalesUM;
                        }
                        else
                        {
                            UOM = rowSODtl.UOM;
                        }

                        int pdDimConvFac = 1;
                        string pcNegQtyAction = string.Empty;
                        string opWarningMsg = string.Empty;
                        string sellingQtyChangeMsg = string.Empty;
                        bool IsFOCItem = false;
                        if(loopCnt==2)
                        {
                            if (rowFOC == 0)
                            {
                                Qty = rowSODtl.OrderQty;
                            }
                            else
                            {
                                Qty = rowSODtl.FOCQty;
                                IsFOCItem = true;
                            }
                        }
                        else
                        {
                            if (rowSODtl.OrderQty > 0)
                            {
                                Qty = rowSODtl.OrderQty;
                            }
                            else if(rowSODtl.FOCQty>0)
                            {
                                Qty = rowSODtl.FOCQty;
                                IsFOCItem = true;
                            }
                            else
                            {
                                //Something not right
                            }
                        }
                        
                        

                        SOClient.ChangeSellingQtyMaster(ref ds, Qty, false, false, true, true, false,
                            true, PartNum, "", "", "", UOM, pdDimConvFac, out pcNegQtyAction, out opWarningMsg,
                            out sellingQtyChangeMsg);

                        if (rowSODtl.SellingPrice > 0)
                        {
                            //ds.OrderDtl[currentIndex].UnitPrice = rowSODtl.SellingPrice;
                            //ds.OrderDtl[currentIndex].DocUnitPrice = rowSODtl.SellingPrice;
                            //ds.OrderDtl[currentIndex].DspUnitPrice = rowSODtl.SellingPrice;
                            //ds.OrderDtl[currentIndex].DocDspUnitPrice = rowSODtl.SellingPrice;
                            //SOClient.ChangeUnitPrice(ref ds);
                        }

                        //ds.OrderDtl[currentIndex].LockQty = true;
                        if (objSOHd.BulkOrder)
                        {
                            ds.OrderDtl[currentIndex].UserDefinedColumns["Number14"] = Qty;
                        }
                        else
                        {
                            ds.OrderDtl[currentIndex].UserDefinedColumns["Number01"] = Qty;
                        }
                            


                        //saras
                        if (!IsFOCItem)
                        {
                            ds.OrderDtl[currentIndex].UserDefinedColumns["Number03"] = rowSODtl.SellingPrice;

                            ds.OrderDtl[currentIndex].UserDefinedColumns["Number04"] = rowSODtl.ProposedSellingPrice > 0 ? rowSODtl.ProposedSellingPrice : rowSODtl.SellingPrice; // PropposeSellingPrice

                            ds.OrderDtl[currentIndex].UserDefinedColumns["Number13"] = rowSODtl.BasePrice;
                            ds.OrderDtl[currentIndex].UserDefinedColumns["Number05"] = rowSODtl.ProposedBasePrice > 0 ? rowSODtl.ProposedBasePrice : rowSODtl.BasePrice; // ProposeBasePrice
                        }
                        else
                        {
                            //if (IsFOCItem)
                            //{
                            // //ds.OrderDtl[currentIndex].UserDefinedColumns["UnitPrice"] = 0.0000m.ToString();
                            // ds.OrderDtl[currentIndex].UserDefinedColumns["UnitPrice"] = decimal.Zero;
                            // ds.OrderDtl[currentIndex].UserDefinedColumns["Number03"] = decimal.Zero;
                            // }
                            ds.OrderDtl[currentIndex].UserDefinedColumns["Number03"] = rowSODtl.SellingPrice;

                            ds.OrderDtl[currentIndex].UserDefinedColumns["Number04"] = decimal.Zero; ; // PropposeSellingPrice

                            //ds.OrderDtl[currentIndex].UserDefinedColumns["UnitPrice"] = decimal.Zero;  // UnitPrice

                            //ds.OrderDtl[currentIndex].UserDefinedColumns["Number13"] = 0;
                            ds.OrderDtl[currentIndex].UserDefinedColumns["Number05"] = decimal.Zero; // ProposeBasePrice

                            ds.OrderDtl[currentIndex].UnitPrice = decimal.Zero;
                            ds.OrderDtl[currentIndex].DocUnitPrice = decimal.Zero;
                            ds.OrderDtl[currentIndex].DspUnitPrice = decimal.Zero;
                            ds.OrderDtl[currentIndex].DocDspUnitPrice = decimal.Zero;
                            SOClient.ChangeUnitPrice(ref ds);
                        }

                        string cRepMsg = string.Empty;
                        string cRespMsgOrdRel = string.Empty;
                        bool lCheckForOrderChangedMs=false;
                        bool lcheckForResponse=false;
                        string cTableName= "OrderDtl";
                        int iCustNum = ds.OrderHed[0].CustNum;
                        bool lweLicensed=false;
                        string cResponseMsg="";
                        string cCreditShipAction="";
                        string cDisplayMsg="";
                        string cCompliantMsg="";
                        string cResponseMsgOrdRel="";
                        

                        SOClient.MasterUpdate(lCheckForOrderChangedMs, lcheckForResponse, cTableName, iCustNum, iOrderNum, lweLicensed, ref  ds, out  cResponseMsg, out  cCreditShipAction, out  cDisplayMsg, out  cCompliantMsg, out  cResponseMsgOrdRel);

                        
                        int iOrderLine = ds.OrderDtl.Max(m=>m.OrderLine);

                        if (!IsFOCItem)
                        {
                            int relRowCnt = 0;
                            foreach (var rowSORel in rowSODtl.SORel.Where(w => w.OrderRelQty > 0))
                            {

                                if (relRowCnt > 0)
                                {
                                    SOClient.GetNewOrderRel(ref ds, iOrderNum, iOrderLine);
                                    int relIndex = ds.OrderRel.Count - 1;
                                    if (ds.OrderRel[relIndex].SellingReqQty != rowSORel.OrderRelQty)
                                    {
                                        ds.OrderRel[relIndex].SellingReqQty = rowSORel.OrderRelQty;
                                        ds.OrderRel[relIndex].OurReqQty = rowSORel.OrderRelQty;
                                        //_svcSalesOrder.ChangeSellingReqQty(ref dsSalesOrder, rowSORel.RelQty);
                                    }

                                    if (ds.OrderRel[relIndex].ShipToNum != rowSORel.ShipToNum)
                                        ds.OrderRel[relIndex].ShipToNum = rowSORel.ShipToNum;
                                    if (ds.OrderRel[relIndex].NeedByDate != rowSORel.ShipByDate)
                                        ds.OrderRel[relIndex].NeedByDate = rowSORel.ShipByDate;
                                    SOClient.ChangeOrderRelShipTo(ref ds);

                                }
                                else
                                {
                                    int relIndex = ds.OrderRel.Count - 1;
                                    if (ds.OrderRel[relIndex].SellingReqQty != rowSORel.OrderRelQty)
                                    {
                                        ds.OrderRel[relIndex].SellingReqQty = rowSORel.OrderRelQty;
                                        ds.OrderRel[relIndex].OurReqQty = rowSORel.OrderRelQty;
                                        //_svcSalesOrder.ChangeSellingReqQty(ref dsSalesOrder, rowSORel.RelQty);
                                    }
                                    if (ds.OrderRel[relIndex].ShipToNum != rowSORel.ShipToNum)
                                        ds.OrderRel[relIndex].ShipToNum = rowSORel.ShipToNum;
                                    if (ds.OrderRel[relIndex].NeedByDate != rowSORel.ShipByDate)
                                        ds.OrderRel[relIndex].NeedByDate = rowSORel.ShipByDate;
                                    ds.OrderRel[relIndex].RowMod = "U";
                                    SOClient.ChangeOrderRelShipTo(ref ds);

                                }

                                SOClient.Update(ref ds);
                                

                                relRowCnt++;
                            }
                        }
                        else
                        {
                            int relRowCnt = 0;
                            foreach (var rowSORel in rowSODtl.SORel.Where(w => w.FOCRelQty > 0))
                            {

                                if (relRowCnt > 0)
                                {
                                    SOClient.GetNewOrderRel(ref ds, iOrderNum, iOrderLine);
                                    int relIndex = ds.OrderRel.Count - 1;
                                    if (ds.OrderRel[relIndex].SellingReqQty != rowSORel.FOCRelQty)
                                    {
                                        ds.OrderRel[relIndex].SellingReqQty = rowSORel.FOCRelQty;
                                        ds.OrderRel[relIndex].OurReqQty = rowSORel.FOCRelQty;
                                        //_svcSalesOrder.ChangeSellingReqQty(ref dsSalesOrder, rowSORel.RelQty);
                                    }

                                    if (ds.OrderRel[relIndex].ShipToNum != rowSORel.ShipToNum)
                                        ds.OrderRel[relIndex].ShipToNum = rowSORel.ShipToNum;
                                    if (ds.OrderRel[relIndex].NeedByDate != rowSORel.ShipByDate)
                                        ds.OrderRel[relIndex].NeedByDate = rowSORel.ShipByDate;
                                    SOClient.ChangeOrderRelShipTo(ref ds);

                                    
                                }
                                else
                                {
                                    int relIndex = ds.OrderRel.Count - 1;
                                    if (ds.OrderRel[relIndex].SellingReqQty != rowSORel.FOCRelQty)
                                    {
                                        ds.OrderRel[relIndex].SellingReqQty = rowSORel.FOCRelQty;
                                        ds.OrderRel[relIndex].OurReqQty = rowSORel.FOCRelQty;
                                        //_svcSalesOrder.ChangeSellingReqQty(ref dsSalesOrder, rowSORel.RelQty);
                                    }
                                    if (ds.OrderRel[relIndex].ShipToNum != rowSORel.ShipToNum)
                                        ds.OrderRel[relIndex].ShipToNum = rowSORel.ShipToNum;
                                    if (ds.OrderRel[relIndex].NeedByDate != rowSORel.ShipByDate)
                                        ds.OrderRel[relIndex].NeedByDate = rowSORel.ShipByDate;
                                    ds.OrderRel[relIndex].RowMod = "U";
                                    SOClient.ChangeOrderRelShipTo(ref ds);

                                   
                                }

                                SOClient.Update(ref ds);
                                
                                relRowCnt++;
                            }
                        }
                        
                        rowFOC++;
                    }
                }

                //ds.OrderHed[0].UserDefinedColumns["CheckBox01"] = true;//Approved
                ds.OrderHed[0].UserDefinedColumns["CheckBox04"] = true;//Submit Order
                ds.OrderHed[0].RowMod = "U";//Submit Order
                SOClient.Update(ref ds);
            }
            catch (Exception ex)
            {
                objEpiRes.ErrMsg = ex.Message.ToString();
            }

            return objEpiRes;

        }

    }
}
