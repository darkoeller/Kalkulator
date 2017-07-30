using System;

namespace BiznisSloj.Doprinosi
{
    public class RacunajBeneficiraniDvaMjeseca
    {
        private readonly decimal _bruto;

        public RacunajBeneficiraniDvaMjeseca(decimal bruto)
        {
            _bruto = bruto;
        }

        private decimal TriSezdesetJedan { get; set; }
        private decimal JedanDvadesetPet { get; set; }

        public void Izracun()
        {
            TriSezdesetJedan = Racunaj(new BeneficiraniTriSedesetJedan(_bruto));
            JedanDvadesetPet = Racunaj(new BeneficiraniJedanDvadesetPet(_bruto));
        }

        public decimal VratiBeneDvaMjeseca()
        {
            return Math.Round(TriSezdesetJedan + JedanDvadesetPet, 2);
        }

        private static decimal Racunaj(Doprinos doprinos)
        {
            return doprinos.RacunajDoprinos();
        }
    }
}