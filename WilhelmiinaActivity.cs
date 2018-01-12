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
using System.Threading.Tasks;
using Android.Graphics;

namespace Sonatiina
{
	[Activity(Label = "WilhelmiinaActivity")]
	public class WilhelmiinaActivity : Activity
	{
        private List<Ravintola> lista;
        private ListView feedItemsListView;
        private ProgressDialog progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
            this.SetTheme(ChangeTheme.getTheme());
            SetContentView(Resource.Layout.Wilhelmiina);

            //Tuodaan activitylle ravintolan nimi ja sijainti listassa
            string rnimi = Intent.GetStringExtra("rnimi") ?? "Virhe ravintolan tietojen saannissa";
            int rkoodi = Intent.GetIntExtra("rkoodi", 0);

            //Asetetaan luotava listView layoutissa olevaksi
            this.feedItemsListView = this.FindViewById<ListView>(Resource.Id.feedItemsListView);

            //Latausdialogi
            this.progressDialog = new ProgressDialog(this);
            this.progressDialog.SetMessage("Ladataan...");

            //Tuodaan ravintolan tiedot URL-luokasta
            URL SonaattiData = new URL();
            string[] rssURL = { SonaattiData.getWeekURL(rkoodi) };
            string[] aoAjat = SonaattiData.getHours();
            string[] osoitteet = SonaattiData.getAddresses();

            //Haetaan netist‰ RSS-syˆte, jossa on ruokalistat
            this.GetFeedItemsList(rssURL);

            //Luodaan toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            //Asetetaan toolbarin title n‰ytt‰m‰‰n p‰iv‰m‰‰r‰
            ActionBar.Title = rnimi;
            ActionBar.SetHomeButtonEnabled(true); //Takaisin-painike p‰‰ll‰
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            //Asetetaan aukioloajat n‰kym‰‰n
            FindViewById<TextView>(Resource.Id.aukioloajat).Text = Resources.GetString(Resource.String.openhours) + aoAjat[rkoodi];

            //Asetetaan osoite n‰kym‰‰n
            FindViewById<TextView>(Resource.Id.osoite).Text = osoitteet[rkoodi];

            //Asetetaan v‰ri osoitteen taustalle
            FindViewById<FrameLayout>(Resource.Id.frameLayout1).SetBackgroundColor(SetColor());

        }

        private Color SetColor()
        {
            if (int.Parse(Android.OS.Build.VERSION.Sdk) >= 23)
            {
                if (ChangeTheme.getTheme() == Resource.Style.DarkTheme)
                {
                    return new Color(Application.Context.GetColor(Resource.Color.osoite_tausta_tumma));
                }
                if (ChangeTheme.getTheme() == Resource.Style.MyCustomTheme)
                {
                    return new Color(Application.Context.GetColor(Resource.Color.sonaatti_violetti));
                }
                if (ChangeTheme.getTheme() == Resource.Style.PinkTheme)
                {
                    return new Color(Application.Context.GetColor(Resource.Color.sonaatti_pinkki));
                }
                return Color.Black;
            }
            else
            {
                if (ChangeTheme.getTheme() == Resource.Style.DarkTheme)
                {
                    return new Color(Color.ParseColor("#1a1a1a"));
                }
                if (ChangeTheme.getTheme() == Resource.Style.MyCustomTheme)
                {
                    return new Color(Color.ParseColor("#6a6ff2"));
                }
                if (ChangeTheme.getTheme() == Resource.Style.PinkTheme)
                {
                    return new Color(Color.ParseColor("#9e069e"));
                }
                return Color.Black;
            }
        }

        private void GetFeedItemsList(string[] url)
        {
            this.progressDialog.Show();

            Task<List<Ravintola>> task1 = Task.Factory.StartNew(() =>
            {
                return FeedService.GetRavintolat(url, false);
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
                    Toast.MakeText(this, aex.InnerException.Message, ToastLength.Short).Show();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext()
            );
        }

        //T‰ytet‰‰n listView FeedItemeill‰
        private void PopulateListView(List<Ravintola> ravintolatList)
        {
            var adapter = new RavintolaListAdapter(this, ravintolatList);
            this.feedItemsListView.Adapter = adapter;
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
    }
}