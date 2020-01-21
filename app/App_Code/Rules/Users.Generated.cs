﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Finsoft.Data;

namespace Finsoft.Rules
{
	public partial class UsersBusinessRules : Finsoft.Rules.SharedBusinessRules
    {
        
        [RowBuilder("Users", RowKind.New)]
        public void BuildNewUsers()
        {
            UpdateFieldValue("UserActivation", true);
        }
    }
}
