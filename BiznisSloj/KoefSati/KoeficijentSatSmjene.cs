using System;

namespace BiznisSloj.KoefSati
{
    public class KoeficijentSatSmjene : KoeficSat
    {
        //private decimal Koeficijent { get; set; }
        public KoeficijentSatSmjene(decimal brojSati, decimal minuli, decimal bodovi, decimal koeficijent)
            : base(brojSati, minuli, bodovi, koeficijent)
        {
        }

        public override decimal RacunajKoefSat()
        {
            Koeficijent = Koeficijent - 1.0m;
            var izracun = Bodovi*BrojSati*Vrijednostboda;
            var dodatak = (Bodovi + Minuli)*Vrijednostboda*BrojSati*Koeficijent;
            return Math.Round(izracun + dodatak, 2);
        }
    }
}