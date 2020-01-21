using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Finsoft.Data;

namespace Finsoft.Models
{
	public enum OrderHedDataField
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
        
        PromotionID,
        
        PromotionTitle,
        
        OrderComments,
        
        InternalRemarks,
        
        PromotionRemarks,
        
        OrderStatusID,
        
        OrderStatusName,
    }
    
    public partial class OrderHedModel : BusinessRulesObjectModel
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
        private int? _promotionID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _promotionTitle;
        
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
        
        public OrderHedModel()
        {
        }
        
        public OrderHedModel(BusinessRules r) : 
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
        
        public int? PromotionID
        {
            get
            {
                return _promotionID;
            }
            set
            {
                _promotionID = value;
                UpdateFieldValue("PromotionID", value);
            }
        }
        
        public string PromotionTitle
        {
            get
            {
                return _promotionTitle;
            }
            set
            {
                _promotionTitle = value;
                UpdateFieldValue("PromotionTitle", value);
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
        
        public FieldValue this[OrderHedDataField field]
        {
            get
            {
                return this[field.ToString()];
            }
        }
    }
}
