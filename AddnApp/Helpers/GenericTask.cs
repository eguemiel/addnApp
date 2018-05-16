using Android.OS;
using System;

namespace AddnApp.Helpers
{
    public class GenericTask : AsyncTask
    {
        public Action<Bundle> PreExecuteAction;

        public Action<Bundle, GenericTask> PostExecuteAction;

        public Action<Bundle, GenericTask> RunInBackgroundAction;

        public Bundle currentBundle;

        public string ErrorMessage { get; set; }
            
        public bool HasError { get; set; }

        public GenericTask()
        {
            currentBundle = new Bundle();
        }

        protected override void OnPreExecute()
        {
            PreExecuteAction?.Invoke(currentBundle);
        }

        protected override void OnPostExecute(Java.Lang.Object result)
        {
            PostExecuteAction?.Invoke(currentBundle, this);
        }

        protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] native_parms)
        {
            RunInBackgroundAction?.Invoke(currentBundle, this);
            return null;
        }
    }

    public static class GenericTaskExtension
    {
        public static GenericTask WithPreExecuteProcess(this GenericTask obj, Action<Bundle> pre)
        {
            obj.PreExecuteAction = pre;
            return obj;
        }

        public static GenericTask WithPosExecuteProcess(this GenericTask obj, Action<Bundle, GenericTask> pos)
        {
            obj.PostExecuteAction = pos;
            return obj;
        }

        public static GenericTask WithBackGroundProcess(this GenericTask obj, Action<Bundle, GenericTask> back)
        {
            obj.RunInBackgroundAction = back;
            return obj;
        }
    }
}
