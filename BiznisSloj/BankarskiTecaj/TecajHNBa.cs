using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.BankarskiTecaj
{
    public struct TecajHnBa : ITecaj
    {
        public decimal VratiEuro()
        {
            try
            {
                var hnbTecaj = NadjiSaWebaHnBa();
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
            return tecaj;
        }

    }
}