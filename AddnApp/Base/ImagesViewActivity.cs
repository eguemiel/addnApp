﻿using System;
using System.Collections.Generic;
using System.IO;
using AddnApp.Base;
using AddnApp.Entities;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Newtonsoft.Json;
using SharpCifs.Smb;
using System.Linq;
using Framework.AddApp.Mobile.Api.Configuration;
using Android.Support.Design.Widget;
using AddnApp.Helpers;
using Android.Widget;
using Android.Views;

namespace AddnApp.Cadastro
{
    [Activity(Label = "ImagesViewActivity", Theme = "@style/AppTheme")]
    public class ImagesViewActivity : AppCompatActivity
    {
        protected List<Bitmap> bitmap;
        protected ImagesViewAdapter adapter;
        protected EnchantedViewPagerExtended imageView;
        protected FloatingActionButton previusImage;
        protected FloatingActionButton nextImage;
        protected RegistroDeReforma registroDeReforma;
        private int quantidadeImagens;
        private int indexAtual;
        public ViewGroup Loading { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CadastroRR_Images_View);

            Loading = FindViewById<ViewGroup>(Resource.Id.loadingImage);
            Loading.BringToFront();

            registroDeReforma = JsonConvert.DeserializeObject<RegistroDeReforma>(Intent.GetStringExtra("RegistroDeReforma"));
            quantidadeImagens = PegaQuantidadeImagensServidor(registroDeReforma);

            if (quantidadeImagens > 0)
            {
                if (bitmap == null)
                    bitmap = new List<Bitmap>();

                bitmap.Add(PegarImagensServidor(registroDeReforma,0));

                imageView = FindViewById<EnchantedViewPagerExtended>(Resource.Id.cadastroRRImagesView);        
                
                adapter = new ImagesViewAdapter(this, bitmap);
                imageView.Adapter = adapter;
            }else
            {
                Program.Main.ShowMessage("O diretório não possui imagens");
            }

            nextImage = FindViewById<FloatingActionButton>(Resource.Id.nextImage);
            previusImage = FindViewById<FloatingActionButton>(Resource.Id.previousImage);

            nextImage.Click += NextImage_Click;
            previusImage.Click += PreviusImage_Click;
            previusImage.Enabled = false;
        }

        private void PreviusImage_Click(object sender, EventArgs e)
        {

            var task = new GenericTask()
                   .WithPreExecuteProcess((b) =>
                   {
                       ShowLoading();

                   }).WithBackGroundProcess((b, t) =>
                   {
                       try
                       {
                           if (indexAtual > 0)
                           {
                               RunOnUiThread(() =>
                               {
                                   bitmap = new List<Bitmap>();
                                   bitmap.Add(PegarImagensServidor(registroDeReforma, indexAtual - 1));

                                   imageView = FindViewById<EnchantedViewPagerExtended>(Resource.Id.cadastroRRImagesView);

                                   adapter = new ImagesViewAdapter(this, bitmap);
                                   imageView.Adapter = adapter;
                               });
                           }
                           else
                               previusImage.Enabled = false;
                       }
                       catch (Exception ex)
                       {
                           Program.Main.ShowMessage(ex.Message, ToastLength.Long, Base.Enums.ToastMessageType.Error);
                       }
                   }).WithPosExecuteProcess((b, t) =>
                   {
                       HideLoading();
                   }).Execute();

        }


