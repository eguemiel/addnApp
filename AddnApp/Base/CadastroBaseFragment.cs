using AddnApp.Base;
using AddnApp.Interfaces;
using Android.Content;

namespace AddnApp.Cadastro
{
    public abstract class CadastroBaseFragment : BaseFragment
    {
        protected IAddnAppActivity currentActivity;

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            currentActivity = (IAddnAppActivity)context;
        }

        protected abstract string FragmentTitle { get; }


        public void SetMainTitle()
        {
            if (!string.IsNullOrEmpty(FragmentTitle) && currentActivity != null)
            {
                currentActivity.SetTitle(FragmentTitle);
            }
        }
    }
}