using Framework.AddApp.Mobile.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.AddApp.Mobile.ApiModels
{
    public class ObservationRequest : BaseRequest
    {
        public int Registro { get; set; }
        public int Item { get; set; }
        public int IdObservacao { get; set; }
        public object Texto { get; set; }
        public object Usuario { get; set; }
    }
}
