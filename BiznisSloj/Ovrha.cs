using System;

namespace BiznisSloj
{
    public class Ovrha
    {
        private static readonly decimal ProsjecnoNeto = 5960.00m;
        private readonly byte _rbovrha;

        public Ovrha(decimal neto, byte rbovrha)
        {
           _rbovrha = rbovrha;
            Neto = neto;
        }

        public decimal ZaOvrsiti { get; private set; }

        private decimal Neto { get; }

        public decimal IzracunajOvrhu()
        {
            switch (_rbovrha)
            {
                case 1:
                    return IzracunPoClanku();
                case 2:
                    return IzracunUzdrzavanje();
                case 3:
                    return OvrhaZaUzdrzavanjeDjece();
            }

            return 0.0m;
        }

        private decimal IzracunPoClanku()
        {
            var netoIznos = Neto;
            var prolaz = ProcjenaClanak(netoIznos);
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

        private decimal IzracunUzdrzavanje()
        {
            var neto = Neto;
            if (neto < ProsjecnoNeto)
            {
                neto = Math.Round(Neto / 2, 2);
                ZaOvrsiti = neto;
                return neto;
            }

            if (neto < ProsjecnoNeto) return 0;
            ZaOvrsiti = Neto - 2980.0m;
            return 2980.0m;
        }


        private decimal OvrhaZaUzdrzavanjeDjece()
        {
            var neto = Neto;
            if (neto < ProsjecnoNeto)
            {
                ZaOvrsiti = Math.Round(neto / 4 * 3, 2);
                return Math.Round(neto / 4 * 1, 2);
            }

            if (neto < ProsjecnoNeto) return 0;
            ZaOvrsiti = neto - 1490.0m;
            return 1490.0m;
        }

        private static byte ProcjenaClanak(decimal netoIznos)
        {
            return netoIznos <= 5297.77m ? (byte) 1 : (byte) 2;
        }
    }
}