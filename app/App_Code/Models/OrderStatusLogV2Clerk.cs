using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Finsoft.Data;

namespace Finsoft.Models
{
	public enum OrderStatusLogV2ClerkDataField
    {
        
        Status,
    }
    
    public partial class OrderStatusLogV2ClerkModel : BusinessRulesObjectModel
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _status;
        
        public OrderStatusLogV2ClerkModel()
        {
        }
        
        public OrderStatusLogV2ClerkModel(BusinessRules r) : 
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
        
        public FieldValue this[OrderStatusLogV2ClerkDataField field]
        {
            get
            {
                return this[field.ToString()];
            }
        }
    }
}
