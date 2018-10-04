using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;
using AddnApp.Helpers;
using Framework.AddApp.Mobile.Api.Configuration;
using Framework.AddApp.Mobile.ApiClient;
using Framework.AddApp.Mobile.ApiModels;
using AddApp.Configuration;

namespace AddnApp
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, MainLauncher =true, Theme = "@style/AppTheme")]
    public class LoginActivity : ADDNAbstractActivity
    {
        public EditText EditUser { get; set; }
        public EditText EditSenha { get; set; }
        public Button BtnAcessar { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            using (var stream = Assets.Open("Configuration.xml"))
            {
                AddnAppConfiguration.Instance.ReadFromXml(stream);
            }

            // Create your application here
            SetContentView(Resource.Layout.login);

            EditUser=FindViewById<EditText>(Resource.Id.txtUsuario);
            EditSenha=FindViewById<EditText>(Resource.Id.txtSenha);
            BtnAcessar=FindViewById<Button>(Resource.Id.btnAcessar);
            BtnAcessar.Click += BtnAcessar_Click;

            Loading = FindViewById<ViewGroup>(Resource.Id.loading);
        }

        private void BtnAcessar_Click(object sender, EventArgs e)
        {
            var task = new GenericTask()
                    .WithPreExecuteProcess((b) =>
                    {
                        Loading.Visibility = ViewStates.Visible;

                    }).WithBackGroundProcess((b, t) =>
                    {
                        try
                        {
                            RunOnUiThread(() => { EditUser.Text = "junior"; });
                            RunOnUiThread(() => { EditSenha.Text = "1234"; });                            
                            if (string.IsNullOrEmpty(EditUser.Text) || string.IsNullOrEmpty(EditSenha.Text))
                            {
                                if (string.IsNullOrEmpty(EditUser.Text))
                                    RunOnUiThread(() => { EditUser.Error = "Favor digitar o usuário"; });

                                if (string.IsNullOrEmpty(EditSenha.Text))
                                    RunOnUiThread(() => { EditSenha.Error = "Favor digitar a senha"; });                                
                            }
                            else
                            {
                                if(true)//Login(EditUser.Text, EditSenha.Text))
                                {
                                    ConfigurationBase.Instance.UserAPP = EditUser.Text;
                                    StartActivity(typeof(MainActivity));
                                    Finish();
                                }
                                else
                                {
                                    ShowError("Usuário ou senha inválidos");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ShowError(ex.Message);
                        }
                    }).WithPosExecuteProcess((b, t) =>
                    {
                        Loading.Visibility = ViewStates.Gone;
                    }).Execute();            
            
        }       

        public void ShowError(string description)
        {
            RunOnUiThread(() =>
            {
                Android.App.AlertDialog.Builder ad = new Android.App.AlertDialog.Builder(this);
                ad.SetTitle("Erro");
                ad.SetMessage(description);
                ad.Show();
            });
        }


        public bool Login(string usuario, string senha)
        {
            try
            {
                var response = LoginApi.Instance.SignIn(new LoginRequest()
                {
                    User = EditUser.Text,
                    Password = EditSenha.Text,
                    PasswordBD = ConfigurationBase.Instance.PasswordBD,
                    Url = ConfigurationBase.Instance.ApiUrl,
                    UserId = ConfigurationBase.Instance.UserIdBD
                });

                return response.Success;
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return false;
            }
        }
    }
}