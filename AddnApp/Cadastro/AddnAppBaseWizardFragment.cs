using Android.Content;

namespace AddnApp.Cadastro
{
    public abstract class AddnAppBaseWizardFragment : BaseWizardFragment
    {
        protected IAddnAppActivity currentActivity;

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            try
            {
                currentActivity = (IAddnAppActivity)context;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        protected abstract string FragmentTitle { get; }

        public void SetMainTitle()
        {
            if (!string.IsNullOrEmpty(FragmentTitle) && currentActivity != null)
            {
                currentActivity.SetTitle(FragmentTitle);
            }
        }

        public override void OnDone()
        {
            throw new System.NotImplementedException();
        }
    }
}