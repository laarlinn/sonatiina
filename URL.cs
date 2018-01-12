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

namespace Sonatiina
{
    public class URL
    {
        //Ravintolat: 1. Piato, 2. Wilhelmiina, 3. Tilia, 4. Libri, 5. Lozzi, 6. Kahvila Syke,
        //7. Uno, 8. Ylistö, 9. Kvarkki, 10. Novelli, 11. Normaalikoulu
         private string[] weekURL = { "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=1408&language=fi",
        "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=1402&language=fi",
        "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=1413&language=fi",
        "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=141301&language=fi",
        "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=1401&language=fi",
        "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=1405&language=fi",
        "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=1414&language=fi",
        "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=1403&language=fi",
        "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=140301&language=fi",
        "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=1409&language=fi",
        "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=1411&language=fi",
        "http://www.sonaatti.fi/modules/MenuRss/MenuRss/CurrentWeek?costNumber=1404&language=fi"};

        //TODO Aukioloaikojen hakeminen RSS/json-feedistä
        private string[] SONAATTI_AUKIOLOAJAT = 
        {"ma-to 7.45-17.30, pe 7.45-16.00, la 8.30-15.00. Lounas ma-to 10.30-17.00, pe 10.30-16.00, la 11.00-15.00.",
        "ma-to 8.00-14.30, pe 8.00-14.00. Lounas ma-to 10.30-14.30, pe 10.30-14.00.",
        "ma-pe 9.00-14.00. Lounas 10.45-14.00.",
        "Ravintola Libri: ma-to 10.30-18.00, pe 10.30-17.00. Cafe Libri: ma-to 8.00-19.00, pe 8.00-17.00, la 11.00-15.00",
        "ma-pe 10.30-14.30, la 11.00-17.00, su 12.00-16.00",
        "ma-to 8.00-16.15, pe 8.00-14.30. Lounas 11.00-12.30.",
        "ma-to 8.00-16.15, pe 8.00-15.15. Lounas 10.30-14.00.",
        "ma-to 8.30-15.30, pe 8.30-15.00. Lounas ma-to 10.30-14.30, pe 10.30-14.00.",
        "ma-pe 10.30-13.00",
        "ma-to 8.30-19.30, pe 8.30-15.00. Lounas 10.30-13.30.",
        "-",
        "ma-to 8.00-16.00, pe 8.00-14.30. Lounas 11.00-13.00."};

        private string[] SONAATTI_OSOITTEET =
            {"Mattilanniemi 2 (Agora-rakennus)",
        "Ahlmaninkatu 2 (MaA-rakennus)",
        "Seminaarinkatu 15 (T-rakennus)",
        "Seminaarinkatu 15 (B-rakennus)",
        "Keskussairaalantie 4 (P-rakennus)",
        "Keskussairaalantie 4 (L-rakennus)",
        "Alvar Aallon katu 9",
        "Survontie 9 C (YFL-rakennus)",
        "Survontie 9 (YK-rakennus)",
        "Vapaudenkatu 39–41 (pääkirjasto)",
        "Yliopistonkatu 1 / Pitkäkatu 8",
        "Seminaarinkatu 15 (C-rakennus)"};

        public string getWeekURL(int i)
        {
            return weekURL[i];
        }

        public string[] getHours()
        {
            return SONAATTI_AUKIOLOAJAT;
        }

        public string[] getAddresses()
        {
            return SONAATTI_OSOITTEET;
        }

    }
}