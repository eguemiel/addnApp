using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System;
using Android.Provider;
using Android.App;
using AddnApp.Base;
using Java.Text;
using Java.Util;
using AddnApp.Entities;
using Android.Support.V4.Content;
using Java.IO;

namespace AddnApp.Cadastro
{
    public class CadastroRRImagemFragment : BaseWizardStepFragment
    {
        protected override int ViewId => Resource.Layout.CadastroRR_Imagem_Fragment;
        
        private GridView imageGrid;
        private FloatingActionButton btnCam;
        private FloatingActionButton btnPictureGalery;
        private EditText txtObservacao;
        public static Bitmap BitmapCadastroRR;
        private int selectedItemMn;
        private Android.Net.Uri File;
        public string CurrentPhotoPath { get; set; }

        public RegistroDeReforma Item { get { return Data as RegistroDeReforma; } }


        public static CadastroRRImagemFragment getInstance()
        {
            return new CadastroRRImagemFragment();
        }

        public CadastroRRImagemFragment()
        {
            
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            btnCam = FindViewById<FloatingActionButton>(Resource.CadastroRR_Imagem.foto_equipamento);
            btnPictureGalery = FindViewById<FloatingActionButton>(Resource.CadastroRR_Imagem.galeria_equipamento);
            imageGrid = FindViewById<GridView>(Resource.CadastroRR_Imagem.gridview_ItemRR);

            imageGrid.ItemClick += ImageGrid_ItemClick;
            imageGrid.ItemLongClick += ImageGrid_ItemLongClick;

            txtObservacao = FindViewById<EditText>(Resource.CadastroRR_Imagem.txtObservacao);
            txtObservacao.TextChanged += TxtObservacao_TextChanged;

            btnCam.Click += BtnCam_Click;
            btnPictureGalery.Click += BtnPictureGalery_Click;

            Activity.InvalidateOptionsMenu();
            RegisterForContextMenu(imageGrid);
        }

        private void TxtObservacao_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var editText = (EditText)sender;
            Item.Observacao = editText.Text;
        }

        private void BtnCam_Click(object sender, EventArgs e)
        {
            try
            {
                if (Item.ListaDeImagens != null && Item.ListaDeImagens.Count == 10)
                    Program.Main.ShowMessage("É possível selecionar no máximo 10 imagens", ToastLength.Short, Base.Enums.ToastMessageType.Warning);
                else
                {
                    Intent takePicture = new Intent(MediaStore.ActionImageCapture);
                    File = FileProvider.GetUriForFile(Context, Context.ApplicationInfo.PackageName + ".provider", GetOutputMediaFile());
                    takePicture.PutExtra(MediaStore.ExtraOutput, File);
                    StartActivityForResult(takePicture, 0);
                }
            }
            catch (Exception ex)
            {
                Program.Main.ShowMessage(ex.Message, ToastLength.Long, Base.Enums.ToastMessageType.Error);
            }
        }


        private void BtnPictureGalery_Click(object sender, EventArgs e)
        {
            if (Item.ListaDeImagens != null && Item.ListaDeImagens.Count == 10)
                Program.Main.ShowMessage("É possível selecionar no máximo 10 imagens", ToastLength.Short, Base.Enums.ToastMessageType.Warning);
            else
            {
                Intent pickPhoto = new Intent(Intent.ActionGetContent, MediaStore.Images.Media.ExternalContentUri);
                pickPhoto.SetType("image/*");
                pickPhoto.PutExtra(Intent.ExtraAllowMultiple, true);

                StartActivityForResult(pickPhoto, 1);
            }
        }

