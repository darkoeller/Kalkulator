using System;

namespace BiznisSloj
{
    public class Ovrha
    {
        private static readonly decimal ProsjecnoNeto = 5960.00m;

        private static readonly decimal MinimalniNeto = 3439.80m;

        public Ovrha(decimal neto)
        {
            Neto = neto;
        }

        public decimal ZaOvrsiti { get; private set; }

        private decimal Neto { get; }

        public decimal IzracunajOvrhu()
        {
            var netoIznos = Neto;
            var prolaz = Procjena(netoIznos);
            switch (prolaz)
            {
                case 1:
                    netoIznos = Math.Round(netoIznos / 4 * 3, 2);
                    break;
                case 2:
                    netoIznos = 3973.33m;
                    break;
            }

            ZaOvrsiti = Neto - netoIznos;
            return netoIznos;
        }

        private static int Procjena(decimal netoIznos)
        {
            return netoIznos <= 5297.77m ? 1 : 2;
        }
    }
}