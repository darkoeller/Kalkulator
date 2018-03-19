using System;

namespace BiznisSloj.Doprinosi
{
    public class RacunajBeneficiraniDvaMjeseca
    {
        private  decimal Bruto { get; }

        public RacunajBeneficiraniDvaMjeseca(decimal bruto)
        {
            Bruto = bruto;
        }

        private decimal TriSezdesetJedan { get; set; }
        private decimal JedanDvadesetPet { get; set; }

        public void Izracun()
        {
            TriSezdesetJedan = Racunaj(new BeneficiraniTriSedesetJedan());
            JedanDvadesetPet = Racunaj(new BeneficiraniJedanDvadesetPet());
        }

        public decimal VratiBeneDvaMjeseca()
        {
            return Math.Round(TriSezdesetJedan + JedanDvadesetPet, 2);
        }

        private decimal Racunaj(IDoprinosi doprinos)
        {
            return doprinos.RacunajDoprinos(Bruto);
        }
    }
}