        private void ImageGrid_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this.Activity, typeof(ImageDetailRRActivity));
            BitmapCadastroRR = Item.ListaDeImagens[e.Position];

            StartActivity(intent);
        }

        private void ImageGrid_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            selectedItemMn = e.Position;
            Activity.OpenContextMenu(imageGrid);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, (int)resultCode, data);

            if (Item.ListaDeImagens == null)
                Item.ListaDeImagens = new List<Bitmap>();

            switch (requestCode)
            {
                case 0:
                    if (resultCode == (int)Result.Ok)
                    {
                        if (Item.ListaDeImagens.Count == 10)
                            Program.Main.ShowMessage("É possível selecionar no máximo 10 imagens", ToastLength.Short, Base.Enums.ToastMessageType.Warning);
                        else
                            Item.ListaDeImagens.Add(GetImageBitmapFromUrl(File, Context));
                    }

                    break;
                case 1:
                    if (resultCode == (int)Result.Ok)
                    {
                        if (data != null)
                        {
                            if (data.ClipData != null)
                            {
                                int count = data.ClipData.ItemCount;
                                for (int i = 0; i < count; i++)
                                {
                                    if(i == 10 || Item.ListaDeImagens.Count == 10)
                                    {
                                        Program.Main.ShowMessage("É possível selecionar no máximo 10 imagens", ToastLength.Short, Base.Enums.ToastMessageType.Warning);
                                        break;
                                    }
                                    Android.Net.Uri selectedImage = data.ClipData.GetItemAt(i).Uri;
                                    Item.ListaDeImagens.Add(GetImageBitmapFromUrl(selectedImage, Context));
                                }
                            }
                            else if (data.Data != null)
                            {
                                if (Item.ListaDeImagens.Count == 10)
                                    Program.Main.ShowMessage("É possível selecionar no máximo 10 imagens", ToastLength.Short, Base.Enums.ToastMessageType.Warning);
                                else
                                {
                                    Android.Net.Uri selectedImage = data.Data;
                                    Item.ListaDeImagens.Add(GetImageBitmapFromUrl(selectedImage, Context));
                                }
                            }
                        }
                    }
                    break;
            }

            imageGrid.Adapter = new ImageAdapter(Context, Item.ListaDeImagens);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            menu.Add(Menu.None, 1001, 1, "Remover Imagem");
            base.OnCreateContextMenu(menu, v, menuInfo);
        }

        public override void OnDestroyView()
        {
            base.OnDestroy();
        }

        public override bool OnContextItemSelected(IMenuItem mi)
        {
            switch (mi.ItemId)
            {
                case 1001:

                    Item.ListaDeImagens.RemoveAt(selectedItemMn);
                    imageGrid.Adapter = new ImageAdapter(Context, Item.ListaDeImagens);
                    break;
            }

            return base.OnContextItemSelected(mi);
        }

        public Java.IO.File GetOutputMediaFile()
        {
            String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").Format(new Date());
            String imageFileName = "JPEG_" + timeStamp + "_";
            File storageDir = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryDcim), "Camera");
            File image = Java.IO.File.CreateTempFile(
                    imageFileName,  /* prefix */
                    ".jpg",         /* suffix */
                    storageDir      /* directory */
            );

            // Save a file: path for use with ACTION_VIEW intents
            CurrentPhotoPath = "file:" + image.AbsolutePath;
            return image;
        }

        public Bitmap GetImageBitmapFromUrl(Android.Net.Uri uri, Context ctx)
        {
            Bitmap imageBitmap = null;
            Bitmap compressedBitmap = null;

            var width = 0;
            var height = 0;

            System.IO.Stream inputStream = ctx.ContentResolver.OpenInputStream(uri);
            imageBitmap = BitmapFactory.DecodeStream(inputStream);

            if (imageBitmap.Width > imageBitmap.Height)
            {
                var percent = (double)1920/imageBitmap.Width;
                width = Convert.ToInt32(imageBitmap.Width * percent);
                height = Convert.ToInt32(imageBitmap.Height * percent);
            }
            else
            {
                var percent = (double)1080/imageBitmap.Width;
                width = Convert.ToInt32(imageBitmap.Width * percent);
                height = Convert.ToInt32(imageBitmap.Height * percent);
            }

            imageBitmap = Bitmap.CreateScaledBitmap(imageBitmap, width, height, true);

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                imageBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                byte[] byteImage = ms.ToArray();
                compressedBitmap = BitmapFactory.DecodeByteArray(byteImage, 0, byteImage.Length);
            };


            if (inputStream != null)
                inputStream.Close();

            imageBitmap.Recycle();             

            return compressedBitmap;
        }
    }
}