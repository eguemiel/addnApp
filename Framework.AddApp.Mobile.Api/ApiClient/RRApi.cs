using Framework.AddApp.Mobile.Api;
using Framework.AddApp.Mobile.ApiModels;
using Framework.AddApp.Mobile.OracleBD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.AddApp.Mobile.ApiClient
{
    public class RrApi : BaseApi<RrApi>
    {
        protected override string GetApiName()
        {
            throw new NotImplementedException();
        }

        public RrResponse GetRr(RrRequest model)
        {
            var command = string.Format("select a.dataabe,a.numeronf,b.nomcli,c.descprod,b.apecli,b.cidcli," +                                         
                                        "(a.registro||'-'||c.item||'-'||'0') as RR from tdadosgerais a " +
                                        "inner join e085cli@sapprot_orcl b on a.codcli = b.codcli " +
                                        "inner join tequipamentos c on a.registro = c.registro  and c.parte=0 " +
                                        "where a.registro = {0} and c.item = {1}", model.Registro, model.Item);                                        
                  
            var response = RrBD.GetRr(command, model.UserId, model.PasswordBD, model.Url);

            return response;
        }

        
    }
}
