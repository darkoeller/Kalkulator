using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.BankarskiTecaj
{
    public struct TecajHnBa : ITecaj
    {
        private static bool _prolaz;

        public decimal VratiEuro()
        {
            try
            {
                var hnbTecaj = NadjiSaWebaHnBa();
                //if (_prolaz) return hnbTecaj;
                //var tecaj = VratiTecajSaNabaveNeta();
                return hnbTecaj;
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Došlo je do pogreške prilikom prezimanja podatka, \n provjerite da li imate pristup internetu"
                    , "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return 0.0m;
            }
        }

        //private static decimal VratiTecajSaNabaveNeta()
        //{
        //    var client = new HttpClient();
        //    var web = FormirajWebString();
        //    var content = client.GetStringAsync(web).Result;
        //    content = content.Replace(".", ",");
        //    if (string.IsNullOrEmpty(content)) return 0.0m;
        //    decimal.TryParse(content, out var tecaj);
        //    return tecaj;
        //}

        private static decimal NadjiSaWebaHnBa()
        {
            var tecaj = 0.0m;
            var jsonObject = new HttpClient().GetStringAsync(@"http://api.hnb.hr/tecajn/v1?valuta=EUR").Result;
            var rss = JArray.Parse(jsonObject);
            foreach (var parsedObject in rss.Children<JObject>())
            foreach (var parsedProperty in parsedObject.Properties())
            {
                var propertyName = parsedProperty.Name;
                if (!propertyName.Equals("Srednji za devize")) continue;
                var propertyValue = (string) parsedProperty.Value;
                decimal.TryParse(propertyValue, out tecaj);
            }
           // _prolaz = true;
            return tecaj;
        }

        //private static string FormirajWebString()
        //{
        //    var web = DateTime.Today.ToShortDateString().Remove(8,1);
        //    var bilder = new StringBuilder("https://www.nabava.net/labs/hnb-tecaj/p/" + web + "/srednji/eur");
        //    return bilder.ToString();
        //}
    }
}