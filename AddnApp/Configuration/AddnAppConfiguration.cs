using Framework.AddApp.Mobile.Api.Configuration;
using System.Collections.Generic;

namespace AddApp.Configuration
{
    public class AddnAppConfiguration : ConfigurationBase
    {
        private static AddnAppConfiguration _instance;
        public new static AddnAppConfiguration Instance
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
