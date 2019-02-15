using System;

namespace BiznisSloj.Porezi
{
    public class IzracunPoreznihKoeficijenata
    {
        private readonly decimal _prirez;
        public IzracunPoreznihKoeficijenata(decimal prirez)
        {
           _prirez = prirez; 
            IzracunajKoeficijente();
        }
        public decimal KoeficijentPoreza24 { get; set; }
        public decimal KoeficijentPoreza36 { get; set; }
        public decimal PoreznoPrirezniKoef {get; set; }

        public void IzracunajKoeficijente()
        {
            PoreznoPrirezniKoef = Math.Round((_prirez / 100) + 1, 4);
            KoeficijentPoreza24 = Math.Round(((24 * PoreznoPrirezniKoef) / (100 - (24 * PoreznoPrirezniKoef))) +1, 8);
            KoeficijentPoreza36 = Math.Round(((36 * PoreznoPrirezniKoef) / (100 - (36 * PoreznoPrirezniKoef))) +1, 8);
        }
    }
}
