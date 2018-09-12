using System;

namespace BiznisSloj
{
    public class DodatakNaPlacu
    {
        private static readonly decimal Koeficijent = 9.1954m;
        private readonly decimal _brojSati;

        public DodatakNaPlacu(decimal brojSati) => _brojSati = brojSati;

        public decimal Izracun() => Math.Round(_brojSati * Koeficijent, 2);
    }
}