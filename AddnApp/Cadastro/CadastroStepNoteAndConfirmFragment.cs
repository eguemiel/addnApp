using Android.OS;
using Android.Views;
using Android.Widget;
using TTI.Framework.Mobile.Android.Base;
using TTI.Nexlog.Collector.Entities;

namespace AddnApp.Cadastro
{
    public class DamageStepNoteAndConfirmFragment : BaseWizardStepFragment
    {
        protected override int ViewId => Resource.Layout.Damage_Step_NoteAndConfirm_Fragment;

        private EditText txtDamageNote;

        public DamageItem Item { get { return Data as DamageItem; } }

        public static DamageStepNoteAndConfirmFragment getInstance()
        {
            return new DamageStepNoteAndConfirmFragment();
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            txtDamageNote = FindViewById<EditText>(Resource.Volume.damage_txtNote);
            txtDamageNote.TextChanged += TxtDamageNote_TextChanged;
            

            if (Item != null)
                txtDamageNote.Text = Item.DamageNote;
        }

        private void TxtDamageNote_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var editText = (EditText)sender;
            Item.DamageNote = editText.Text;
        }
    }
}