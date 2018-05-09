using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.AddApp.Mobile.Api
{
    public class Headers
    {
        private IDictionary<string, string> _values;

        public Headers()
        {
            _values = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Values
        {
            get
            {
                return _values;
            }
        }

        private void SetKey(string key, string value)
        {
            if (_values.ContainsKey(key))
            {
                _values[key] = value;
            }
            else
            {
                _values.Add(key, value);
            }
        }
    }
}