        private void NextImage_Click(object sender, EventArgs e)
        {
            var task = new GenericTask()
                   .WithPreExecuteProcess((b) =>
                   {
                       ShowLoading();

                   }).WithBackGroundProcess((b, t) =>
                   {
                       try
                       {
                           if (indexAtual + 1 < quantidadeImagens)
                           {
                               RunOnUiThread(() =>
                               {
                                   bitmap = new List<Bitmap>();
                                   bitmap.Add(PegarImagensServidor(registroDeReforma, indexAtual + 1));

                                   imageView = FindViewById<EnchantedViewPagerExtended>(Resource.Id.cadastroRRImagesView);

                                   adapter = new ImagesViewAdapter(this, bitmap);
                                   imageView.Adapter = adapter;
                                   previusImage.Enabled = true;
                               });
                           }
                           else
                               nextImage.Enabled = false;
                       }
                       catch (Exception ex)
                       {
                           Program.Main.ShowMessage(ex.Message, ToastLength.Long, Base.Enums.ToastMessageType.Error);
                       }
                   }).WithPosExecuteProcess((b, t) =>
                   {   
                       HideLoading();
                   }).Execute();
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

        private Bitmap PegarImagensServidor(RegistroDeReforma registroDeReforma, int index)
        {
            Bitmap imagem = null;

            indexAtual = index;

            try
            {
                var firstLetterClient = registroDeReforma.NomeCliente.Substring(0, 1);
                var fullClientName = registroDeReforma.NomeCliente.RemoveSpecialCaracters();
                var apelido = registroDeReforma.NomeFantasia.RemoveSpecialCaracters();
                var cityName = registroDeReforma.Cidade.RemoveSpecialCaracters();
                var dateRR = DateTime.Now;
                var nf = registroDeReforma.NotaFiscal;
                var rr = registroDeReforma.DescricaoRR;
                var eqDesc = registroDeReforma.Equipamento.RemoveSpecialCaracters();

                var smbPath = ConfigurationBase.Instance.SmbPath;
                var filePath = string.Format(ConfigurationBase.Instance.FilePath, 
                                                firstLetterClient, fullClientName, apelido,
                                                cityName, dateRR.Year, nf, rr, eqDesc.TrimEnd());
                var auth2 = new NtlmPasswordAuthentication(ConfigurationBase.Instance.NetworkDomain, ConfigurationBase.Instance.NetworkUser, ConfigurationBase.Instance.NetworkPassword);
                var pathConfirm = new SmbFile(string.Format("{0}/{1}", smbPath, filePath), auth2);

                //Create file.
                if (pathConfirm.Exists())
                {
                    SmbFile file = pathConfirm.ListFiles().ToList()[index];

                    if (file.IsFile())
                    {
                        //Get readable stream.
                        var readStream = file.GetInputStream();

                        //Create reading buffer.
                        var memStream = new MemoryStream();

                        //Get bytes.
                        ((Stream)readStream).CopyTo(memStream);

                        //Dispose readable stream.
                        readStream.Dispose();

                        byte[] byteImage = memStream.ToArray();
                        imagem = BitmapFactory.DecodeByteArray(byteImage, 0, byteImage.Length);
                    }
                }

                else
                    throw new Exception("Diretório não encontrado");
            }
            catch (Exception ex)
            {
                Program.Main.ShowMessage(ex.Message, Android.Widget.ToastLength.Long, Base.Enums.ToastMessageType.Error);
            }

            return imagem;
        }


        private int PegaQuantidadeImagensServidor(RegistroDeReforma registroDeReforma)
        {
            int quantidadeImagens = 0;
            try
            {
                var firstLetterClient = registroDeReforma.NomeCliente.Substring(0, 1);
                var fullClientName = registroDeReforma.NomeCliente.RemoveSpecialCaracters();
                var apelido = registroDeReforma.NomeFantasia.RemoveSpecialCaracters();
                var cityName = registroDeReforma.Cidade.RemoveSpecialCaracters();
                var dateRR = DateTime.Now;
                var nf = registroDeReforma.NotaFiscal;
                var rr = registroDeReforma.DescricaoRR;
                var eqDesc = registroDeReforma.Equipamento.RemoveSpecialCaracters();

                var smbPath = ConfigurationBase.Instance.SmbPath;
                var filePath = string.Format(ConfigurationBase.Instance.FilePath,
                                                firstLetterClient, fullClientName, apelido,
                                                cityName, dateRR.Year, nf, rr, eqDesc.TrimEnd());
                var auth2 = new NtlmPasswordAuthentication(ConfigurationBase.Instance.NetworkDomain, ConfigurationBase.Instance.NetworkUser, ConfigurationBase.Instance.NetworkPassword);
                var pathConfirm = new SmbFile(string.Format("{0}{1}", smbPath, filePath), auth2);

                //Create file.
                if (pathConfirm.Exists())
                {
                    quantidadeImagens = pathConfirm.ListFiles().Length;                    
                }
                else
                    quantidadeImagens = 0;
            }
            catch (Exception ex)
            {
                Program.Main.ShowMessage(ex.Message, Android.Widget.ToastLength.Long, Base.Enums.ToastMessageType.Error);
            }

            return quantidadeImagens;
        }

        public void ShowLoading()
        {
            RunOnUiThread(() =>
            Loading.Visibility = ViewStates.Visible);
        }


        private void HideLoading()
        {
            RunOnUiThread(() =>
            Loading.Visibility = ViewStates.Gone);
        }
    }
}