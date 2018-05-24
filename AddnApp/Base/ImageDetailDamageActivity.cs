using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;

namespace AddnApp.Cadastro
{
    [Activity(Label = "ImageDetailRRActivity", Theme = "@style/AppTheme")]
    public class ImageDetailRRActivity : AppCompatActivity
    {
        protected Bitmap bitmap, bitmapResized;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CadastroRR_Image_Detail);

            bitmap = CadastroRRImagemFragment.BitmapCadastroRR;

            bitmapResized = bitmap;

            ImageView imageView = FindViewById<ImageView>(Resource.Id.CadastroRR_Image_Detail);
            imageView.SetImageBitmap(bitmapResized);
        }

        public Bitmap Base64ToBitmap(string base64String)
        {
            byte[] imageAsBytes = Base64.Decode(base64String, Base64Flags.Default);
            return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}