using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.AddApp.Mobile.Api.Configuration
{
    public class AddnAppConfiguration : ConfigurationBase
    {
        private static AddnAppConfiguration _instance;

        protected new static AddnAppConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AddnAppConfiguration();
                    ConfigurationBase.Instance = _instance;
                }
                return _instance;
            }
        }

        protected override void SetValues(IDictionary<string, string> values)
        {
            
        }
    }
}
