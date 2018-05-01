using AddnApp.Base;
using AddnApp.Events;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace AddnApp.Base
{
    public abstract class BaseWizardFragment : BaseFragment
    {
        protected override int ViewId => Resource.Layout.base_wizard;
        public BaseWizardStepFragment[] Steps { get; set; }
        public int ActiveStepId { get; private set; }
        public int StepCount { get { return Steps.Length; } }
        protected ProgressBar ProgressBar { get; set; }
        public object Data { get; set; }
        protected Button BtnBack { get; set; }
        protected Button BtnNext { get; set; }
        protected int ContainerId { get; set; }

        protected TextView TxtSteps { get; set; }

        public abstract void OnDone();

        public BaseWizardStepFragment ActiveStep
        {
            get
            {
                var activeStep = Steps[ActiveStepId - 1];
                activeStep.Data = Data;
                activeStep.Wizard = this;
                return activeStep;
            }
        }

        public BaseWizardFragment()
        {
            ActiveStepId = 1;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            InitializeComponents();
        }

        public override void OnStart()
        {
            base.OnStart();

            SetActiveStep();
        }

        protected virtual void InitializeComponents()
        {
            ProgressBar = FindViewById<ProgressBar>(Resource.Base_wizard.progressBar);
            BtnBack = FindViewById<Button>(Resource.Base_wizard.btnBack);
            BtnBack.Click += BtnBack_Click;
            BtnNext = FindViewById<Button>(Resource.Base_wizard.btnNext);
            BtnNext.Click += BtnNext_Click;
            TxtSteps = FindViewById<TextView>(Resource.Base_wizard.txtSteps);
            ContainerId = Resource.Base_wizard.container;
        }

        protected void Main_OnBackPressedEvent(EventBackPressedArgs e)
        {
            if (ActiveStepId >= 1)
            {
                ActiveStepId--;
                SetActiveStep();
            }
            e.Cancel = true;
        }

        protected void BtnBack_Click(object sender, System.EventArgs e)
        {
            Main_OnBackPressedEvent(new EventBackPressedArgs());
        }

        protected void BtnNext_Click(object sender, System.EventArgs e)
        {
            NextStep();
        }

        public void NextStep()
        {
            if (!ActiveStep.IsValid())
                return;

            ActiveStep.BeforeContinue();

            if (ActiveStepId == StepCount)
            {
                OnDone();
            }
            else
            {
                ActiveStepId++;
                SetActiveStep();
            }
        }

        private void SetStepTitle()
        {
            if (TxtSteps != null)
            {
                TxtSteps.Text = string.Format("Passo {0} de {1}", ActiveStepId, StepCount);
            }
        }

        private void SetActiveStep()
        {
            ActiveStep.LoadData(() =>
            {
                ProgressBar.Max = StepCount;
                ProgressBar.Progress = ActiveStepId;

                if (ActiveStepId == 1)
                {
                    BtnBack.Enabled = false;
                }
                else
                {
                    BtnBack.Enabled = true;
                }

                if (ActiveStepId == StepCount)
                {
                    BtnNext.Text = "Done";
                }
                else
                {
                    BtnNext.Text = "Next";
                }

                var tx = ChildFragmentManager.BeginTransaction();
                tx.Replace(ContainerId, ActiveStep);
                tx.AddToBackStack(null);
                tx.Commit();

                SetStepTitle();
            });
        }
    }
}