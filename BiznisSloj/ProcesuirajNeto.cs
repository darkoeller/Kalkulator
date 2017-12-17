using System;
using BiznisSloj.Olaksice;
using BiznisSloj.Porezi;

namespace BiznisSloj
{
    public class ProcesuirajNeto
    {
        private static readonly decimal PorezMax = 17500.0m;
        private static readonly decimal PorKoef24 = 4200.0m;

        public ProcesuirajNeto(decimal neto, decimal faktor, decimal prirez, bool chekdoprinosi)
        {
            Neto = neto;
            Faktor = faktor;
            Prirez = prirez;
            CheckDoprinosi = chekdoprinosi;
        }

        public decimal Bruto { get; private set; }
        private decimal Faktor { get; }
        private decimal Neto { get; }
        private decimal Prirez { get; }
        private decimal KoefPrireza { get; set; }
        private decimal KoefPorezaPrireza24 { get; set; }
        private decimal KoefPorezaPrireza36 { get; set; }
        private bool CheckDoprinosi { get; }

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
            if (neto > 37147.60m - (PorKoef24 * KoefPrireza + (19647.0m - odbitak) * 0.36m * KoefPrireza) &&
                CheckDoprinosi)
                Bruto = Math.Round(CetvrtaMetoda(neto, odbitak), 2);
            else if (neto <= odbitak) Bruto = neto * 1.25m;
            else if (neto < PorezMax - PorKoef24 * KoefPrireza + odbitak)
                Bruto = Math.Round(DrugaMetoda(neto, odbitak), 2);
            else if (neto > PorezMax - PorKoef24 * KoefPrireza + odbitak)
                Bruto = Math.Round(TrecaMetoda(neto, odbitak), 2);
        }

        private decimal CetvrtaMetoda(decimal neto, decimal odbitak)
        {
            return PorezMax + odbitak + (neto - (PorezMax - PorKoef24 * KoefPrireza + odbitak)) * KoefPorezaPrireza36 +
                   9286.80m;
        }

        private decimal TrecaMetoda(decimal neto, decimal odbitak)
        {
            return (PorezMax + odbitak + (neto - (PorezMax - PorKoef24 * KoefPrireza + odbitak)) *
                    KoefPorezaPrireza36) /
                   0.8m;
        }

        //računa neto bez poreza
        private decimal DrugaMetoda(decimal neto, decimal odbitak)
        {
            return ((neto - odbitak) * KoefPorezaPrireza24 + odbitak) / 0.8m;
        }
    }
}