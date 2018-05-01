using Android.OS;
using Android.Views;
using System;
using v4app = Android.Support.V4.App;

namespace AddnApp.Base
{
    public abstract class BaseFragment : v4app.Fragment
    {
        protected abstract int ViewId { get; }

        public v4app.FragmentTransaction Transaction { get; set; }

        public virtual void LoadData(Action callBack)
        {
            callBack();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(ViewId, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {

        }

        public TView FindViewById<TView>(int id) where TView : View
        {
            return View.FindViewById<TView>(id);
        }

    }
}