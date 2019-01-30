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
            if (neto <= odbitak) return neto * 1.25m;

            if (neto < (30000.0m - (7200.0m * (decimal)KoefPrireza) + odbitak))
            return Math.Round(DrugaMetoda(neto, odbitak), 2);

            //sa ograničenjem doprinosa
            if (neto > (30000.0m - (7200.0m * (decimal)KoefPrireza)+ odbitak ) || neto < 40550.0m - (7200.0m * (decimal)KoefPrireza + (10500.0m - odbitak) * (decimal)KoefPrireza))
            return Math.Round(PetaMetoda(neto, odbitak), 2);
            //bez ograničenja
            //if (neto >(30000.0m -(7200.0m * (decimal)KoefPrireza) + odbitak))
            //    return Math.Round(CetvrtaMetoda(neto, odbitak), 2);
            
            return 0.0m;
        }

        private decimal PetaMetoda(decimal neto, decimal odbitak) => 30000.0m + odbitak + (neto - (30000.0m - (7200.0m * (decimal)KoefPrireza)+ odbitak)) * (decimal)KoefPorezaPrireza36 + 10137.6m;
        //bez ograničenja
        //private decimal CetvrtaMetoda(decimal neto, decimal odbitak) => ((30000.0m + odbitak + (neto - (30000.0m - (7200.0m * (decimal)KoefPrireza) + odbitak)) * (decimal)KoefPorezaPrireza36)/ 0.8m);

        private decimal DrugaMetoda(decimal neto, decimal odbitak) => ((((neto - odbitak) * (decimal)KoefPorezaPrireza24)) + odbitak) / 0.8m;
    }
}