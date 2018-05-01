using AddnApp.Base;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace AddnApp.Cadastro
{
    public class DamageStepNoteAndConfirmFragment : BaseWizardStepFragment
    {
        protected override int ViewId => Resource.Layout.CadastroRR_Confirmar_Fragment;

        private EditText txtDamageNote;

        public static DamageStepNoteAndConfirmFragment getInstance()
        {
            return new DamageStepNoteAndConfirmFragment();
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
        }
    }
}