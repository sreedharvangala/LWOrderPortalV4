using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Finsoft.Data;

namespace Finsoft.Models
{
	public enum OrderStatusLogV2Lv2DataField
    {
        
        Status,
        
        UserComment,
    }
    
    public partial class OrderStatusLogV2Lv2Model : BusinessRulesObjectModel
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _status;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _userComment;
        
        public OrderStatusLogV2Lv2Model()
        {
        }
        
        public OrderStatusLogV2Lv2Model(BusinessRules r) : 
                base(r)
        {
        }
        
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                UpdateFieldValue("Status", value);
            }
        }
        
        public string UserComment
        {
            get
            {
                return _userComment;
            }
            set
            {
                _userComment = value;
                UpdateFieldValue("UserComment", value);
            }
        }
        
        public FieldValue this[OrderStatusLogV2Lv2DataField field]
        {
            get
            {
                return this[field.ToString()];
            }
        }
    }
}
