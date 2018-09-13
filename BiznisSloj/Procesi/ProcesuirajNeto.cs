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
            if (neto > (odbitak * (36m + 0.36m * Prirez) - 11758.56m * Prirez + 2673744m) / 100m)
                return Math.Round(PetaMetoda(neto, odbitak), 2);
            if (neto <= (odbitak * (36m + 0.36m * Prirez) - 11272.99m * Prirez + 2587420.8m) / 100m &&
                neto > odbitak + 17500m - 175m * 24m - 1.75m * 24m * Prirez)
                return Math.Round(CetvrtaMetoda(neto, odbitak), 2);
            if (neto > odbitak && neto <= odbitak + 17500m - 175m * 24m - 1.75m * 24m * Prirez)
                return Math.Round(DrugaMetoda(neto, odbitak), 2);
            if (neto <= odbitak) return neto * 1.25m;
            return 0.0m;
        }

        private decimal PetaMetoda(decimal neto, decimal odbitak) => (100 * neto - odbitak * (36m + 0.36m * Prirez) - 5564.64m * Prirez + 405936m) / (64m - 0.36m * Prirez);
 
        private decimal CetvrtaMetoda(decimal neto, decimal odbitak) => (neto - odbitak * (0.36m + 0.0036m * Prirez) - 21m * Prirez - 2100m) / (0.512m - 0.00288m * Prirez);

         private decimal DrugaMetoda(decimal neto, decimal odbitak) => (neto - odbitak * (0.24m + 0.0024m * Prirez)) / (0.608m - 0.00192m * Prirez);
    }
}