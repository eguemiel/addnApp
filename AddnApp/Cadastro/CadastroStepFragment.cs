using System;
using System.Linq;
using Android.OS;
using Android.Views;
using Android.Widget;
using AddnApp.Base;
using AddnApp.Helpers;

namespace AddnApp.Cadastro
{
    [Android.App.Activity(Label = "Steps", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class DamageStepFragment : AddnAppBaseWizardFragment
    {
        public DamageStepFragment()
        {
            Steps = new BaseWizardStepFragment[]
            {
                new DamageStepTagFragment(),
                new DamageStepImageFragment(),
                new DamageStepNoteAndConfirmFragment(),
            };
        }

        private bool CanFileDamage = true;

<<<<<<< .merge_file_a02056
        protected override string FragmentTitle => Resources.GetString(Resource.String.VolumeDamageStepFragmentTitle);

        protected override int ViewId => Resource.Layout.Damage_Step_Fragment;
=======
        protected override string FragmentTitle => "Cadastro de RR";

        protected override int ViewId => Resource.Layout.CadastroRR_Fragment;
>>>>>>> .merge_file_a12108

        protected override void InitializeComponents()
        {
            this.SetMainTitle();

<<<<<<< .merge_file_a02056
            ProgressBar = FindViewById<ProgressBar>(Resource.Damage_step.progressBar);
            BtnBack = FindViewById<Button>(Resource.Damage_step.btnBack);
            BtnBack.Click += BtnBack_Click;
            BtnNext = FindViewById<Button>(Resource.Damage_step.btnNext);
            BtnNext.Click += BtnNext_Click;
            TxtSteps = FindViewById<TextView>(Resource.Damage_step.txtSteps);
            ContainerId = Resource.Damage_step.container;
=======
            ProgressBar = FindViewById<ProgressBar>(Resource.CadastroRR.progressBar);
            BtnBack = FindViewById<Button>(Resource.CadastroRR.btnBack);
            BtnBack.Click += BtnBack_Click;
            BtnNext = FindViewById<Button>(Resource.CadastroRR.btnNext);
            BtnNext.Click += BtnNext_Click;
            TxtSteps = FindViewById<TextView>(Resource.CadastroRR.txtSteps);
            ContainerId = Resource.CadastroRR.container;
>>>>>>> .merge_file_a12108
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
<<<<<<< .merge_file_a02056
                if (string.IsNullOrEmpty(damageItem.TagNumber))
                {
                    Program.Main.ShowMessage(Resources.GetString(Resource.String.VolumeDamageStepFragmentProvideTag), ToastLength.Long);
                    return;
                }

                if (string.IsNullOrEmpty(damageItem.DamageNote))
                {
                    Program.Main.ShowMessage(Resources.GetString(Resource.String.VolumeDamageStepFragmentInsertNote), ToastLength.Long);
                    return;
                }

                if (!damageItem.ImageList.Any())
                {
                    Program.Main.ShowMessage(Resources.GetString(Resource.String.VolumeDamageStepFragmentInsertImage), ToastLength.Long);
                    return;
                }

                var request = new DamageCargoByTagNumberRequest();
                var response = new DamageCargoByTagNumberResponse();

                if (!CanFileDamage)
                    return;

                var task = new GenericTask()
                    .WithPreExecuteProcess((b) =>
                    {
                        CanFileDamage = false;

                        Program.Main.ShowLoading();

                        request.DamageNote = damageItem.DamageNote;
                        request.TagNumber = damageItem.TagNumber;

                        foreach (var item in damageItem.ImageList)
                            request.ImageList.Add(Helpers.ConvertToBase64String(item));

                    }).WithBackGroundProcess((b, t) =>
                    {
                        try
                        {
                            response = DamageLogApi.Instance.IdentificationDamageCargoByTagNumber(request);
                        }
                        catch (Exception ex)
                        {
                            Program.Main.ShowError(ex.Message);
                        }
                    }).WithPosExecuteProcess((b, t) =>
                    {
                        if (response != null && response.Success)
                        {
                            Toast.MakeText(Context, Resources.GetString(Resource.String.VolumeDamageStepFragmentSuccessfully), ToastLength.Short).Show();
                            Program.Main.CloseSoftKeyobard();
                            Program.Main.Navigate<HomeFragment>();
                        }
                        else
                        {
                            if(response != null)
                                Toast.MakeText(Context, response.WarningMessage, ToastLength.Short).Show();                            
                            Program.Main.HideLoading();
                        }

                        CanFileDamage = true;
                    }).Execute();
=======

>>>>>>> .merge_file_a12108
            }
        }
    }
}