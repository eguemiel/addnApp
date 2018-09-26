using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AddnApp.Base;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using TiagoSantos.EnchantedViewPager;

namespace AddnApp.Cadastro
{
    [Activity(Label = "ImagesViewActivity", Theme = "@style/AppTheme")]
    public class ImagesViewActivity : AppCompatActivity
    {
        protected List<Bitmap> bitmap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CadastroRR_Images_View);

            bitmap = CadastroRRIdentificacaoFragment.ListBitmapCadastroRR;

            EnchantedViewPager imageView = FindViewById<EnchantedViewPager>(Resource.Id.cadastroRRImagesView);
            
            ImagesViewAdapter adapter = new ImagesViewAdapter(this, bitmap);
            imageView.Adapter = adapter;
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