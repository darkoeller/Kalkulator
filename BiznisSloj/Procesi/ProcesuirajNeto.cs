﻿using System;
using BiznisSloj.Olaksice;
using BiznisSloj.Porezi;

namespace BiznisSloj.Procesi
{
    public class ProcesuirajNeto
    {
        public ProcesuirajNeto(decimal neto, decimal faktor, decimal prirez, bool stup1I2)
        {
            Neto = neto;
            Faktor = faktor;
            Prirez = prirez;
            MirStup = stup1I2;
        }

        private decimal Faktor { get; }
        private decimal Neto { get; }
        private decimal Prirez { get; }
        private decimal KoefPrireza { get; set; }
        private decimal KoefPorezaPrireza24 { get; set; }
        private decimal KoefPorezaPrireza36 { get; set; }
        private bool MirStup { get; }

        private decimal IzracunOlaksiceClanova()
        {
            var olaksica = new IzracunOlaksice(Faktor);
            var umanjenje = olaksica.VratiOlaksicu();
            return umanjenje;
        }

        public ProcesuirajPlacu Izracunaj()
        {
            var odbitak = IzracunOlaksiceClanova();
            var priporezkoef = new PorezniKoeficijenti(Prirez);
            KoefPrireza = priporezkoef.KoefPrireza;
            KoefPorezaPrireza24 = priporezkoef.KoefPorezaPrireza24;
            KoefPorezaPrireza36 = priporezkoef.KoefPorezaPrireza36;
            var izracunBruta = Math.Round(NadjiMetoduZaIzracun(Neto, odbitak), 2);
            var placa = new ProcesuirajPlacu(izracunBruta, Prirez, MirStup, Faktor);
            var usporedjeniIznosiBruta = new UsporediIVratiBrutoIznos(Neto, placa, Prirez, Faktor,MirStup).Usporedi();
            return usporedjeniIznosiBruta  ;
        }

        //odabir izračuna poreza
        private decimal  NadjiMetoduZaIzracun(decimal neto, decimal odbitak)
        {
            if (neto > 38496.60m - (4200.0m * KoefPrireza + (20966.0m - odbitak) * 0.36m * KoefPrireza))
              return  Math.Round(CetvrtaMetoda(neto, odbitak), decimals: 2);
            if (neto <= odbitak) return  neto * 1.25m;
            if (neto < 17500.00m - (4200.00m * KoefPrireza) + odbitak)
               return  Math.Round(DrugaMetoda(neto, odbitak), decimals: 2);
            if (neto > 17500.00m - (4200.0m * KoefPrireza) + odbitak)
              return   Math.Round(TrecaMetoda(neto, odbitak), decimals: 2);
            return 0.0m;
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