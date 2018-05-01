using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using AddnApp.Base;

namespace AddnApp
{
    
    public abstract class ADDNAbstractActivity : AppCompatActivity

    {
        public TFragment Navigate<TFragment>() where TFragment : BaseFragment, new()
        {
            return Navigate(new TFragment());
        }

        private TFragment Navigate<TFragment>(TFragment fragment) where TFragment : BaseFragment, new()
        {
            var tx = SupportFragmentManager.BeginTransaction();
            tx.Replace(Resource.Id.main_container, fragment);
            tx.AddToBackStack(fragment.GetType().Name);
            tx.Commit();

            return fragment;
        }
    }
}