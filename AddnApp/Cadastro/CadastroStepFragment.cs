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

            }
        }
    }
}