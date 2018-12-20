using System;

namespace BiznisSloj.Stimul
{
    public class Stimulacija
    {
        private readonly decimal _satiRada;
        private decimal _bodovi;
        private readonly int _vrsta;
        private readonly decimal _koeficijent = 11.0172m;

        public Stimulacija(decimal getSatiRada, decimal getBodovi, int vrsta)
        {
            _satiRada = getSatiRada;
            _bodovi = getBodovi;
            _vrsta = vrsta;
        }

        public decimal Izracun()
        {
            decimal bod = 0;
            decimal izracun = 0;
            switch (_vrsta)
            {
                case 0:
                    izracun = (_bodovi * _koeficijent * _satiRada / 10)/2;
                    break;

                    case 1:
                        izracun = _bodovi * _koeficijent * _satiRada / 10;
                        break;
                    case 2:
                        bod = _bodovi * 1.15m;
                        _bodovi = bod - _bodovi;
                         izracun = _bodovi * _koeficijent * _satiRada;
                        break;
                    case 3:
                          bod = _bodovi * 1.2m;
                        _bodovi = bod - _bodovi;
                         izracun = _bodovi * _koeficijent * _satiRada;
                        break;
                   default:
                    return izracun;
            }

            return Math.Round(izracun, 2);
        }
        
    }
}