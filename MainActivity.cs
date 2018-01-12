using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.Collections.Generic;
using Android;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sonatiina
{
	[Activity (Label = "Sonatiina", MainLauncher = true)]
	public class Activity1 : Activity
    {
        private List<Ravintola> lista;
		private ListView RavintolaListView;
		private ProgressDialog progressDialog;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            //Vaihtaa teeman
            this.SetTheme(ChangeTheme.getTheme());
            
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            this.RavintolaListView = this.FindViewById<ListView>(Resource.Id.feedItemsListView);

			this.progressDialog = new ProgressDialog(this);
			this.progressDialog.SetMessage("Ladataan...");

            //Ravintolat: 1. Piato, 2. Wilhelmiina, 3. Tilia, 4. Libri, 5. Lozzi, 6. Kahvila Syke, 7. Uno, 8. Ylistˆ, 9. Kvarkki, 10. Novelli, 11. Normaalikoulu
            string[] url = { "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=1408&language=fi" , "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=1402&language=fi" ,
            "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=1413&language=fi","http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=141301&language=fi",
            "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=1401&language=fi","http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=1405&language=fi",
            "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=1414&language=fi","http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=1403&language=fi",
            "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=140301&language=fi",
            "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=1409&language=fi","http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=1411&language=fi",
            "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentDay?costNumber=1404&language=fi"};

            this.GetRavintolatList(url); //piato

            //Aukioloajat: http://users.jyu.fi/~jualkaup/www/aukioloajat.rss

            //Haetaan nykyinen p‰iv‰m‰‰r‰ ja tallennetaan merkkijonoksi
            DateTime now = DateTime.Now.ToLocalTime();
            string currentTime = now.ToString("dd.MM.yyyy");

            //Luodaan toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            //Asetetaan toolbarin title n‰ytt‰m‰‰n p‰iv‰m‰‰r‰
            ActionBar.Title = "Sonatiina - " + currentTime;
        }

        private void GetRavintolatList(string[] url)
		{
			this.progressDialog.Show();

			Task<List<Ravintola>> task1 = Task.Factory.StartNew(() =>
			                                                   {
				return FeedService.GetRavintolat(url, true);
			}
			);

			Task task2 = task1.ContinueWith((antecedent) =>
			                                {
				try
				{
					this.progressDialog.Dismiss();
					this.lista = antecedent.Result;
                    this.PopulateListView(this.lista);
                                                }
				catch (AggregateException aex)
				{
					Toast.MakeText (this, aex.InnerException.Message, ToastLength.Short).Show ();
				}
			}, TaskScheduler.FromCurrentSynchronizationContext()
			);
		}

		private void PopulateListView(List<Ravintola> ravintolatList)
		{


            var adapter = new RavintolaListAdapter(this, ravintolatList);
			this.RavintolaListView.Adapter = adapter;
            adapter.NotifyDataSetChanged();
			this.RavintolaListView.ItemClick += OnListViewItemClick;
		}

		protected void OnListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			
			var t = lista[e.Position];
            var intent = new Intent(this, typeof(WilhelmiinaActivity));
            intent.PutExtra("rnimi", t.Nimi);
            intent.PutExtra("rkoodi",e.Position);
            StartActivity(intent);

            //var intent = new Intent(this, typeof(WilhelmiinaActivity));
            //StartActivity(intent);
			//Android.Widget.Toast.MakeText(this, t.Link, Android.Widget.ToastLength.Short).Show();
		}

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menu_about)
            {
                var intent = new Intent(this, typeof(AboutActivity));
                StartActivity(intent);
            }
            if (item.ItemId == Resource.Id.menu_preferences)
            {
                var intent = new Intent(this, typeof(SettingsActivity));
                StartActivity(intent);
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}


