using AddnApp.Base;
using Android.App;
using Android.OS;
using Android.Views;

namespace AddnApp
{
    [Activity(Label = "HomeFragment")]
    public class HomeFragment : BaseFragment
    {
        protected override int ViewId { get { return Resource.Layout.Home; } }

        public string Title
        {
            get
            {
                return "Home";
            }
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            Program.Main.SetTitle(Title);

        }
    }
}