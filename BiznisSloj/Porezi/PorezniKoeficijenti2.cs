﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.Porezi
{
    [JsonObject("Koeficijenti")]
    public class PorezniKoeficijenti2 : ValueObject<PorezniKoeficijenti2>
    {
        public PorezniKoeficijenti2(decimal prirez)
        {
            Stopa = (double) prirez;
            OdrediKoeficijente(Stopa);
        }

        private PorezniKoeficijenti2() {}

        private double Stopa { get; set; }
        public double KoefPrireza { get; private set; }
        public double KoefPorezaPrireza24 { get; private set; }
        public double KoefPorezaPrireza36 { get; private set; }

        protected override bool EqualsCore(PorezniKoeficijenti2 other) => Stopa.Equals(other?.Stopa)
                                                                          && KoefPrireza.Equals(other?.KoefPrireza)
                                                                          && KoefPorezaPrireza24.Equals(other?.KoefPorezaPrireza24)
                                                                          && KoefPorezaPrireza36.Equals(other?.KoefPorezaPrireza36);

        protected override int GetHashCodeCore()
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

            var porezniKoeficijentis = rezultat as IList<PorezniKoeficijenti2> ?? rezultat.ToList().AsReadOnly();
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
            IList<PorezniKoeficijenti2> listaPrireza = item.AsParallel().Select(p => new PorezniKoeficijenti2
            {
                Stopa = (double) p[nameof(Stopa)],
                KoefPrireza = (double) p[nameof(KoefPrireza)],
                KoefPorezaPrireza24 = (double) p[nameof(KoefPorezaPrireza24)],
                KoefPorezaPrireza36 = (double) p[nameof(KoefPorezaPrireza36)]
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
            var stope = from p in item.AsParallel().AsOrdered() select (string) p[nameof(Stopa)];
            return stope.AsReadOnly();
        }
    }
}