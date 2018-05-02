using System;

namespace BiznisSloj.Datumi
{
    public class RazlikaDatuma
    {
        private readonly int[] _brojMjesecnihDana = {31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        private int _dani;
        private DateTime _doDatuma;
        private int _godine;
        private int _mjeseci;
        private DateTime _odDatuma;
        private double _ukupnoDana;


        public RazlikaDatuma(DateTime stariDatum, DateTime noviDatum)
        {
            _odDatuma = stariDatum;
            _doDatuma = noviDatum;
        }

        public Datum VratiIzracun()
        {
            if (_odDatuma > _doDatuma)
            {
                _odDatuma = _doDatuma;
                _doDatuma = _odDatuma;
            }

            //izračun
            var povecaj = RacunajVeljacu();
            //izračun mjeseci
            povecaj = VratiBrojMjeseci(povecaj);
            //izračun godina
            _godine = _doDatuma.Year - (_odDatuma.Year + povecaj);
            var vraćeniDatum = new Datum(_godine, _mjeseci, _dani, _ukupnoDana);
            _ukupnoDana = _doDatuma.Subtract(_odDatuma).TotalDays + 1;
            return vraćeniDatum;
        }

        private int RacunajVeljacu()
        {
            var povecaj = 0;

            if (_odDatuma.Day > _doDatuma.Day) povecaj = _brojMjesecnihDana[_odDatuma.Month - 1];

            /// ako je mjesec veljača
            /// if it's to _dani is less then from _dani
            if (povecaj == -1) povecaj = DateTime.IsLeapYear(_odDatuma.Year) ? 29 : 28;

            if (povecaj != 0)
            {
                _dani = _doDatuma.Day + povecaj - _odDatuma.Day;
                povecaj = 1;
            }
            else
            {
                _dani = _doDatuma.Day - _odDatuma.Day;
            }

            return povecaj;
        }

        private int VratiBrojMjeseci(int povecaj)
        {
            if (_odDatuma.Month + povecaj > _doDatuma.Month)
            {
                _mjeseci = _doDatuma.Month + 12 - (_odDatuma.Month + povecaj);
                povecaj = 1;
            }
            else
            {
                _mjeseci = _doDatuma.Month - (_odDatuma.Month + povecaj);
                povecaj = 0;
            }

            return povecaj;
        }
    }
}