using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.V7.App;
using Oracle.ManagedDataAccess.Client;

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

            // Create your application here
            SetContentView(Resource.Layout.login);

            EditUser=FindViewById<EditText>(Resource.Id.txtUsuario);
            EditSenha=FindViewById<EditText>(Resource.Id.txtSenha);
            BtnAcessar=FindViewById<Button>(Resource.Id.btnAcessar);
            BtnAcessar.Click += BtnAcessar_Click;
        }

        private void BtnAcessar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EditUser.Text) || string.IsNullOrEmpty(EditSenha.Text))
            {
                if (string.IsNullOrEmpty(EditUser.Text))
                    EditUser.Error = "Favor digitar o usuário";

                if (string.IsNullOrEmpty(EditSenha.Text))
                    EditSenha.Error = "Favor digitar a senha";
                
                if (EditUser.Text != "junior" && EditSenha.Text != "1234")
                    ShowError("Usuário ou senha inválidos");
            }
            else
            {
                StartActivity(typeof(MainActivity));
                Finish();
            }
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


        public void Login()
        {
            string conString = "User Id=multgestor_addn;Password=mult;Data Source=192.168.33.104:1521/xe;";
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select registro from tequipamentos where registro = :id";

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", 50);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            //await context.Response.WriteAsync("Employee First Name: " + reader.GetString(0) + "\n");
                        }

                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        //await context.Response.WriteAsync(ex.Message);
                    }
                }
            }
        }
    }
}