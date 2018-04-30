using System.Collections.Generic;
using System.Linq;

namespace BiznisSloj.Porezi
{
    public class PorezniKoeficijenti
    {
        public PorezniKoeficijenti(decimal prirez)
        {
            Stopa = prirez;
            OdrediKoeficijente(Stopa);
        }

        private PorezniKoeficijenti()
        {
        }

        private decimal Stopa { get; set; }
        public decimal KoefPrireza { get; private set; }
        public decimal KoefPorezaPrireza24 { get; private set; }
        public decimal KoefPorezaPrireza36 { get; private set; }

        private void OdrediKoeficijente(decimal stopa)
        {
            var rezultat = PoreznoPrirezniKoef();

            var porezniKoeficijentis = rezultat as IList<PorezniKoeficijenti> ?? rezultat.ToList();
            KoefPrireza = porezniKoeficijentis
                .AsParallel()
                .Where(r => r.Stopa == stopa)
                .Select(r => r.KoefPrireza)
                .FirstOrDefault();
            KoefPorezaPrireza24 = porezniKoeficijentis
                .AsParallel()
                .Where(r => r.Stopa == stopa)
                .Select(r => r.KoefPorezaPrireza24)
                .FirstOrDefault();
            KoefPorezaPrireza36 = porezniKoeficijentis
                .AsParallel()
                .Where(r => r.Stopa == stopa)
                .Select(r => r.KoefPorezaPrireza36)
                .FirstOrDefault();
        }

        private static IEnumerable<PorezniKoeficijenti> PoreznoPrirezniKoef()
        {
            var porezniKoef = new List<PorezniKoeficijenti>
            {
                new PorezniKoeficijenti
                {
                    Stopa = 0m,
                    KoefPrireza = 1m,
                    KoefPorezaPrireza24 = 1.315789474m,
                    KoefPorezaPrireza36 = 1.562500000m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 1m,
                    KoefPrireza = 1.01m,
                    KoefPorezaPrireza24 = 1.319957761m,
                    KoefPorezaPrireza36 = 1.571338781m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 2m,
                    KoefPrireza = 1.02m,
                    KoefPorezaPrireza24 = 1.324152542m,
                    KoefPorezaPrireza36 = 1.580278129m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 3m,
                    KoefPrireza = 1.03m,
                    KoefPorezaPrireza24 = 1.328374070m,
                    KoefPorezaPrireza36 = 1.589319771m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 4m,
                    KoefPrireza = 1.04m,
                    KoefPorezaPrireza24 = 1.332622601m,
                    KoefPorezaPrireza36 = 1.598465473m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 5m,
                    KoefPrireza = 1.05m,
                    KoefPorezaPrireza24 = 1.336898396m,
                    KoefPorezaPrireza36 = 1.607717042m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 6m,
                    KoefPrireza = 1.06m,
                    KoefPorezaPrireza24 = 1.341201717m,
                    KoefPorezaPrireza36 = 1.617076326m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 6.25m,
                    KoefPrireza = 1.06m,
                    KoefPorezaPrireza24 = 1.342281879m,
                    KoefPorezaPrireza36 = 1.619433198m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 6.5m,
                    KoefPrireza = 1.07m,
                    KoefPorezaPrireza24 = 1.343363783m,
                    KoefPorezaPrireza36 = 1.621796951m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 7m,
                    KoefPrireza = 1.07m,
                    KoefPorezaPrireza24 = 1.345532831m,
                    KoefPorezaPrireza36 = 1.626545218m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 7.5m,
                    KoefPrireza = 1.08m,
                    KoefPorezaPrireza24 = 1.347708895m,
                    KoefPorezaPrireza36 = 1.631321370m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 8m,
                    KoefPrireza = 1.08m,
                    KoefPorezaPrireza24 = 1.349892009m,
                    KoefPorezaPrireza36 = 1.636125654m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 9m,
                    KoefPrireza = 1.09m,
                    KoefPorezaPrireza24 = 1.354279523m,
                    KoefPorezaPrireza36 = 1.645819618m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 10m,
                    KoefPrireza = 1.1m,
                    KoefPorezaPrireza24 = 1.358695652m,
                    KoefPorezaPrireza36 = 1.655629139m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 12m,
                    KoefPrireza = 1.12m,
                    KoefPorezaPrireza24 = 1.367614880m,
                    KoefPorezaPrireza36 = 1.675603217m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 13m,
                    KoefPrireza = 1.13m,
                    KoefPorezaPrireza24 = 1.372118551m,
                    KoefPorezaPrireza36 = 1.685772084m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 14m,
                    KoefPrireza = 1.14m,
                    KoefPorezaPrireza24 = 1.376651982m,
                    KoefPorezaPrireza36 = 1.696065129m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 15m,
                    KoefPrireza = 1.15m,
                    KoefPorezaPrireza24 = 1.381215470m,
                    KoefPorezaPrireza36 = 1.706484642m
                },
                new PorezniKoeficijenti
                {
                    Stopa = 18m,
                    KoefPrireza = 1.18m,
                    KoefPorezaPrireza24 = 1.395089286m,
                    KoefPorezaPrireza36 = 1.738525730m
                }
            };
            return porezniKoef;
        }
    }
}