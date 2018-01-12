using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Preferences;

namespace Sonatiina
{
    class ChangeTheme
    {
        private static int[] themes = { Resource.Style.MyCustomTheme, Resource.Style.DarkTheme, Resource.Style.PinkTheme };

        public ChangeTheme()
        {
          // TODO  
        }

        public static int getTheme()
        {
            // hakee SharedPreferencestä valitun teeman
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            int selection = int.Parse(preferences.GetString("pref_selected_theme","0"));
            return themes[selection];
        }
    }
}