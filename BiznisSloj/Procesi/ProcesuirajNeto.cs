using System;
using BiznisSloj.Olaksice;
using BiznisSloj.Porezi;

namespace BiznisSloj.Procesi
{
    public class ProcesuirajNeto
    {
        public ProcesuirajNeto(decimal neto, decimal faktor, decimal prirez, bool mirStup)
        {
            Neto = neto;
            Faktor = faktor;
            Prirez = prirez;
            MirStup = mirStup;
        }

        private decimal Faktor { get; }
        private decimal Neto { get; }
        private decimal Prirez { get; }
        private double KoefPrireza { get; set; }
        private double KoefPorezaPrireza24 { get; set; }
        private double KoefPorezaPrireza36 { get; set; }
        private bool MirStup { get; }

        private decimal IzracunOlaksiceClanova()
        {
            var olaksica = new IzracunOlaksice(Faktor);
            var umanjenje = olaksica.VratiOlaksicu();
            return umanjenje;
        }

        private void PostaviKoeficijentePorezaPrireza()
        {
            var priporezkoef = new PorezniKoeficijenti2(Prirez);
            KoefPrireza = priporezkoef.KoefPrireza;
            KoefPorezaPrireza24 = priporezkoef.KoefPorezaPrireza24;
            KoefPorezaPrireza36 = priporezkoef.KoefPorezaPrireza36;
        }

        public ProcesuirajPlacu Izracunaj()
        {
            var odbitak = IzracunOlaksiceClanova();
            PostaviKoeficijentePorezaPrireza();
            var izracunBruta = NadjiMetoduZaIzracun(Neto, odbitak);
            var placa = ProcesuirajBruto.VratiIzracunPlace(izracunBruta, Faktor, Prirez, MirStup);
            var usporedjeniIznosiBruta = new UsporediIVratiBrutoIznos(Neto, placa, Prirez, Faktor, MirStup).Usporedi();
            return usporedjeniIznosiBruta;
        }

        private decimal NadjiMetoduZaIzracun(decimal neto, decimal odbitak)
        {
            if (neto > 38496.60m -
                (4200.0m * (decimal) KoefPrireza + (20966.0m - odbitak) * 0.36m * (decimal) KoefPrireza))
                return Math.Round(CetvrtaMetoda(neto, odbitak), 2);
            if (neto <= odbitak) return neto * 1.25m;
            if (neto < 17500.00m - 4200.00m * (decimal) KoefPrireza + odbitak)
                return Math.Round(DrugaMetoda(neto, odbitak), 2);
            return neto > 17500.00m - 4200.0m * (decimal) KoefPrireza + odbitak
                ? Math.Round(TrecaMetoda(neto, odbitak), 2)
                : 0.0m;
        }

        private decimal CetvrtaMetoda(decimal neto, decimal odbitak)
        {
            return 17500.00m + odbitak + (neto - (17500.0m - 4200.0m * (decimal) KoefPrireza + odbitak)) *
                   (decimal) KoefPorezaPrireza36 +
                   9624.00m;
        }

        private decimal TrecaMetoda(decimal neto, decimal odbitak)
        {
            return (17500.0m + odbitak + (neto - (17500.0m - 4200.0m * (decimal) KoefPrireza + odbitak)) *
                    (decimal) KoefPorezaPrireza36) /
                   0.8m;
        }

        //računa neto bez poreza
        private decimal DrugaMetoda(decimal neto, decimal odbitak)
        {
            return ((neto - odbitak) * (decimal) KoefPorezaPrireza24 + odbitak) / 0.8m;
        }
    }
}