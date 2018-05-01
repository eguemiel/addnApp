using AddnApp.Base;
using Android.App;

namespace AddnApp.Interfaces
{
    public interface IAddnAppActivity
    {
        void SetTitle(string value);

        FragmentManager GetFragmentManager();

        TFragment Navigate<TFragment>() where TFragment : BaseFragment, new();

        TFragment Navigate<TFragment>(TFragment fragment) where TFragment : BaseFragment, new();
    }
}