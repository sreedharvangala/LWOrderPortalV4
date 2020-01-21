using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Finsoft.Data;

namespace Finsoft.Models
{
	public enum OrdersWaitingForApprovalDataField
    {
        
        OrderHedID,
        
        CompanyID,
        
        CompanyName,
        
        CustSysRowID,
        
        CustNum,
        
        CustID,
        
        CustomerName,
        
        ShipToSysRowID,
        
        ShipToName,
        
        BulkOrder,
        
        OrderDate,
        
        ReqShipDate,
        
        OrderType,
        
        OrderComments,
        
        InternalRemarks,
        
        PromotionRemarks,
        
        OrderStatusID,
        
        OrderStatusName,
        
        Owner,
        
        Creator,
        
        CreatedOn,
        
        ModifiedBy,
        
        ModifiedOn,
        
        Exception,
        
        TotalAmount,
        
        ExceptionDtl,
        
        EpiOrderNum,
        
        EpiError,
        
        TargetReportingDate,
        
        CAddress1,
        
        CAddress2,
        
        CAddress3,
        
        CCity,
        
        CState,
        
        CZip,
        
        CCountry,
        
        SAddress1,
        
        SAddress2,
        
        SAddress3,
        
        SCity,
        
        SState,
        
        SZip,
        
        SCountry,
        
        Balance,
        
        CreditLimit,
        
        UnPaidInvoices,
        
        OutStandingOrders,
        
        UnPostedInvoices,
        
        AgeCurr,
        
        Age30,
        
        Age60,
        
        Age90,
        
        Age120,
        
        Age150,
        
        TermsExceed,
        
        Promotion,
        
        PhoneNum,
        
        FaxNum,
        
        SPhoneNum,
        
        SFaxNum,
    }
    
    public partial class OrdersWaitingForApprovalModel : BusinessRulesObjectModel
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _orderHedID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _companyID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _companyName;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private System.Guid? _custSysRowID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _custNum;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _custID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _customerName;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private System.Guid? _shipToSysRowID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _shipToName;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool? _bulkOrder;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private DateTime? _orderDate;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private DateTime? _reqShipDate;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _orderType;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _orderComments;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _internalRemarks;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _promotionRemarks;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _orderStatusID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _orderStatusName;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _owner;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _creator;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private DateTime? _createdOn;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _modifiedBy;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private DateTime? _modifiedOn;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool? _exception;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _totalAmount;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _exceptionDtl;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _epiOrderNum;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _epiError;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private DateTime? _targetReportingDate;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _cAddress1;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _cAddress2;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _cAddress3;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _cCity;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _cState;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _cZip;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _cCountry;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _sAddress1;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _sAddress2;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _sAddress3;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _sCity;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _sState;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _sZip;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _sCountry;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _balance;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _creditLimit;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _unPaidInvoices;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _outStandingOrders;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _unPostedInvoices;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _ageCurr;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _age30;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _age60;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _age90;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _age120;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _age150;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool? _termsExceed;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _promotion;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _phoneNum;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _faxNum;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _sPhoneNum;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _sFaxNum;
        
        public OrdersWaitingForApprovalModel()
        {
        }
        
        public OrdersWaitingForApprovalModel(BusinessRules r) : 
                base(r)
        {
        }
        
        public int? OrderHedID
        {
            get
            {
                return _orderHedID;
            }
            set
            {
                _orderHedID = value;
                UpdateFieldValue("OrderHedID", value);
            }
        }
        
