using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;
using Android.Preferences;
using Android.Content;

namespace Sonatiina
{
    [Activity(Label = "@string/settingsButton")]
    public class SettingsActivity : Activity, ISharedPreferencesOnSharedPreferenceChangeListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetTheme(ChangeTheme.getTheme());
            SetContentView(Resource.Layout.Settings);

            var transaction = FragmentManager.BeginTransaction();
            var setFragment = new SettingsFragment();
            transaction.Replace(Resource.Id.container, setFragment);
            transaction.Commit();

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "Asetukset";

            ActionBar.SetHomeButtonEnabled(true); // Takaisin-painike p‰‰ll‰
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
            prefs.RegisterOnSharedPreferenceChangeListener(this);
          
        }

        public class SettingsFragment : PreferenceFragment
        {
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                this.AddPreferencesFromResource(Resource.Xml.preferences);
            }

        }

    public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }


        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            Intent intent = new Intent(this, typeof(Activity1));
            intent.AddFlags(ActivityFlags.ClearTask);
            intent.AddFlags(ActivityFlags.NewTask);
            this.Finish();
            StartActivity(intent);
        }
    }
}