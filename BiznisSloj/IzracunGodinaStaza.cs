using System;

namespace BiznisSloj
{
    public class IzracunGodinaStaza
    {
        private readonly decimal _godine;
        private readonly decimal _koeficijent;

        public IzracunGodinaStaza(decimal godine, decimal koeficijent)
        {
            _godine = godine;
            _koeficijent = koeficijent;
        }

        public decimal Izracun() =>  Math.Round(_koeficijent * _godine * 0.5m / 100m, 2);
    }
}