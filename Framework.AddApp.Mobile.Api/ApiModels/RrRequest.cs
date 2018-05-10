using Framework.AddApp.Mobile.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.AddApp.Mobile.ApiModels
{
    public class RrRequest : BaseRequest
    {
        public int Registro { get; set; }
        public int Item { get; set; }
    }
}
