using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Finsoft.Data;

namespace Finsoft.Rules
{
	public partial class PartBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        protected override void EnumerateDynamicAccessControlRules(string controllerName)
        {
            //FieldValue orderId = SelectExternalFilterFieldValueObject(
            //    "ContextFields_ExOrderType");
            
        }
        
    }
}
