using System;

namespace BiznisSloj.Doprinosi
{
    public class RacunajBeneficiraniCetiriMjeseca
    {
        private readonly decimal _bruto;

        public RacunajBeneficiraniCetiriMjeseca(decimal bruto)
        {
            _bruto = bruto;
        }

        private decimal OsamTridesetDevet { get; set; }
        private decimal DvaOsamdesetDevet { get; set; }

        public void Izracun()
        {
            OsamTridesetDevet = Racunaj(new BeneficiraniOsamTridesetDevet(_bruto));
            DvaOsamdesetDevet = Racunaj(new BeneficiraniDvaOsamdesetDevet(_bruto));
        }

        public decimal VratiBeneCetiriMjeseca()
        {
            return Math.Round(OsamTridesetDevet + DvaOsamdesetDevet, 2);
        }

        private static decimal Racunaj(Doprinos doprinos)
        {
            return doprinos.RacunajDoprinos();
        }
    }
}