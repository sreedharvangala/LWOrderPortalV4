using System;
using System.Collections;
using System.Collections.Generic;

namespace Finsoft.Data
{
	[Serializable]
    public class FieldValue
    {
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _name;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private object _oldValue;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private object _newValue;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool _noCheck;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool _modified;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool _readOnly;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _error;
        
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool _enableConversion = true;
        
        public FieldValue()
        {
        }
        
        public FieldValue(string fieldName)
        {
            this._name = fieldName;
        }
        
        public FieldValue(string fieldName, object newValue) : 
                this(fieldName, null, newValue)
        {
        }
        
        public FieldValue(string fieldName, object oldValue, object newValue)
        {
            this._name = fieldName;
            this._oldValue = oldValue;
            this._newValue = newValue;
            CheckModified();
        }
        
        public FieldValue(string fieldName, object oldValue, object newValue, bool readOnly) : 
                this(fieldName, oldValue, newValue)
        {
            this._readOnly = readOnly;
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
            }
        }
        
        public object OldValue
        {
            get
            {
                return _oldValue;
            }
            set
            {
                if (_enableConversion && (value is string))
                	_oldValue = Controller.StringToValue(((string)(value)));
                else
                	_oldValue = value;
            }
        }
        
        public object NewValue
        {
            get
            {
                return _newValue;
            }
            set
            {
                if (_enableConversion && (value is string))
                	_newValue = Controller.StringToValue(((string)(value)));
                else
                	_newValue = value;
            }
        }
        
        public bool Modified
        {
            get
            {
                return (_modified && (_noCheck || !ReadOnly));
            }
            set
            {
                _modified = value;
                _noCheck = true;
            }
        }
        
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
            }
        }
        
        public object Value
        {
            get
            {
                CheckModified();
                if (Modified)
                	return NewValue;
                else
                	return OldValue;
            }
            set
            {
                OldValue = value;
                Modified = false;
            }
        }
        
        public string Error
        {
            get
            {
                return this._error;
            }
            set
            {
                this._error = value;
            }
        }
        
        public override string ToString()
        {
            var oldValueInfo = string.Empty;
            var v = Value;
            if (this.Modified)
            {
                var ov = OldValue;
                if (ov == null)
                	ov = "null";
                oldValueInfo = string.Format(" (old value = {0})", ov);
            }
            var isReadOnly = string.Empty;
            if (ReadOnly)
            	isReadOnly = " (read-only)";
            if (v == null)
            	v = "null";
            var err = string.Empty;
            if (!(string.IsNullOrEmpty(Error)))
            	err = string.Format("; Input Error: {0}", Error);
            return string.Format(string.Format("{0} = {1}{2}{3}{4}", Name, v, oldValueInfo, isReadOnly, err));
        }
        
        public void CheckModified()
        {
            if (_noCheck)
            	return;
            if (string.Empty.Equals(NewValue))
            	NewValue = null;
            if (NewValue == null)
            	if (OldValue != null)
                	_modified = true;
                else
                	_modified = false;
            else
            	if (OldValue != null)
                	_modified = !(NewValue.Equals(OldValue));
                else
                	_modified = true;
        }
        
        public void AssignTo(object instance)
        {
            CheckModified();
            var t = instance.GetType();
            var propInfo = t.GetProperty(Name);
            var v = Value;
            if (v != null)
            	if (propInfo.PropertyType.IsGenericType)
                	if (propInfo.PropertyType.GetProperty("Value").PropertyType.Equals(typeof(Guid)))
                    	v = new Guid(Convert.ToString(v));
                    else
                    	v = Convert.ChangeType(v, propInfo.PropertyType.GetProperty("Value").PropertyType);
                else
                	v = Convert.ChangeType(v, propInfo.PropertyType);
            t.InvokeMember(Name, System.Reflection.BindingFlags.SetProperty, null, instance, new object[] {
                        v});
        }
        
        public void EnableConversion()
        {
            _enableConversion = true;
        }
        
        public void DisableConversion()
        {
            _enableConversion = false;
        }
    }
    
    public class FieldValueDictionary : SortedDictionary<string, FieldValue>
    {
        
        public FieldValueDictionary()
        {
        }
        
        public FieldValueDictionary(ActionArgs args)
        {
            if (args.Values != null)
            	AddRange(args.Values);
        }
        
        public FieldValueDictionary(List<FieldValue> values)
        {
            if (values != null)
            	AddRange(values.ToArray());
        }
        
        public FieldValueDictionary(FieldValue[] values)
        {
            if (values != null)
            	AddRange(values);
        }
        
        public void AddRange(FieldValue[] values)
        {
            foreach (var fvo in values)
            	this[fvo.Name] = fvo;
        }
        
        public void Assign(IDictionary values, bool assignToNewValues)
        {
            foreach (string fieldName in values.Keys)
            {
                if (!(ContainsKey(fieldName)))
                	Add(fieldName, new FieldValue(fieldName));
                var v = this[fieldName];
                if (assignToNewValues)
                {
                    v.NewValue = values[fieldName];
                    v.CheckModified();
                }
                else
                	v.OldValue = values[fieldName];
            }
        }
    }
}
