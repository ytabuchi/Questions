using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace XQuestions.Droid
{
    [Activity(Label = "XQuestions.Droid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(CanvasViewActivity)));
        }
    }
}

