using System;
using System.Windows;

namespace BiznisSloj
{
    public class Ovrha
    {
        private static readonly decimal ProsjecnoNeto = 5664.00m;

        private static readonly decimal MinimalniNeto = 3276.00m;

        public Ovrha(decimal neto)
        {
            Neto = neto;
        }

        public decimal ZaOvrsiti { get; private set; }

        private decimal Neto { get; }

        public decimal IzracunajOvrhu()
        {
            var netoIznos = Neto;

            if (Neto <= MinimalniNeto)
            {
                MessageBox.Show("Ovrha nije moguća zbog minimalnog neta \n ili pogrešno upisanog iznosa.");
                return Neto;
            }
            if (Neto >= ProsjecnoNeto)
            {
                netoIznos = 3776.00m;
                ZaOvrsiti = Neto - netoIznos;
                return netoIznos;
            }
            if (Neto > MinimalniNeto && Neto < ProsjecnoNeto)
            {
                netoIznos = Math.Round(netoIznos / 3 * 2, 2);
                ZaOvrsiti = Neto - netoIznos;
                return netoIznos;
            }
            return netoIznos;
        }
    }
}