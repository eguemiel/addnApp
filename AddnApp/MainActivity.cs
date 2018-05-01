using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Views;

namespace AddnApp
{
    [Activity(Label = "AddnApp",  Theme = "@style/AppTheme")]
    public class MainActivity : ADDNAbstractActivity
    {
        public ViewGroup Loading { get; set; }
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        private Android.Support.V7.Widget.Toolbar toolbar;
        private Spinner toolbarSpinner;
        private TableRow toolbarControls;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Loading = FindViewById<ViewGroup>(Resource.Id.loading);
            Loading.BringToFront();
            toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            toolbarSpinner = FindViewById<Spinner>(Resource.Id.toolbar_spinner);
            toolbarControls = FindViewById<TableRow>(Resource.Id.toolbar_controls);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override bool OnMenuOpened(int featureId, IMenu menu)
        {
            return base.OnMenuOpened(featureId, menu);
        }

    }
}

