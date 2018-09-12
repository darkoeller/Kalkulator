using System;

namespace BiznisSloj.KoefSati
{
    public class KoefSatSamoPrva : KoeficSat
    {
        public KoefSatSamoPrva(decimal brojSati, decimal minuli, decimal bodovi, decimal koeficijent)
            : base(brojSati, minuli, bodovi, koeficijent)
        {
        }

        public override decimal RacunajKoefSat()
        {
            return Math.Round(Bodovi * BrojSati * Vrijednostboda * Koeficijent, 2);
        }
    }
}