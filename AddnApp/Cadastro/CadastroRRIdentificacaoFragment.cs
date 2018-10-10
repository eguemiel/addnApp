using Android.OS;
using Android.Views;
using AddnApp.Base;
using AddnApp.Helpers;
using Android.Widget;
using AddnApp.Entities;
using Framework.AddApp.Mobile.ApiClient;
using Framework.AddApp.Mobile.ApiModels;
using System;
using Framework.AddApp.Mobile.Api.Configuration;
using AddnApp.Base.Enums;
using Android.Content;
using SharpCifs.Smb;
using System.IO;
using Android.Graphics;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AddnApp.Cadastro
{
    public class CadastroRRIdentificacaoFragment : BaseWizardStepFragment
    {
        private TextView txtRR { get; set; }
        private TextView txtClientName { get; set; }
        private TextView txtData { get; set; }
        private TextView txtEquipamentoDescription { get; set; }
        private TextView txtNroNota { get; set; }
        private TextView txtItem { get; set; }
        private ImageButton btnFindRR { get; set; }
        private Button btnFindImages { get; set; }
        public RegistroDeReforma Item { get { return Data as RegistroDeReforma; } }

        protected override int ViewId => Resource.Layout.CadastroRR_Registro_Fragment;

        public static CadastroRRIdentificacaoFragment getInstance()
        {
            return new CadastroRRIdentificacaoFragment();
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            txtClientName = FindViewById<TextView>(Resource.CadastroRR_Registro.txtInfoClientName);
            txtData = FindViewById<TextView>(Resource.CadastroRR_Registro.txtInfoData);
            txtEquipamentoDescription = FindViewById<TextView>(Resource.CadastroRR_Registro.txtInfoEquipamentDescription);
            txtNroNota = FindViewById<TextView>(Resource.CadastroRR_Registro.txtInfoNroNota);
            txtRR = FindViewById<TextView>(Resource.CadastroRR_Registro.txtRR);
            txtItem = FindViewById<TextView>(Resource.CadastroRR_Registro.txtItem);
            btnFindRR = FindViewById<ImageButton>(Resource.CadastroRR_Registro.findRR);
            btnFindRR.Click += BtnFindRR_Click;
            btnFindImages = FindViewById<Button>(Resource.CadastroRR_Registro.findImages);
            btnFindImages.Click += BtnFindImages_Click;
        }

        private void BtnFindImages_Click(object sender, EventArgs e)
        {
            try
            {
                var task = new GenericTask()
                    .WithPreExecuteProcess((b) =>
                    {
                        Program.Main.ShowLoading();

                    }).WithBackGroundProcess((b, t) =>
                    {
                        try
                        {
                            var intent = new Intent(this.Activity, typeof(ImagesViewActivity));
                            intent.PutExtra("RegistroDeReforma", JsonConvert.SerializeObject(Item));
                            StartActivity(intent);
                        }
                        catch (Exception ex)
                        {
                            Program.Main.ShowMessage(ex.Message, ToastLength.Long, ToastMessageType.Error);
                        }
                    }).WithPosExecuteProcess((b, t) =>
                    {
                        Program.Main.HideLoading();
                    }).Execute();
            }
            catch (Exception ex)
            {
                Program.Main.ShowMessage(string.Format("Ocorreu um erro ao buscar as imagens no servidor: {0}", ex.Message),ToastLength.Long, ToastMessageType.Error);
            }
        }

        private void BtnFindRR_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRR.Text) || string.IsNullOrEmpty(txtItem.Text))
            {
                Program.Main.ShowMessage("Preencha as informações para realizar a busca");
            }
            else
            {
                ProcessRRNumber(new RegistroDeReforma { NumeroRR = txtRR.Text, NumeroItem = txtItem.Text });
                btnFindImages.Visibility = ViewStates.Visible;
                btnFindImages.Enabled = true;
                Program.Main.CloseSoftKeyobard();
            }
        }

        private void ProcessRRNumber(RegistroDeReforma registroDeReforma)
        {
            RrResponse response = new RrResponse();
            var task = new GenericTask()
                .WithPreExecuteProcess((b) =>
                {
                    Program.Main.ShowLoading();
                }).WithBackGroundProcess((b, t) =>
                {
                    try
                    {
                        var request = new RrRequest();
                        request.Item = Convert.ToInt32(registroDeReforma.NumeroItem);
                        request.Registro = Convert.ToInt32(registroDeReforma.NumeroRR);
                        request.PasswordBD = ConfigurationBase.Instance.PasswordBD;
                        request.Url = ConfigurationBase.Instance.ApiUrl;
                        request.UserId = ConfigurationBase.Instance.UserIdBD;
                        response = RrApi.Instance.GetRr(request);
                    }
                    catch (Exception ex)
                    {
                        Program.Main.ShowMessage("Ocorreu um erro ao buscar a RR", ToastLength.Long, ToastMessageType.Error);
                    }

                }).WithPosExecuteProcess((b, t) =>
                {
                    if (response.Success)
                    {
                        txtClientName.Text = response.NomeCliente;
                        txtData.Text = response.DataAbertura;
                        txtEquipamentoDescription.Text = response.DescricaoEquipamento;
                        txtNroNota.Text = response.NumeroNF.ToString();

                        LoadDataRegistroDeReforma(response);
                    }

                    Program.Main.HideLoading();
                }).Execute();
        }

        private void LoadDataRegistroDeReforma(RrResponse response)
        {
            Item.DataCadastro = response.DataAbertura;
            Item.Equipamento = response.DescricaoEquipamento;
            Item.NomeCliente = response.NomeCliente;
            Item.NotaFiscal = response.NumeroNF;
            Item.NumeroItem = txtItem.Text;
            Item.NumeroRR = txtRR.Text;
            Item.DescricaoRR = response.RR;
            Item.Cidade = response.Cidade;
            Item.NomeFantasia = response.NomeFantasia;
        }

        
    }
}