using System;

namespace BiznisSloj.Doprinosi
{
    public class RacunajBeneficiraniCetiriMjeseca
    {
        public RacunajBeneficiraniCetiriMjeseca(decimal bruto)
        {
            Bruto = bruto;
        }

        private decimal Bruto { get; }

        private decimal OsamTridesetDevet { get; set; }
        private decimal DvaOsamdesetDevet { get; set; }

        public void Izracun()
        {
            OsamTridesetDevet = Racunaj(new BeneficiraniOsamTridesetDevet());
            DvaOsamdesetDevet = Racunaj(new BeneficiraniDvaOsamdesetDevet());
        }

        public decimal VratiBeneCetiriMjeseca()
        {
            return Math.Round(OsamTridesetDevet + DvaOsamdesetDevet, 2);
        }

        private decimal Racunaj(IDoprinosi doprinos)
        {
            return doprinos.RacunajDoprinos(Bruto);
        }
    }
}