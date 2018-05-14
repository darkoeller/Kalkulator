using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.Porezi
{
    public class PorezniKoeficijenti2
    {
        public PorezniKoeficijenti2(decimal prirez)
        {
            Stopa = (double) prirez;
            OdrediKoeficijente(Stopa);
        }

        private PorezniKoeficijenti2()
        {
        }

        private double Stopa { get; set; }
        public double KoefPrireza { get; private set; }
        public double KoefPorezaPrireza24 { get; private set; }
        public double KoefPorezaPrireza36 { get; private set; }

        private bool Equals(PorezniKoeficijenti2 other)
        {
            return Stopa.Equals(other.Stopa)
                   && KoefPrireza.Equals(other.KoefPrireza)
                   && KoefPorezaPrireza24.Equals(other.KoefPorezaPrireza24)
                   && KoefPorezaPrireza36.Equals(other.KoefPorezaPrireza36);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is PorezniKoeficijenti2 koeficijenti && Equals(koeficijenti);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Stopa.GetHashCode();
                hashCode = (hashCode * 397) ^ KoefPrireza.GetHashCode();
                hashCode = (hashCode * 397) ^ KoefPorezaPrireza24.GetHashCode();
                hashCode = (hashCode * 397) ^ KoefPorezaPrireza36.GetHashCode();
                return hashCode;
            }
        }

        private void OdrediKoeficijente(double stopa)
        {
            var rezultat = PoreznoPrirezniKoef();

            var porezniKoeficijentis = rezultat as IList<PorezniKoeficijenti2> ?? rezultat.ToList();
            KoefPrireza = porezniKoeficijentis
                .AsParallel()
                .Where(r => r.Stopa.Equals(stopa))
                .Select(r => r.KoefPrireza)
                .FirstOrDefault();
            KoefPorezaPrireza24 = porezniKoeficijentis
                .AsParallel()
                .Where(r => r.Stopa.Equals(stopa))
                .Select(r => r.KoefPorezaPrireza24)
                .FirstOrDefault();
            KoefPorezaPrireza36 = porezniKoeficijentis
                .AsParallel()
                .Where(r => r.Stopa.Equals(stopa))
                .Select(r => r.KoefPorezaPrireza36)
                .FirstOrDefault();
        }

        private static IEnumerable<PorezniKoeficijenti2> PoreznoPrirezniKoef()
        {
            var item = VratiJArrayKoeficijenata();
            IList<PorezniKoeficijenti2> listaPrireza = item.Select(p => new PorezniKoeficijenti2
            {
                Stopa = (double) p["Stopa"],
                KoefPrireza = (double) p["KoefPrireza"],
                KoefPorezaPrireza24 = (double) p["KoefPorezaPrireza24"],
                KoefPorezaPrireza36 = (double) p["KoefPorezaPrireza36"]
            }).ToList();
            return listaPrireza;
        }

        private static JArray VratiJArrayKoeficijenata()
        {
            var jsonObject = File.ReadAllText(@"PorezneStope.json");
            var rss = JObject.Parse(jsonObject);
            var item = (JArray) rss["StopePoreza"];
            return item;
        }

        public static IEnumerable<string> VratiStopePrireza()
        {
            var item = VratiJArrayKoeficijenata();
            var stope = from p in item select (string) p["Stopa"];
            return stope;
        }
    }
}