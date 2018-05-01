using AddnApp.Base;
using Android.App;

namespace AddnApp.Cadastro
{
    public interface IAddnAppActivity
    {
        void SetTitle(string value);

        FragmentManager GetFragmentManager();

        TFragment Navigate<TFragment>() where TFragment : BaseFragment, new();

        TFragment Navigate<TFragment>(TFragment fragment) where TFragment : BaseFragment, new();
    }
}