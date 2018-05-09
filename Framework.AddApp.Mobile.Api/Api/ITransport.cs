using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.AddApp.Mobile.Api
{
    public delegate void TransportErrorHandlerDelegate(ITransport sender, int statusCode);

    public interface ITransport
    {
        ITransport OnError(TransportErrorHandlerDelegate errorHandler);
        ITransport Request(string uri, string httpMethod);
        ITransport WithBody<T>(T body);
        T Response<T>();
    }
}
