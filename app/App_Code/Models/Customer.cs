using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Finsoft.Data;

namespace Finsoft.Models
{
	public enum CustomerDataField
    {
        
        SysRowID,
        
        Company,
        
        CustID,
        
        CustNum,
        
        Name,
        
        Address1,
        
        Address2,
        
        Address3,
        
        City,
        
        State,
        
        Zip,
        
        Country,
        
        CreditLimit,
        
        Balance,
        
        CreditLimit2,
        
        UnPaidInvoices,
        
        OutStandingOrders,
        
        UnPostedInvoices,
        
        AgeCurr,
        
        Age30,
        
        Age60,
        
        Age90,
        
        Age120,
        
        Age150,
        
        TermsExceed,
        
        SalesRepCode,
        
        PhoneNum,
        
        FaxNum,
    }
    
    public partial class CustomerModel : BusinessRulesObjectModel
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private System.Guid? _sysRowID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _company;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _custID;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int? _custNum;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _name;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _address1;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _address2;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _address3;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _city;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _state;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _zip;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _country;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _creditLimit;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _balance;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _creditLimit2;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _unPaidInvoices;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _outStandingOrders;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _unPostedInvoices;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _ageCurr;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _age30;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _age60;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _age90;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _age120;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private decimal? _age150;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool? _termsExceed;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _salesRepCode;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _phoneNum;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _faxNum;
        
        public CustomerModel()
        {
        }
        
        public CustomerModel(BusinessRules r) : 
                base(r)
        {
        }
        
        public System.Guid? SysRowID
        {
            get
            {
                return _sysRowID;
            }
            set
            {
                _sysRowID = value;
                UpdateFieldValue("SysRowID", value);
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
        
        public string CustID
        {
            get
            {
                return _custID;
            }
            set
            {
                _custID = value;
                UpdateFieldValue("CustID", value);
            }
        }
        
        public int? CustNum
        {
            get
            {
                return _custNum;
            }
            set
            {
                _custNum = value;
                UpdateFieldValue("CustNum", value);
            }
        }
        
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                UpdateFieldValue("Name", value);
            }
        }
        
        public string Address1
        {
            get
            {
                return _address1;
            }
            set
            {
                _address1 = value;
                UpdateFieldValue("Address1", value);
            }
        }
        
        public string Address2
        {
            get
            {
                return _address2;
            }
            set
            {
                _address2 = value;
                UpdateFieldValue("Address2", value);
            }
        }
        
        public string Address3
        {
            get
            {
                return _address3;
            }
            set
            {
                _address3 = value;
                UpdateFieldValue("Address3", value);
            }
        }
        
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                UpdateFieldValue("City", value);
            }
        }
        
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                UpdateFieldValue("State", value);
            }
        }
        
        public string Zip
        {
            get
            {
                return _zip;
            }
            set
            {
                _zip = value;
                UpdateFieldValue("Zip", value);
            }
        }
        
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                UpdateFieldValue("Country", value);
            }
        }
        
        public decimal? CreditLimit
        {
            get
            {
                return _creditLimit;
            }
            set
            {
                _creditLimit = value;
                UpdateFieldValue("CreditLimit", value);
            }
        }
        
        public decimal? Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                UpdateFieldValue("Balance", value);
            }
        }
        
        public decimal? CreditLimit2
        {
            get
            {
                return _creditLimit2;
            }
            set
            {
                _creditLimit2 = value;
                UpdateFieldValue("CreditLimit2", value);
            }
        }
        
        public decimal? UnPaidInvoices
        {
            get
            {
                return _unPaidInvoices;
            }
            set
            {
                _unPaidInvoices = value;
                UpdateFieldValue("UnPaidInvoices", value);
            }
        }
        
        public decimal? OutStandingOrders
        {
            get
            {
                return _outStandingOrders;
            }
            set
            {
                _outStandingOrders = value;
                UpdateFieldValue("OutStandingOrders", value);
            }
        }
        
        public decimal? UnPostedInvoices
        {
            get
            {
                return _unPostedInvoices;
            }
            set
            {
                _unPostedInvoices = value;
                UpdateFieldValue("UnPostedInvoices", value);
            }
        }
        
        public decimal? AgeCurr
        {
            get
            {
                return _ageCurr;
            }
            set
            {
                _ageCurr = value;
                UpdateFieldValue("AgeCurr", value);
            }
        }
        
        public decimal? Age30
        {
            get
            {
                return _age30;
            }
            set
            {
                _age30 = value;
                UpdateFieldValue("Age30", value);
            }
        }
        
        public decimal? Age60
        {
            get
            {
                return _age60;
            }
            set
            {
                _age60 = value;
                UpdateFieldValue("Age60", value);
            }
        }
        
        public decimal? Age90
        {
            get
            {
                return _age90;
            }
            set
            {
                _age90 = value;
                UpdateFieldValue("Age90", value);
            }
        }
        
        public decimal? Age120
        {
            get
            {
                return _age120;
            }
            set
            {
                _age120 = value;
                UpdateFieldValue("Age120", value);
            }
        }
        
        public decimal? Age150
        {
            get
            {
                return _age150;
            }
            set
            {
                _age150 = value;
                UpdateFieldValue("Age150", value);
            }
        }
        
        public bool? TermsExceed
        {
            get
            {
                return _termsExceed;
            }
            set
            {
                _termsExceed = value;
                UpdateFieldValue("TermsExceed", value);
            }
        }
        
        public string SalesRepCode
        {
            get
            {
                return _salesRepCode;
            }
            set
            {
                _salesRepCode = value;
                UpdateFieldValue("SalesRepCode", value);
            }
        }
        
        public string PhoneNum
        {
            get
            {
                return _phoneNum;
            }
            set
            {
                _phoneNum = value;
                UpdateFieldValue("PhoneNum", value);
            }
        }
        
        public string FaxNum
        {
            get
            {
                return _faxNum;
            }
            set
            {
                _faxNum = value;
                UpdateFieldValue("FaxNum", value);
            }
        }
        
        public FieldValue this[CustomerDataField field]
        {
            get
            {
                return this[field.ToString()];
            }
        }
    }
}
