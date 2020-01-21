using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Finsoft.Data;

namespace Finsoft.Models
{
	public enum UsersDataField
    {
        
        UserID,
        
        UserName,
        
        Password,
        
        FullName,
        
        Email,
        
        ManagerID,
        
        ManagerUserName,
        
        Phone,
        
        UserActivation,
        
        Roles,
        
        Company,
    }
    
    public partial class UsersModel : BusinessRulesObjectModel
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _userID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _userName;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _password;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _fullName;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _email;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _managerID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _managerUserName;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _phone;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool? _userActivation;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _roles;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _company;
        
        public UsersModel()
        {
        }
        
        public UsersModel(BusinessRules r) : 
                base(r)
        {
        }
        
        public int? UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
                UpdateFieldValue("UserID", value);
            }
        }
        
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                UpdateFieldValue("UserName", value);
            }
        }
        
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                UpdateFieldValue("Password", value);
            }
        }
        
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
                UpdateFieldValue("FullName", value);
            }
        }
        
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                UpdateFieldValue("Email", value);
            }
        }
        
        public int? ManagerID
        {
            get
            {
                return _managerID;
            }
            set
            {
                _managerID = value;
                UpdateFieldValue("ManagerID", value);
            }
        }
        
        public string ManagerUserName
        {
            get
            {
                return _managerUserName;
            }
            set
            {
                _managerUserName = value;
                UpdateFieldValue("ManagerUserName", value);
            }
        }
        
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                UpdateFieldValue("Phone", value);
            }
        }
        
        public bool? UserActivation
        {
            get
            {
                return _userActivation;
            }
            set
            {
                _userActivation = value;
                UpdateFieldValue("UserActivation", value);
            }
        }
        
        public string Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
                UpdateFieldValue("Roles", value);
            }
        }
        
        public string Company
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
                UpdateFieldValue("Company", value);
            }
        }
        
        public FieldValue this[UsersDataField field]
        {
            get
            {
                return this[field.ToString()];
            }
        }
    }
}
