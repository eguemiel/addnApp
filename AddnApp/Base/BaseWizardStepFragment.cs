using Android.App;
using System;

namespace AddnApp.Base
{
    public class BaseWizardStepFragment : BaseFragment
    {
        public object Data { get; set; }
        public BaseWizardFragment Wizard { get; set; }

        protected override int ViewId => throw new NotImplementedException();

        public virtual bool IsValid()
        {
            return true;
        }

        public virtual void BeforeContinue() { }

        public void ShowValidAlert(string msg, Action ok = null)
        {
            var alert = new AlertDialog.Builder(Context);
            alert.SetMessage(msg);
            alert.SetPositiveButton("Ok", (s, a) => {
                ok?.Invoke();
            });
            Dialog dialog = alert.Create();
            dialog.Show();
        }
    }
}