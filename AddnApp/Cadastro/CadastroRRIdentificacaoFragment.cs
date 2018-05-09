using Android.OS;
using Android.Views;
using AddnApp.Base;
using AddnApp.Helpers;

namespace AddnApp.Cadastro
{
    public class CadastroRRIdentificacaoFragment : BaseWizardStepFragment
    {
        protected override int ViewId => throw new System.Exception();        

        public static CadastroRRIdentificacaoFragment getInstance()
        {
            return new CadastroRRIdentificacaoFragment();
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
           
        }     

        private void ProcessTag()
        {   

            var task = new GenericTask()
                .WithPreExecuteProcess((b) =>
                {
                   
                }).WithBackGroundProcess((b, t) =>
                {
                   
                }).WithPosExecuteProcess((b, t) =>
                {
                    
                }).Execute();
        }
    }
}