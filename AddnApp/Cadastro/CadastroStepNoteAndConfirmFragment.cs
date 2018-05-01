using AddnApp.Base;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace AddnApp.Cadastro
{
    public class DamageStepNoteAndConfirmFragment : BaseWizardStepFragment
    {
<<<<<<< .merge_file_a13580
        protected override int ViewId => Resource.Layout.Damage_Step_NoteAndConfirm_Fragment;
=======
        protected override int ViewId => Resource.Layout.CadastroRR_Confirmar_Fragment;
>>>>>>> .merge_file_a13492

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