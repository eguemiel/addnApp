using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.AddApp.Mobile.Api
{
    public abstract class BaseApi<T> where T:class, new ()
    {
        public static event EventHandler Unauthorized;
        public static T Instance { get; private set; }

        protected abstract string GetApiName();

        static BaseApi()
        {
            Instance = new T();
        }

        private string GetFullUri(string methodName)
        {
            return string.Format(@"{0}/{1}/{2}", null);
        }

        private void HandleError(ITransport sender, int statusCode)
        {
            switch (statusCode)
            {
                case 401: Unauthorized?.Invoke(null, new EventArgs()); break;
            }
        }

        protected TResponse Get<TResponse>(string methodName)
        {
            return Settings.Transport
                .Request(GetFullUri(methodName), "GET")
                .OnError(HandleError)
                .Response<TResponse>();
        }

        protected TResponse Post<TResponse>(string methodName)
        {
            return Settings.Transport
                .Request(GetFullUri(methodName), "POST")
                .OnError(HandleError)
                .Response<TResponse>();
        }

        protected TResponse Post<TRequest, TResponse>(string methodName, TRequest body) where TRequest : class
        {
            return Settings.Transport
                .Request(GetFullUri(methodName), "POST")
                .OnError(HandleError)
                .WithBody(body)
                .Response<TResponse>();
        }
    }
}
