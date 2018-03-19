using System;

namespace BiznisSloj.Doprinosi
{
    public class RacunajBeneficiraniTriMjeseca
    {
        private  decimal Bruto { get; }

        public RacunajBeneficiraniTriMjeseca(decimal bruto)
        {
            Bruto = bruto;
        }

        private decimal PetOsamdesetTri { get; set; }
        private decimal DvaNulaJedan { get; set; }

        public void Izracun()
        {
            PetOsamdesetTri = Racunaj(new BeneficiraniPetOsamdesetTri());
            DvaNulaJedan = Racunaj(new BeneficiraniDvaNulaJedan());
        }

        public decimal VratiBeneficiraniTriMjeseca()
        {
            return Math.Round(PetOsamdesetTri + DvaNulaJedan, 2);
        }

        private decimal Racunaj(IDoprinosi doprinos)
        {
            return doprinos.RacunajDoprinos(Bruto);
        }
    }
}