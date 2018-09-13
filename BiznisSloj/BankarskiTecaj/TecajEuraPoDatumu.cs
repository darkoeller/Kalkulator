using System;
using System.Globalization;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.BankarskiTecaj
{
    public class TecajEuraPoDatumu
    {
        private readonly DateTime? _datum;

        public TecajEuraPoDatumu(DateTime? datum)  => _datum = datum;

        private string FormatiranjeAdrese(string datum)
        {
            var bilder = new StringBuilder(" http://api.hnb.hr/tecajn/v1?datum=");
            bilder.Append(datum + "&valuta=EUR");
            var iznos = VratiDatum(bilder.ToString());
            return iznos;
        }

        private string VratiDatum(string novo)
        {
            var tecaj = 0.0m;
            var jsonObject = new WebClient().DownloadString(novo);
            var rss = JArray.Parse(jsonObject);
            foreach (var parsedObject in rss.Children<JObject>())
            foreach (var parsedProperty in parsedObject.Properties())
            {
                var propertyName = parsedProperty.Name;
                if (!propertyName.Equals("Srednji za devize")) continue;
                var propertyValue = (string) parsedProperty.Value;
                decimal.TryParse(propertyValue, out tecaj);
            }

            return tecaj.ToString(CultureInfo.InvariantCulture);
        }

        public string OblikujDatum()
        {
            if (_datum == null) return "";
            var godina = _datum.Value.Year.ToString();
            var mjesec = _datum.Value.Month.ToString();
            if (mjesec.Length == 1) mjesec = "0" + mjesec;
            var dan = _datum.Value.Day.ToString();
            if (dan.Length == 1) dan = "0" + dan;
            var noviDatum = godina + "-" + mjesec + "-" + dan;
            var vraceno = FormatiranjeAdrese(noviDatum);
            return vraceno;
        }
    }
}