        public int? CompanyID
        {
            get
            {
                return _companyID;
            }
            set
            {
                _companyID = value;
                UpdateFieldValue("CompanyID", value);
            }
        }
        
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
                UpdateFieldValue("CompanyName", value);
            }
        }
        
        public System.Guid? CustSysRowID
        {
            get
            {
                return _custSysRowID;
            }
            set
            {
                _custSysRowID = value;
                UpdateFieldValue("CustSysRowID", value);
            }
        }
        
        public int? CustNum
        {
            get
            {
                return _custNum;
            }
            set
            {
                _custNum = value;
                UpdateFieldValue("CustNum", value);
            }
        }
        
        public string CustID
        {
            get
            {
                return _custID;
            }
            set
            {
                _custID = value;
                UpdateFieldValue("CustID", value);
            }
        }
        
        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
                UpdateFieldValue("CustomerName", value);
            }
        }
        
        public System.Guid? ShipToSysRowID
        {
            get
            {
                return _shipToSysRowID;
            }
            set
            {
                _shipToSysRowID = value;
                UpdateFieldValue("ShipToSysRowID", value);
            }
        }
        
        public string ShipToName
        {
            get
            {
                return _shipToName;
            }
            set
            {
                _shipToName = value;
                UpdateFieldValue("ShipToName", value);
            }
        }
        
        public bool? BulkOrder
        {
            get
            {
                return _bulkOrder;
            }
            set
            {
                _bulkOrder = value;
                UpdateFieldValue("BulkOrder", value);
            }
        }
        
        public DateTime? OrderDate
        {
            get
            {
                return _orderDate;
            }
            set
            {
                _orderDate = value;
                UpdateFieldValue("OrderDate", value);
            }
        }
        
        public DateTime? ReqShipDate
        {
            get
            {
                return _reqShipDate;
            }
            set
            {
                _reqShipDate = value;
                UpdateFieldValue("ReqShipDate", value);
            }
        }
        
        public string OrderType
        {
            get
            {
                return _orderType;
            }
            set
            {
                _orderType = value;
                UpdateFieldValue("OrderType", value);
            }
        }
        
        public string OrderComments
        {
            get
            {
                return _orderComments;
            }
            set
            {
                _orderComments = value;
                UpdateFieldValue("OrderComments", value);
            }
        }
        
        public string InternalRemarks
        {
            get
            {
                return _internalRemarks;
            }
            set
            {
                _internalRemarks = value;
                UpdateFieldValue("InternalRemarks", value);
            }
        }
        
        public string PromotionRemarks
        {
            get
            {
                return _promotionRemarks;
            }
            set
            {
                _promotionRemarks = value;
                UpdateFieldValue("PromotionRemarks", value);
            }
        }
        
        public int? OrderStatusID
        {
            get
            {
                return _orderStatusID;
            }
            set
            {
                _orderStatusID = value;
                UpdateFieldValue("OrderStatusID", value);
            }
        }
        
        public string OrderStatusName
        {
            get
            {
                return _orderStatusName;
            }
            set
            {
                _orderStatusName = value;
                UpdateFieldValue("OrderStatusName", value);
            }
        }
        
        public string Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
                UpdateFieldValue("Owner", value);
            }
        }
        
        public string Creator
        {
            get
            {
                return _creator;
            }
            set
            {
                _creator = value;
                UpdateFieldValue("Creator", value);
            }
        }
        
        public DateTime? CreatedOn
        {
            get
            {
                return _createdOn;
            }
            set
            {
                _createdOn = value;
                UpdateFieldValue("CreatedOn", value);
            }
        }
        
        public string ModifiedBy
        {
            get
            {
                return _modifiedBy;
            }
            set
            {
                _modifiedBy = value;
                UpdateFieldValue("ModifiedBy", value);
            }
        }
        
        public DateTime? ModifiedOn
        {
            get
            {
                return _modifiedOn;
            }
            set
            {
                _modifiedOn = value;
                UpdateFieldValue("ModifiedOn", value);
            }
        }
        
        public bool? Exception
        {
            get
            {
                return _exception;
            }
            set
            {
                _exception = value;
                UpdateFieldValue("Exception", value);
            }
        }
        
        public decimal? TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
                UpdateFieldValue("TotalAmount", value);
            }
        }
        
        public string ExceptionDtl
        {
            get
            {
                return _exceptionDtl;
            }
            set
            {
                _exceptionDtl = value;
                UpdateFieldValue("ExceptionDtl", value);
            }
        }
        
        public int? EpiOrderNum
        {
            get
            {
                return _epiOrderNum;
            }
            set
            {
                _epiOrderNum = value;
                UpdateFieldValue("EpiOrderNum", value);
            }
        }
        
        public string EpiError
        {
            get
            {
                return _epiError;
            }
            set
            {
                _epiError = value;
                UpdateFieldValue("EpiError", value);
            }
        }
        
        public DateTime? TargetReportingDate
        {
            get
            {
                return _targetReportingDate;
            }
            set
            {
                _targetReportingDate = value;
                UpdateFieldValue("TargetReportingDate", value);
            }
        }
        
        public string CAddress1
        {
            get
            {
                return _cAddress1;
            }
            set
            {
                _cAddress1 = value;
                UpdateFieldValue("CAddress1", value);
            }
        }
        
        public string CAddress2
        {
            get
            {
                return _cAddress2;
            }
            set
            {
                _cAddress2 = value;
                UpdateFieldValue("CAddress2", value);
            }
        }
        
        public string CAddress3
        {
            get
            {
                return _cAddress3;
            }
            set
            {
                _cAddress3 = value;
                UpdateFieldValue("CAddress3", value);
            }
        }
        
        public string CCity
        {
            get
            {
                return _cCity;
            }
            set
            {
                _cCity = value;
                UpdateFieldValue("CCity", value);
            }
        }
        
        public string CState
        {
            get
            {
                return _cState;
            }
            set
            {
                _cState = value;
                UpdateFieldValue("CState", value);
            }
        }
        
        public string CZip
        {
            get
            {
                return _cZip;
            }
            set
            {
                _cZip = value;
                UpdateFieldValue("CZip", value);
            }
        }
        
        public string CCountry
        {
            get
            {
                return _cCountry;
            }
            set
            {
                _cCountry = value;
                UpdateFieldValue("CCountry", value);
            }
        }
        
        public string SAddress1
        {
            get
            {
                return _sAddress1;
            }
            set
            {
                _sAddress1 = value;
                UpdateFieldValue("SAddress1", value);
            }
        }
        
        public string SAddress2
        {
            get
            {
                return _sAddress2;
            }
            set
            {
                _sAddress2 = value;
                UpdateFieldValue("SAddress2", value);
            }
        }
        
        public string SAddress3
        {
            get
            {
                return _sAddress3;
            }
            set
            {
                _sAddress3 = value;
                UpdateFieldValue("SAddress3", value);
            }
        }
        
        public string SCity
        {
            get
            {
                return _sCity;
            }
            set
            {
                _sCity = value;
                UpdateFieldValue("SCity", value);
            }
        }
        
        public string SState
        {
            get
            {
                return _sState;
            }
            set
            {
                _sState = value;
                UpdateFieldValue("SState", value);
            }
        }
        
        public string SZip
        {
            get
            {
                return _sZip;
            }
            set
            {
                _sZip = value;
                UpdateFieldValue("SZip", value);
            }
        }
        
        public string SCountry
        {
            get
            {
                return _sCountry;
            }
            set
            {
                _sCountry = value;
                UpdateFieldValue("SCountry", value);
            }
        }
        
        public decimal? Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                UpdateFieldValue("Balance", value);
            }
        }
        
        public decimal? CreditLimit
        {
            get
            {
                return _creditLimit;
            }
            set
            {
                _creditLimit = value;
                UpdateFieldValue("CreditLimit", value);
            }
        }
        
        public decimal? UnPaidInvoices
        {
            get
            {
                return _unPaidInvoices;
            }
            set
            {
                _unPaidInvoices = value;
                UpdateFieldValue("UnPaidInvoices", value);
            }
        }
        
        public decimal? OutStandingOrders
        {
            get
            {
                return _outStandingOrders;
            }
            set
            {
                _outStandingOrders = value;
                UpdateFieldValue("OutStandingOrders", value);
            }
        }
        
        public decimal? UnPostedInvoices
        {
            get
            {
                return _unPostedInvoices;
            }
            set
            {
                _unPostedInvoices = value;
                UpdateFieldValue("UnPostedInvoices", value);
            }
        }
        
        public decimal? AgeCurr
        {
            get
            {
                return _ageCurr;
            }
            set
            {
                _ageCurr = value;
                UpdateFieldValue("AgeCurr", value);
            }
        }
        
        public decimal? Age30
        {
            get
            {
                return _age30;
            }
            set
            {
                _age30 = value;
                UpdateFieldValue("Age30", value);
            }
        }
        
        public decimal? Age60
        {
            get
            {
                return _age60;
            }
            set
            {
                _age60 = value;
                UpdateFieldValue("Age60", value);
            }
        }
        
        public decimal? Age90
        {
            get
            {
                return _age90;
            }
            set
            {
                _age90 = value;
                UpdateFieldValue("Age90", value);
            }
        }
        
        public decimal? Age120
        {
            get
            {
                return _age120;
            }
            set
            {
                _age120 = value;
                UpdateFieldValue("Age120", value);
            }
        }
        
        public decimal? Age150
        {
            get
            {
                return _age150;
            }
            set
            {
                _age150 = value;
                UpdateFieldValue("Age150", value);
            }
        }
        
        public bool? TermsExceed
        {
            get
            {
                return _termsExceed;
            }
            set
            {
                _termsExceed = value;
                UpdateFieldValue("TermsExceed", value);
            }
        }
        
        public string Promotion
        {
            get
            {
                return _promotion;
            }
            set
            {
                _promotion = value;
                UpdateFieldValue("Promotion", value);
            }
        }
        
        public string PhoneNum
        {
            get
            {
                return _phoneNum;
            }
            set
            {
                _phoneNum = value;
                UpdateFieldValue("PhoneNum", value);
            }
        }
        
        public string FaxNum
        {
            get
            {
                return _faxNum;
            }
            set
            {
                _faxNum = value;
                UpdateFieldValue("FaxNum", value);
            }
        }
        
        public string SPhoneNum
        {
            get
            {
                return _sPhoneNum;
            }
            set
            {
                _sPhoneNum = value;
                UpdateFieldValue("SPhoneNum", value);
            }
        }
        
        public string SFaxNum
        {
            get
            {
                return _sFaxNum;
            }
            set
            {
                _sFaxNum = value;
                UpdateFieldValue("SFaxNum", value);
            }
        }
        
        public FieldValue this[OrdersWaitingForApprovalDataField field]
        {
            get
            {
                return this[field.ToString()];
            }
        }
    }
}
