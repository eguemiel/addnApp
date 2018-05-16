using System;
using System.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using AddnApp.Base;
using AddnApp.Helpers;
using AddnApp.Entities;
using SharpCifs.Smb;

namespace AddnApp.Cadastro
{
    [Android.App.Activity(Label = "Steps", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class CadastroRRFragment : AddnAppBaseWizardFragment
    {
        public CadastroRRFragment()
        {
            Steps = new BaseWizardStepFragment[]
            {
                new CadastroRRIdentificacaoFragment(),
                new CadastroRRImagemFragment()
            };
        }

        private bool CanFileDamage = true;

        protected override string FragmentTitle => "Cadastro de RR";

        protected override int ViewId => Resource.Layout.CadastroRR_Fragment;

        protected override void InitializeComponents()
        {
            this.SetMainTitle();


            ProgressBar = FindViewById<ProgressBar>(Resource.CadastroRR.progressBar);
            BtnBack = FindViewById<Button>(Resource.CadastroRR.btnBack);
            BtnBack.Click += BtnBack_Click;
            BtnNext = FindViewById<Button>(Resource.CadastroRR.btnNext);
            BtnNext.Click += BtnNext_Click;
            TxtSteps = FindViewById<TextView>(Resource.CadastroRR.txtSteps);
            ContainerId = Resource.CadastroRR.container;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            this.SetMainTitle();
        }

        public override void OnDone()
        {
            if (Data != null)
            {
                RegistroDeReforma registroDeReforma = (RegistroDeReforma)Data;

                if (string.IsNullOrEmpty(registroDeReforma.NumeroRR))
                {
                    //ShowMessage(Resources.GetString(Resource.String.VolumeDamageStepFragmentProvideTag), ToastLength.Long);
                    return;
                }               

                if (!registroDeReforma.ListaDeImagens.Any())
                {
                    //Program.Main.ShowMessage(Resources.GetString(Resource.String.VolumeDamageStepFragmentInsertImage), ToastLength.Long);
                    return;
                }

                var task = new GenericTask()
                    .WithPreExecuteProcess((b) =>
                    {
                        //Program.Main.ShowLoading();                      

                    }).WithBackGroundProcess((b, t) =>
                    {
                        try
                        {
                            InserirImagensServidor(registroDeReforma);


                        }
                        catch (Exception ex)
                        {
                            //Program.Main.ShowError(ex.Message);
                        }
                    }).WithPosExecuteProcess((b, t) =>
                    {

                        //Toast.MakeText(Context, response.WarningMessage, ToastLength.Short).Show();
                        //Program.Main.HideLoading();
                    }).Execute();
            }
        }

        private void InserirImagensServidor(RegistroDeReforma registroDeReforma)
        {
            foreach (var item in registroDeReforma.ListaDeImagens)
            {
                //Remover Caracteres Especiais, Nome, NomeFantasia, Descricao do Equipamento e Cidade
                //':','\','/','|','*','?','"','<','>','|'
                //192.168.0.244/Clientes/Primeira Letra nome Cliente/Nome -- NomeFantasia/Unidade {Cidade}/Ano/NF NroNota R.R. NroRRConsulta DescricaoEquipamento/NomeImagem DataComUnderline
                //Get the SmbFile specifying the file name to be created.
                var file = new SmbFile("smb://Junior:230411.a@192.168.33.102/Code/teste.jpg");

                //Create file.
                file.CreateNewFile();

                //Get writable stream.
                var writeStream = file.GetOutputStream();

                //Write bytes.
                writeStream.Write(Helpers.Helpers.GetImageArray(item));

                //Dispose writable stream.
                writeStream.Dispose();
            }
            
        }
    }
}