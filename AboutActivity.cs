using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
namespace Sonatiina
{
    [Activity(Label = "@string/aboutButton")]
    public class AboutActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.About);
        }
    }
}