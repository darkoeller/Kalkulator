using System;

namespace BiznisSloj.Doprinosi
{
    public class RacunajBeneficiraniTriMjeseca
    {
        private readonly decimal _bruto;

        public RacunajBeneficiraniTriMjeseca(decimal bruto)
        {
            _bruto = bruto;
        }

        public decimal PetOsamdesetTri { get; set; }
        public decimal DvaNulaJedan { get; set; }

        public void Izracun()
        {
            PetOsamdesetTri = Racunaj(new BeneficiraniPetOsamdesetTri(_bruto));
            DvaNulaJedan = Racunaj(new BeneficiraniDvaNulaJedan(_bruto));
        }

        public decimal VratiBeneficiraniTriMjeseca()
        {
            return Math.Round(PetOsamdesetTri + DvaNulaJedan, 2);
        }

        private static decimal Racunaj(Doprinos doprinos)
        {
            return doprinos.RacunajDoprinos();
        }
    }
}