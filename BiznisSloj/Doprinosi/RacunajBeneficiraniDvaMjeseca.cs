using System;

namespace BiznisSloj.Doprinosi
{
    public class RacunajBeneficiraniDvaMjeseca
    {
        public RacunajBeneficiraniDvaMjeseca(decimal bruto)
        {
            Bruto = bruto;
        }

        private decimal Bruto { get; }

        private decimal TriSezdesetJedan { get; set; }
        private decimal JedanDvadesetPet { get; set; }

        public void Izracun()
        {
            TriSezdesetJedan = Racunaj(new BeneficiraniTriSedesetJedan());
            JedanDvadesetPet = Racunaj(new BeneficiraniJedanDvadesetPet());
        }

        public decimal VratiBeneDvaMjeseca() => Math.Round(TriSezdesetJedan + JedanDvadesetPet, 2);
 
        private decimal Racunaj(IDoprinosi doprinos) => doprinos.RacunajDoprinos(Bruto);
    }
}