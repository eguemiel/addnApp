using System;
using System.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using AddnApp.Base;
using AddnApp.Helpers;
using AddnApp.Entities;
using SharpCifs.Smb;
using Framework.AddApp.Mobile.ApiClient;
using Framework.AddApp.Mobile.ApiModels;
using Framework.AddApp.Mobile.Api.Configuration;

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
                    Program.Main.ShowMessage("Favor inserir um Registro de Reforma (RR)");
                    return;
                }               

                if ((registroDeReforma.ListaDeImagens == null || !registroDeReforma.ListaDeImagens.Any()) && string.IsNullOrWhiteSpace(registroDeReforma.Observacao))
                {
                    Program.Main.ShowMessage("É necessário inserir ao menos uma imagem ou uma observação para continuar");
                    return;
                }

                var task = new GenericTask()
                    .WithPreExecuteProcess((b) =>
                    {
                        Program.Main.ShowLoading();                      

                    }).WithBackGroundProcess((b, t) =>
                    {
                        try
                        {
                            if(registroDeReforma.ListaDeImagens != null && registroDeReforma.ListaDeImagens.Any())
                                InserirImagensServidor(registroDeReforma);

                            if (!string.IsNullOrWhiteSpace(registroDeReforma.Observacao))
                            {
                                var request = new ObservationRequest();
                                request.Item = Convert.ToInt32(registroDeReforma.NumeroItem);
                                request.Registro = Convert.ToInt32(registroDeReforma.NumeroRR);
                                request.PasswordBD = ConfigurationBase.Instance.PasswordBD;
                                request.Url = ConfigurationBase.Instance.ApiUrl;
                                request.UserId = ConfigurationBase.Instance.UserIdBD;

                                var observationResponse = RrApi.Instance.GetIdObservacao(request);

                                if (observationResponse.Success)
                                {
                                    var idObservation = observationResponse.IdObservation + 1;

                                    request.Texto = registroDeReforma.Observacao;
                                    request.Usuario = ConfigurationBase.Instance.UserAPP;
                                    request.IdObservacao = idObservation;

                                    var responseObservacao = RrApi.Instance.InsertObservacao(request);
                                    if (!responseObservacao)
                                        throw new Exception("Ocorreu um erro ao inserir informações de observação");
                                }
                                else
                                    throw new Exception("Ocorreu um erro ao buscar informações da obseração");
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.Main.ShowMessage(ex.Message, ToastLength.Long, Base.Enums.ToastMessageType.Error);
                        }
                    }).WithPosExecuteProcess((b, t) =>
                    {
                        var wizard = Program.Main.Navigate<CadastroRRFragment>();
                        wizard.Data = new RegistroDeReforma();
                        Program.Main.ShowMessage("Cadastros de imagens realizado com sucesso", ToastLength.Long, Base.Enums.ToastMessageType.Success);
                        Program.Main.HideLoading();                        
                    }).Execute();
            }
        }

        private void InserirImagensServidor(RegistroDeReforma registroDeReforma)
        {
            foreach (var item in registroDeReforma.ListaDeImagens)
            {
                //Remover Caracteres Especiais, Nome, NomeFantasia, Descricao do Equipamento e Cidade
                //
                //Primeira Letra nome Cliente/Nome -- 
                //NomeFantasia /Unidade {Cidade}/Ano/NF NroNota R.R. NroRRConsulta DescricaoEquipamento/
                //NomeImagem DataComUnderline
                //Get the SmbFile specifying the file name to be created.

                var firstLetterClient = registroDeReforma.NomeCliente.Substring(0, 1);
                var fullClientName = registroDeReforma.NomeCliente;
                var apelido = registroDeReforma.NomeFantasia;
                var cityName = registroDeReforma.Cidade;
                var dateRR = DateTime.Parse(registroDeReforma.DataCadastro);
                var nf = registroDeReforma.NotaFiscal;
                var rr = registroDeReforma.DescricaoRR;
                var eqDesc = registroDeReforma.Equipamento;

                var smbPath = "smb://192.168.0.244/Clientes/";
                var filePath = string.Format("{0}/{1} -- {2}/Unidade {3}/{4}/NF {5} R.R. {6} {7}",
                                            firstLetterClient, fullClientName, apelido,
                                            cityName, dateRR.Year, nf, rr, eqDesc );

                var fileName = string.Format("{0}.{1}",DateTime.Now.ToString().Replace('/','_').Replace(':','_').Replace(' ', '_'),"jpg");

                var auth2 = new NtlmPasswordAuthentication("addnbr", "suporte", "@master01");
                var pathConfirm = new SmbFile(string.Format("{0}/{1}", smbPath, filePath), auth2);

                //Create file.
                if (!pathConfirm.Exists())
                    pathConfirm.Mkdirs();

                var file = new SmbFile(string.Format("{0}/{1}/{2}", smbPath, filePath, fileName), auth2);
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