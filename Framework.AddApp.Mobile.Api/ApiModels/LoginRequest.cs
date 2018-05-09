using Framework.AddApp.Mobile.Api;

namespace Framework.AddApp.Mobile.ApiModels
{
    public class LoginRequest : BaseRequest
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}