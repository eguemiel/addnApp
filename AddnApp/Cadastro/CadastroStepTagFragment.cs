﻿using Android.OS;
using Android.Views;
using Android.Widget;
using System.Linq;
using System.Threading.Tasks;
using Android.Support.V7.Widget;

namespace AddnApp.Cadastro
{
    public class DamageStepTagFragment : BaseWizardStepFragment
    {
        protected override int ViewId => throw new System.Exception();        

        public static DamageStepTagFragment getInstance()
        {
            return new DamageStepTagFragment();
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
           
        }     

        private void ProcessTag()
        {   

            var task = new GenericTask()
                .WithPreExecuteProcess((b) =>
                {
                   
                }).WithBackGroundProcess((b, t) =>
                {
                   
                }).WithPosExecuteProcess((b, t) =>
                {
                    
                }).Execute();
        }
    }
}