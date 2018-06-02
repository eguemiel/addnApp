using Android.Support.V7.App;
using AddnApp.Base;
using AddnApp.Interfaces;
using Android.App;
using Android.Views;
using Android.Widget;
using AddnApp.Base.Enums;
using Android.Graphics;
using Android.Views.InputMethods;
using Android.Content;

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

        public void ShowMessage(string message, ToastLength length = ToastLength.Short, ToastMessageType toastMessageType = ToastMessageType.InfoBlue)
        {
            RunOnUiThread(() =>
            {
                View layout = LayoutInflater.Inflate(Resource.Layout.toast,
                               FindViewById<ViewGroup>(Resource.Id.toast_layout_root));

                ImageView image = layout.FindViewById<ImageView>(Resource.Id.image);
                TextView text = layout.FindViewById<TextView>(Resource.Id.text);
                text.Text = message;

                switch (toastMessageType)
                {
                    case ToastMessageType.InfoWhite:
                        layout.SetBackgroundColor(Color.ParseColor(ApplicationContext.GetString(Resource.Color.toastInfoWhite)));
                        image.SetImageResource(Resource.Drawable.ic_info_black_24dp);
                        image.SetColorFilter(Color.ParseColor(ApplicationContext.GetString(Resource.Color.black)));
                        text.SetTextColor(Color.ParseColor(ApplicationContext.GetString(Resource.Color.black)));
                        break;
                    case ToastMessageType.InfoBlue:
                        layout.SetBackgroundColor(Color.ParseColor(ApplicationContext.GetString(Resource.Color.toastInfoBlue)));
                        text.SetTextColor(Color.ParseColor(ApplicationContext.GetString(Resource.Color.white)));
                        image.SetImageResource(Resource.Drawable.ic_info_black_24dp);
                        break;
                    case ToastMessageType.Error:
                        layout.SetBackgroundColor(Color.ParseColor(ApplicationContext.GetString(Resource.Color.toastError)));
                        text.SetTextColor(Color.ParseColor(ApplicationContext.GetString(Resource.Color.white)));
                        image.SetImageResource(Resource.Drawable.ic_info_black_24dp);
                        break;
                    case ToastMessageType.Warning:
                        layout.SetBackgroundColor(Color.ParseColor(ApplicationContext.GetString(Resource.Color.toastWarning)));
                        image.SetImageResource(Resource.Drawable.ic_warning_black_24dp);
                        text.SetTextColor(Color.ParseColor(ApplicationContext.GetString(Resource.Color.white)));
                        break;
                    case ToastMessageType.Success:
                        layout.SetBackgroundColor(Color.ParseColor(ApplicationContext.GetString(Resource.Color.toastSuccess)));
                        text.SetTextColor(Color.ParseColor(ApplicationContext.GetString(Resource.Color.white)));
                        image.SetImageResource(Resource.Drawable.ic_done_black_24dp);
                        break;
                    default:
                        break;
                }

                Toast toast = new Toast(ApplicationContext);
                toast.SetGravity(GravityFlags.FillHorizontal | GravityFlags.Top, 0, 0);
                toast.Duration = length;
                toast.View = layout;
                toast.Show();
            });
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

        public void CloseSoftKeyobard()
        {
            View view = this.CurrentFocus;
            if (view != null)
            {
                InputMethodManager imm = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
            }
        }
    }
}