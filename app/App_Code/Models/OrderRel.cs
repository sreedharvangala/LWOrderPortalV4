using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Finsoft.Data;

namespace Finsoft.Models
{
	public enum OrderRelDataField
    {
        
        OrderRelID,
        
        OrderHedID,
        
        OrderHedCustID,
        
        OrderHedBulkOrder,
        
        OrderDtlID,
        
        OrderDtlPartNum,
        
        OrderRelQty,
        
        FocrelQty,
        
        ShipByDate,
        
        ShipToSysRowID,
        
        ShipToName,
        
        Seq,
        
        OHCompany,
        
        OHCustNum,
        
        OHCustID,
    }
    
    public partial class OrderRelModel : BusinessRulesObjectModel
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _orderRelID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _orderHedID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _orderHedCustID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool? _orderHedBulkOrder;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _orderDtlID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _orderDtlPartNum;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _orderRelQty;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _focrelQty;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private DateTime? _shipByDate;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private System.Guid? _shipToSysRowID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _shipToName;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _seq;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _oHCompany;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _oHCustNum;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _oHCustID;
        
        public OrderRelModel()
        {
        }
        
        public OrderRelModel(BusinessRules r) : 
                base(r)
        {
        }
        
        public int? OrderRelID
        {
            get
            {
                return _orderRelID;
            }
            set
            {
                _orderRelID = value;
                UpdateFieldValue("OrderRelID", value);
            }
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
        
        public string OrderHedCustID
        {
            get
            {
                return _orderHedCustID;
            }
            set
            {
                _orderHedCustID = value;
                UpdateFieldValue("OrderHedCustID", value);
            }
        }
        
        public bool? OrderHedBulkOrder
        {
            get
            {
                return _orderHedBulkOrder;
            }
            set
            {
                _orderHedBulkOrder = value;
                UpdateFieldValue("OrderHedBulkOrder", value);
            }
        }
        
        public int? OrderDtlID
        {
            get
            {
                return _orderDtlID;
            }
            set
            {
                _orderDtlID = value;
                UpdateFieldValue("OrderDtlID", value);
            }
        }
        
        public string OrderDtlPartNum
        {
            get
            {
                return _orderDtlPartNum;
            }
            set
            {
                _orderDtlPartNum = value;
                UpdateFieldValue("OrderDtlPartNum", value);
            }
        }
        
        public decimal? OrderRelQty
        {
            get
            {
                return _orderRelQty;
            }
            set
            {
                _orderRelQty = value;
                UpdateFieldValue("OrderRelQty", value);
            }
        }
        
        public decimal? FocrelQty
        {
            get
            {
                return _focrelQty;
            }
            set
            {
                _focrelQty = value;
                UpdateFieldValue("FocrelQty", value);
            }
        }
        
        public DateTime? ShipByDate
        {
            get
            {
                return _shipByDate;
            }
            set
            {
                _shipByDate = value;
                UpdateFieldValue("ShipByDate", value);
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
        
        public int? Seq
        {
            get
            {
                return _seq;
            }
            set
            {
                _seq = value;
                UpdateFieldValue("Seq", value);
            }
        }
        
        public string OHCompany
        {
            get
            {
                return _oHCompany;
            }
            set
            {
                _oHCompany = value;
                UpdateFieldValue("OHCompany", value);
            }
        }
        
        public int? OHCustNum
        {
            get
            {
                return _oHCustNum;
            }
            set
            {
                _oHCustNum = value;
                UpdateFieldValue("OHCustNum", value);
            }
        }
        
        public string OHCustID
        {
            get
            {
                return _oHCustID;
            }
            set
            {
                _oHCustID = value;
                UpdateFieldValue("OHCustID", value);
            }
        }
        
        public FieldValue this[OrderRelDataField field]
        {
            get
            {
                return this[field.ToString()];
            }
        }
    }
}
