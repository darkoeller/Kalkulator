using System;

namespace BiznisSloj
{
    public class MinuliRad
    {
        private static readonly decimal VrijednostBoda = 11.0172m;
        private readonly decimal _brojSati;
        private readonly decimal _minuli;

        public MinuliRad(decimal brojSati, decimal minuli)
        {
            _brojSati = brojSati;
            _minuli = minuli;
        }

        public decimal Izracun()
        {
            return Math.Round(_brojSati * _minuli * VrijednostBoda, 2);
        }
    }
}