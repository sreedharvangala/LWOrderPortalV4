using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Finsoft.Data;

namespace Finsoft.Models
{
	public enum OrderStatusLogV2DataField
    {
    }
    
    public partial class OrderStatusLogV2Model : BusinessRulesObjectModel
    {
        
        public OrderStatusLogV2Model()
        {
        }
        
        public OrderStatusLogV2Model(BusinessRules r) : 
                base(r)
        {
        }
        
        public FieldValue this[OrderStatusLogV2DataField field]
        {
            get
            {
                return this[field.ToString()];
            }
        }
    }
}
