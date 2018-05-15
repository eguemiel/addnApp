using System;
using System.Collections.Generic;
using System.IO;

namespace Framework.AddApp.Mobile.Api.Configuration
{
    public abstract class ConfigurationBase
    {
        public static ConfigurationBase Instance { get; protected set; }

        public string ApiUrl { get; private set; }
        public string UserId { get; private set; }
        public string Password { get; private set; }

        protected abstract void SetValues(IDictionary<string, string> values);

        public void ReadFromXml(Stream stream)
        {
            var doc = new System.Xml.XmlDocument();
            doc.Load(stream);

            var dic = new Dictionary<string, string>();
            var adds = doc.GetElementsByTagName("add");
            for (var i = 0; i < adds.Count; i++)
            {
                var add = adds.Item(i);
                dic.Add(add.Attributes["key"].Value, add.Attributes["value"].Value);
            }

            SetThisValues(dic);
        }

        private void SetThisValues(IDictionary<string, string> values)
        {
            ApiUrl = ReadString(values, "ApiUrl");
            UserId = ReadString(values, "UserId");
            Password = ReadString(values, "Password");

            SetValues(values);
        }

        protected string ReadString(IDictionary<string, string> dic, string key, bool required = true)
        {
            if (dic.ContainsKey(key))
            {
                return dic[key];
            }
            else
            {
                if (required)
                {
                    throw new Exception($"Missing required configuration! Key: \"{key}\"");
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
