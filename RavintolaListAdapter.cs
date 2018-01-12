using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Android;
using System.Text.RegularExpressions;
using Android.Text;
using Android.Graphics;

namespace Sonatiina
{
	public class RavintolaListAdapter : BaseAdapter<Ravintola>
	{
		protected Activity context = null;
		protected List<Ravintola> ravintolatList = new List<Ravintola>();


		public RavintolaListAdapter(Activity context, List<Ravintola> ravintolatList)
			: base()
		{
			this.context = context;
			this.ravintolatList = ravintolatList;

		}

		public override Ravintola this[int position]
		{
			get { return this.ravintolatList[position]; }
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override int Count
		{
			get { return this.ravintolatList.Count; }
		}

        /// <summary>
        /// Asettaa ja muotoilee tekstin.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="convertView"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
		public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var Ravintola = this.ravintolatList[position];
            var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.FeedItemListItemLayout, parent, false)) as LinearLayout;
            //string del = "<br>";
            string del2 = "()";
            //feedItem.Description = feedItem.Description.Replace(del, "");
            Ravintola.Ruokalista = Ravintola.Ruokalista.Replace(del2, "");
            Ravintola.Ruokalista = FormatText(Ravintola.Ruokalista);
            SpannedString text = new SpannedString(Html.FromHtml(Ravintola.Ruokalista));
            //feedItem.Description = Regex.Replace(feedItem.Description, @"^\s*$\n", string.Empty, RegexOptions.Multiline);
            view.FindViewById<TextView>(Resource.Id.title).Text = Ravintola.Nimi;
            view.FindViewById<TextView>(Resource.Id.creator).TextFormatted = text;
            view.FindViewById<TextView>(Resource.Id.creator).SetTextColor(setDescColor());
            view.FindViewById<TextView>(Resource.Id.title).SetTextColor(setTitleColor());

            //view.FindViewById<TextView>(Resource.Id.creator).SetTextAppearance(ChangeTheme.getTheme());
            //view.FindViewById<TextView>(Resource.Id.title).SetTextAppearance(ChangeTheme.getTheme());
            //view.FindViewById<TextView>(Resource.Id.creator)
            return view;
        }

        public static Color setDescColor()
        {
            if (int.Parse(Android.OS.Build.VERSION.Sdk) >= 23)
            {
                if (ChangeTheme.getTheme() == Resource.Style.DarkTheme)
                {
                    return new Color(Application.Context.GetColor(Resource.Color.lista_kuvaus_kirkas));
                }
                if (ChangeTheme.getTheme() == Resource.Style.MyCustomTheme)
                {
                    return new Color(Application.Context.GetColor(Resource.Color.lista_kuvaus_tumma));
                }
                if (ChangeTheme.getTheme() == Resource.Style.PinkTheme)
                {
                    return new Color(Application.Context.GetColor(Resource.Color.valkoinen));
                }
                return Color.Black;

            }
            else
            {
                if (ChangeTheme.getTheme() == Resource.Style.DarkTheme)
                {
                    return new Color(Color.ParseColor("#ffffff"));
                }
                if (ChangeTheme.getTheme() == Resource.Style.MyCustomTheme)
                {
                    return new Color(Color.ParseColor("#050505"));
                }
                if (ChangeTheme.getTheme() == Resource.Style.PinkTheme)
                {
                    return new Color(Color.ParseColor("#ffffff"));
                }
                return Color.Black;
            }
        }

        public static Color setTitleColor()
        {
            if (int.Parse(Android.OS.Build.VERSION.Sdk) >= 23)
            {

                if (ChangeTheme.getTheme() == Resource.Style.DarkTheme)
                {
                    return new Color(Application.Context.GetColor(Resource.Color.lista_otsikko_vihrea));
                }
                if (ChangeTheme.getTheme() == Resource.Style.MyCustomTheme)
                {
                    return new Color(Application.Context.GetColor(Resource.Color.lista_otsikko_vihrea));
                }
                if (ChangeTheme.getTheme() == Resource.Style.PinkTheme)
                {
                    return new Color(Application.Context.GetColor(Resource.Color.valkoinen));
                }
                return Color.Black;
            }
            else
            {
                if (ChangeTheme.getTheme() == Resource.Style.DarkTheme)
                {
                    return new Color(Color.ParseColor("#93B233"));
                }
                if (ChangeTheme.getTheme() == Resource.Style.MyCustomTheme)
                {
                    return new Color(Color.ParseColor("#93B233"));
                }
                if (ChangeTheme.getTheme() == Resource.Style.PinkTheme)
                {
                    return new Color(Color.ParseColor("#ffffff"));
                }
                return Color.Black;
            }
        }


        public static string FormatText(string s)
        {
            s = s.Replace("SALAATTILOUNAS", "<b>SALAATTILOUNAS:</b>");
            s = s.Replace("KEITTOLOUNAS:", "<b>KEITTOLOUNAS:</b>");
            s = s.Replace("Keittolounas:", "<b>KEITTOLOUNAS:</b>");
            s = s.Replace("KASVISLOUNAS:", "<b>KASVISLOUNAS:</b>");
            s = s.Replace("LOUNAS:", "<b>LOUNAS:</b>");
            s = s.Replace("PAISTOPISTE:", "<b>PAISTOPISTE:</b>");
            s = s.Replace("JÄLKIRUOKA:", "<b>JÄLKIRUOKA:</b>");
            return s;
        }
	}
}