using Android.Support.V7.App;
using AddnApp.Base;
using AddnApp.Interfaces;
using Android.App;
using Android.Views;

namespace AddnApp
{

    public abstract class ADDNAbstractActivity : AppCompatActivity, IAddnAppActivity
    {
        public ViewGroup Loading { get; set; }

        public FragmentManager GetFragmentManager()
        {
            throw new System.NotImplementedException();
        }

        public TFragment Navigate<TFragment>() where TFragment : BaseFragment, new()
        {
            return Navigate(new TFragment());
        }

        public TFragment Navigate<TFragment>(TFragment fragment) where TFragment : BaseFragment, new()
        {
            var tx = SupportFragmentManager.BeginTransaction();
            tx.Replace(Resource.Id.main_container, fragment);
            tx.AddToBackStack(fragment.GetType().Name);
            tx.Commit();

            return fragment;
        }

        public void SetTitle(string value)
        {
            this.Title = value;
        }       

        public void ShowLoading()
        {
            RunOnUiThread(() =>
            Loading.Visibility = ViewStates.Visible);
        }

        public void HideLoading()
        {
            RunOnUiThread(() =>
            Loading.Visibility = ViewStates.Gone);
        }
    }
}