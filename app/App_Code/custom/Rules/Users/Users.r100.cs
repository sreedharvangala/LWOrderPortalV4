using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Finsoft.Data;
using Finsoft.Models;
using Finsoft.Security;

namespace Finsoft.Rules
{
	public partial class UsersBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        /// <summary>This method will execute in any view for an action
        /// with a command name that matches "Insert|Update".
        /// </summary>
        [Rule("r100")]
        public void r100Implementation(UsersModel instance)
        {
            // This is the placeholder for method implementation.
           instance.Password= ApplicationMembershipProvider.EncodeUserPassword(instance.Password);
        }
    }
}
