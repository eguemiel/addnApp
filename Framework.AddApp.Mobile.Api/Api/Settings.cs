using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Framework.AddApp.Mobile.Api
{
    public class Settings
    {
        public static Headers Headers { get; set; }
        public static JsonSerializerSettings JsonSettings { get; set; }
        public static Type TransportType { get; set; }

        static Settings()
        {
            Headers = new Headers();

            JsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public static ITransport Transport
        {
            get
            {
                if (TransportType == null)
                {
                    throw new Exception("Need to set \"Settings.TransportType\" on the start of the app!");
                }

                var transport = Activator.CreateInstance(TransportType) as ITransport;

                if (transport == null)
                {
                    throw new Exception("The type \"Settings.TransportType\" need to implement the interface \"TTI.Framework.Mobile.Api.ITransport\"!");
                }

                return transport;
            }
        }
    }
}
