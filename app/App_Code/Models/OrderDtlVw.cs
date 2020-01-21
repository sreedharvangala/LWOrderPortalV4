using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Finsoft.Data;

namespace Finsoft.Models
{
	public enum OrderDtlVwDataField
    {
        
        OrderDtlID,
        
        OrderHedID,
        
        OrderHedCustID,
        
        OrderHedBulkOrder,
        
        PartSysRowID,
        
        PartNum,
        
        PartDescription,
        
        Uom,
        
        SellingPrice,
        
        BasePrice,
        
        OrderQty,
        
        Focqty,
        
        ChangeSellingPrice,
        
        ChangeBasePrice,
        
        ProposedBasePrice,
        
        ProposedSellingPrice,
        
        Amount,
        
        CtnConv,
        
        ProposedAmount,
        
        OHCompany,
        
        OHCustNum,
        
        OHShipByDate,
        
        OHShipToSysRowID,
        
        OHShipToName,
        
        SellingPricePerBottle,
        
        BasePricePerBottle,
        
        ProposedSellingPricePerBottle,
        
        ProposedBasePricePerBottle,
    }
    
    public partial class OrderDtlVwModel : BusinessRulesObjectModel
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _orderDtlID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _orderHedID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _orderHedCustID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool? _orderHedBulkOrder;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private System.Guid? _partSysRowID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _partNum;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _partDescription;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _uom;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _sellingPrice;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _basePrice;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _orderQty;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _focqty;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool? _changeSellingPrice;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool? _changeBasePrice;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _proposedBasePrice;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _proposedSellingPrice;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _amount;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _ctnConv;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _proposedAmount;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _oHCompany;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _oHCustNum;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private DateTime? _oHShipByDate;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private System.Guid? _oHShipToSysRowID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _oHShipToName;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _sellingPricePerBottle;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _basePricePerBottle;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _proposedSellingPricePerBottle;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _proposedBasePricePerBottle;
        
        public OrderDtlVwModel()
        {
        }
        
        public OrderDtlVwModel(BusinessRules r) : 
                base(r)
        {
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
        
        public System.Guid? PartSysRowID
        {
            get
            {
                return _partSysRowID;
            }
            set
            {
                _partSysRowID = value;
                UpdateFieldValue("PartSysRowID", value);
            }
        }
        
        public string PartNum
        {
            get
            {
                return _partNum;
            }
            set
            {
                _partNum = value;
                UpdateFieldValue("PartNum", value);
            }
        }
        
        public string PartDescription
        {
            get
            {
                return _partDescription;
            }
            set
            {
                _partDescription = value;
                UpdateFieldValue("PartDescription", value);
            }
        }
        
        public string Uom
        {
            get
            {
                return _uom;
            }
            set
            {
                _uom = value;
                UpdateFieldValue("Uom", value);
            }
        }
        
        public decimal? SellingPrice
        {
            get
            {
                return _sellingPrice;
            }
            set
            {
                _sellingPrice = value;
                UpdateFieldValue("SellingPrice", value);
            }
        }
        
        public decimal? BasePrice
        {
            get
            {
                return _basePrice;
            }
            set
            {
                _basePrice = value;
                UpdateFieldValue("BasePrice", value);
            }
        }
        
        public decimal? OrderQty
        {
            get
            {
                return _orderQty;
            }
            set
            {
                _orderQty = value;
                UpdateFieldValue("OrderQty", value);
            }
        }
        
        public decimal? Focqty
        {
            get
            {
                return _focqty;
            }
            set
            {
                _focqty = value;
                UpdateFieldValue("Focqty", value);
            }
        }
        
        public bool? ChangeSellingPrice
        {
            get
            {
                return _changeSellingPrice;
            }
            set
            {
                _changeSellingPrice = value;
                UpdateFieldValue("ChangeSellingPrice", value);
            }
        }
        
        public bool? ChangeBasePrice
        {
            get
            {
                return _changeBasePrice;
            }
            set
            {
                _changeBasePrice = value;
                UpdateFieldValue("ChangeBasePrice", value);
            }
        }
        
        public decimal? ProposedBasePrice
        {
            get
            {
                return _proposedBasePrice;
            }
            set
            {
                _proposedBasePrice = value;
                UpdateFieldValue("ProposedBasePrice", value);
            }
        }
        
        public decimal? ProposedSellingPrice
        {
            get
            {
                return _proposedSellingPrice;
            }
            set
            {
                _proposedSellingPrice = value;
                UpdateFieldValue("ProposedSellingPrice", value);
            }
        }
        
        public decimal? Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                UpdateFieldValue("Amount", value);
            }
        }
        
        public decimal? CtnConv
        {
            get
            {
                return _ctnConv;
            }
            set
            {
                _ctnConv = value;
                UpdateFieldValue("CtnConv", value);
            }
        }
        
        public decimal? ProposedAmount
        {
            get
            {
                return _proposedAmount;
            }
            set
            {
                _proposedAmount = value;
                UpdateFieldValue("ProposedAmount", value);
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
        
        public DateTime? OHShipByDate
        {
            get
            {
                return _oHShipByDate;
            }
            set
            {
                _oHShipByDate = value;
                UpdateFieldValue("OHShipByDate", value);
            }
        }
        
        public System.Guid? OHShipToSysRowID
        {
            get
            {
                return _oHShipToSysRowID;
            }
            set
            {
                _oHShipToSysRowID = value;
                UpdateFieldValue("OHShipToSysRowID", value);
            }
        }
        
        public string OHShipToName
        {
            get
            {
                return _oHShipToName;
            }
            set
            {
                _oHShipToName = value;
                UpdateFieldValue("OHShipToName", value);
            }
        }
        
        public decimal? SellingPricePerBottle
        {
            get
            {
                return _sellingPricePerBottle;
            }
            set
            {
                _sellingPricePerBottle = value;
                UpdateFieldValue("SellingPricePerBottle", value);
            }
        }
        
        public decimal? BasePricePerBottle
        {
            get
            {
                return _basePricePerBottle;
            }
            set
            {
                _basePricePerBottle = value;
                UpdateFieldValue("BasePricePerBottle", value);
            }
        }
        
        public decimal? ProposedSellingPricePerBottle
        {
            get
            {
                return _proposedSellingPricePerBottle;
            }
            set
            {
                _proposedSellingPricePerBottle = value;
                UpdateFieldValue("ProposedSellingPricePerBottle", value);
            }
        }
        
        public decimal? ProposedBasePricePerBottle
        {
            get
            {
                return _proposedBasePricePerBottle;
            }
            set
            {
                _proposedBasePricePerBottle = value;
                UpdateFieldValue("ProposedBasePricePerBottle", value);
            }
        }
        
        public FieldValue this[OrderDtlVwDataField field]
        {
            get
            {
                return this[field.ToString()];
            }
        }
    }
}
