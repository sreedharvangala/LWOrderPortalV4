using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Finsoft.Data;

namespace Finsoft.Models
{
	public enum OrderStatusLogV2ApprovalDataField
    {
        
        RejectedRemarks,
        
        RejectedReason,
    }
    
    public partial class OrderStatusLogV2ApprovalModel : BusinessRulesObjectModel
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _rejectedRemarks;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _rejectedReason;
        
        public OrderStatusLogV2ApprovalModel()
        {
        }
        
        public OrderStatusLogV2ApprovalModel(BusinessRules r) : 
                base(r)
        {
        }
        
        public string RejectedRemarks
        {
            get
            {
                return _rejectedRemarks;
            }
            set
            {
                _rejectedRemarks = value;
                UpdateFieldValue("RejectedRemarks", value);
            }
        }
        
        public string RejectedReason
        {
            get
            {
                return _rejectedReason;
            }
            set
            {
                _rejectedReason = value;
                UpdateFieldValue("RejectedReason", value);
            }
        }
        
        public FieldValue this[OrderStatusLogV2ApprovalDataField field]
        {
            get
            {
                return this[field.ToString()];
            }
        }
    }
}
