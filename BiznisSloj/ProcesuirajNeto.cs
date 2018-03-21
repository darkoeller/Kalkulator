using System;
using BiznisSloj.Olaksice;
using BiznisSloj.Porezi;

namespace BiznisSloj
{
    public class ProcesuirajNeto
    {
        public ProcesuirajNeto(decimal neto, decimal faktor, decimal prirez)
        {
            Neto = neto;
            Faktor = faktor;
            Prirez = prirez;
        }

        public decimal Bruto { get; private set; }
        private decimal Faktor { get; }
        private decimal Neto { get; }
        private decimal Prirez { get; }
        private decimal KoefPrireza { get; set; }
        private decimal KoefPorezaPrireza24 { get; set; }
        private decimal KoefPorezaPrireza36 { get; set; }

        private decimal IzracunOlaksiceClanova()
        {
            var olaksica = new IzracunOlaksice(Faktor);
            var umanjenje = olaksica.VratiOlaksicu();
            return umanjenje;
        }

        public void Izracunaj()
        {
            var odbitak = IzracunOlaksiceClanova();
            var priporezkoef = new PorezniKoeficijenti(Prirez);
            KoefPrireza = priporezkoef.KoefPrireza;
            KoefPorezaPrireza24 = priporezkoef.KoefPorezaPrireza24;
            KoefPorezaPrireza36 = priporezkoef.KoefPorezaPrireza36;
            NadjiMetoduZaIzracun(Neto, odbitak);
        }

        //odabir izračuna poreza
        private void NadjiMetoduZaIzracun(decimal neto, decimal odbitak)
        {
            if (neto > 38496.60m - (4200.0m * KoefPrireza + (20966.0m - odbitak) * 0.36m * KoefPrireza))
                Bruto = Math.Round(CetvrtaMetoda(neto, odbitak), 2);
            else if (neto <= odbitak) Bruto = neto * 1.25m;
            else if (neto < 17500.00m - (4200.00m * KoefPrireza) + odbitak)
                Bruto = Math.Round(DrugaMetoda(neto, odbitak), 2);
            else if (neto > 17500.00m - (4200.0m * KoefPrireza) + odbitak)
                Bruto = Math.Round(TrecaMetoda(neto, odbitak), 2);
        }

        private decimal CetvrtaMetoda(decimal neto, decimal odbitak)
        {
            return 17500.00m + odbitak + ((neto - (17500.0m - (4200.0m * KoefPrireza) + odbitak)) * KoefPorezaPrireza36) + 9624.00m;
        }

        private decimal TrecaMetoda(decimal neto, decimal odbitak)
        {
            return (17500.0m + odbitak + (neto - (17500.0m - (4200.0m * KoefPrireza) + odbitak)) * KoefPorezaPrireza36) / 0.8m;
        }

        //računa neto bez poreza
        private decimal DrugaMetoda(decimal neto, decimal odbitak)
        {
            return ((neto - odbitak) * KoefPorezaPrireza24 + odbitak) / 0.8m;
        }
    }
}