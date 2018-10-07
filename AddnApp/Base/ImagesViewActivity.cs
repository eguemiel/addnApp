using System;
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
using TiagoSantos.EnchantedViewPager;
using Android.Views;

namespace AddnApp.Cadastro
{
    [Activity(Label = "ImagesViewActivity", Theme = "@style/AppTheme")]
    public class ImagesViewActivity : AppCompatActivity
    {
        protected List<Bitmap> bitmap;
        protected ImagesViewAdapter adapter;
        protected EnchantedViewPagerExtended imageView;
        protected RegistroDeReforma registroDeReforma;
        private int quantidadeImagens;
        private int indexImageAtual;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CadastroRR_Images_View);

            registroDeReforma = JsonConvert.DeserializeObject<RegistroDeReforma>(Intent.GetStringExtra("RegistroDeReforma"));
            quantidadeImagens = PegaQuantidadeImagensServidor(registroDeReforma);

            if (quantidadeImagens > 0)
            {
                bitmap = PegarImagensServidor(registroDeReforma, 0);

                imageView = FindViewById<EnchantedViewPagerExtended>(Resource.Id.cadastroRRImagesView);                            
                
                adapter = new ImagesViewAdapter(this, bitmap);
                imageView.Adapter = adapter;
            }else
            {
                Program.Main.ShowMessage("O diretório não possui imagens");
            }
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

        private List<Bitmap> PegarImagensServidor(RegistroDeReforma registroDeReforma, int indexImage)
        {
            List<Bitmap> listaDeImagens = new List<Bitmap>();
            
            try
            {
                //var firstLetterClient = registroDeReforma.NomeCliente.Substring(0, 1);
                //var fullClientName = registroDeReforma.NomeCliente.RemoveSpecialCaracters();
                //var apelido = registroDeReforma.NomeFantasia.RemoveSpecialCaracters();
                //var cityName = registroDeReforma.Cidade.RemoveSpecialCaracters();
                //var dateRR = DateTime.Now;
                //var nf = registroDeReforma.NotaFiscal;
                //var rr = registroDeReforma.DescricaoRR;
                //var eqDesc = registroDeReforma.Equipamento.RemoveSpecialCaracters();

                var smbPath = "smb://192.168.33.102/Users/JR/Documents/DEV/Images/";
                //var smbPath = "smb://192.168.0.244/Clientes/";
                //var filePath = string.Format("{0}/{1} -- {2}/Unidade {3}/{4}/NF {5} R.R. {6} {7}/Fotos C.Q/",
                //                            firstLetterClient, fullClientName, apelido,
                //                            cityName, dateRR.Year, nf, rr, eqDesc);
                var filePath = "E/Eguemiel Miquelin Junior -- Miquelin Jr Equipamentos/Unidade Sertãozinho/2018/" +
                    "/NF 1234 R.R. 2344 Rosa Pilicoildal/";
                var auth2 = new NtlmPasswordAuthentication("WORKGROUP", "juninhomiquelin@hotmail.com", "Juh2Iamah36*.D");
                var pathConfirm = new SmbFile(string.Format("{0}/{1}", smbPath, filePath), auth2);

                //Create file.
                if (pathConfirm.Exists())
                {
                    foreach (SmbFile file in pathConfirm.ListFiles().ToList().GetRange(indexImage, indexImage + 2))
                    {
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
                            listaDeImagens.Add(BitmapFactory.DecodeByteArray(byteImage, 0, byteImage.Length));
                        }
                    }
                }
                else
                    throw new Exception("Diretório não encontrado");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listaDeImagens;
        }


        private int PegaQuantidadeImagensServidor(RegistroDeReforma registroDeReforma)
        {
            int quantidadeImagens = 0;
            try
            {
                //var firstLetterClient = registroDeReforma.NomeCliente.Substring(0, 1);
                //var fullClientName = registroDeReforma.NomeCliente.RemoveSpecialCaracters();
                //var apelido = registroDeReforma.NomeFantasia.RemoveSpecialCaracters();
                //var cityName = registroDeReforma.Cidade.RemoveSpecialCaracters();
                //var dateRR = DateTime.Now;
                //var nf = registroDeReforma.NotaFiscal;
                //var rr = registroDeReforma.DescricaoRR;
                //var eqDesc = registroDeReforma.Equipamento.RemoveSpecialCaracters();

                var smbPath = "smb://192.168.33.102/Users/JR/Documents/DEV/Images/";
                //var smbPath = "smb://192.168.0.244/Clientes/";
                //var filePath = string.Format("{0}/{1} -- {2}/Unidade {3}/{4}/NF {5} R.R. {6} {7}/Fotos C.Q/",
                //                            firstLetterClient, fullClientName, apelido,
                //                            cityName, dateRR.Year, nf, rr, eqDesc);
                var filePath = "E/Eguemiel Miquelin Junior -- Miquelin Jr Equipamentos/Unidade Sertãozinho/2018/" +
                    "/NF 1234 R.R. 2344 Rosa Pilicoildal/";
                var auth2 = new NtlmPasswordAuthentication("WORKGROUP", "juninhomiquelin@hotmail.com", "Juh2Iamah36*.D");
                var pathConfirm = new SmbFile(string.Format("{0}/{1}", smbPath, filePath), auth2);

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
                throw new Exception(ex.Message);
            }

            return quantidadeImagens;
        }
    }
}