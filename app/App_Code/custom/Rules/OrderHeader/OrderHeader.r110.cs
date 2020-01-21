using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Finsoft.Data;
using Finsoft.Models;

namespace Finsoft.Rules
{
	public partial class OrderHeaderBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Insert|Update".
        /// </summary>
        [Rule("r110")]
        public void r110Implementation(OrderHeaderModel instance)
        {
            // This is the placeholder for method implementation.
            if(!string.IsNullOrEmpty(instance.Promotion))
            {
                string[] strArr= instance.Promotion.Split(',');
                if(strArr.Count()>1)
                {
                    if(strArr.Any(w=> w== "1"))
                    {
                        PreventDefault();
                        Result.ShowAlert("Not Valid Combination, Clear Promotion \"None\" Checkbox.");
                    }
                }
            }
        }
    }
}
