using Framework.AddApp.Mobile.Api;
using Framework.AddApp.Mobile.ApiModels;
using Framework.AddApp.Mobile.OracleBD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.AddApp.Mobile.ApiClient
{
    public class LoginApi : BaseApi<LoginApi>
    {
        protected override string GetApiName()
        {
            return "Login";
        }

        public LoginResponse SignIn(LoginRequest model)
        {
            var command = string.Format("select * from users where nome = '{0}' and senha = '{1}'", model.User, model.Password);
            var reader = Connection.SelectCommand(command, model.UserId, model.PasswordBD, model.Url);

            return new LoginResponse() { Success = reader != null };
        }
    }
}
