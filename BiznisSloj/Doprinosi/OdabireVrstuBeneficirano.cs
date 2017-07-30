using System;

namespace BiznisSloj.Doprinosi
{
    public class OdabireVrstuBeneficirano
    {
        private readonly decimal _bruto;
        private readonly int _vrsta;

        public OdabireVrstuBeneficirano(decimal bruto, int vrsta)
        {
            _bruto = bruto;
            _vrsta = vrsta;
        }

        public decimal Beneficirani1 { get; private set; }
        public decimal Beneficirani2 { get; private set; }

        public decimal VratiBeneficirani()
        {
            switch (_vrsta)
            {
                case 0:
                    var bene1 = new BeneficiraniTriSedesetJedan(_bruto);
                    Beneficirani1 = Math.Round(bene1.RacunajDoprinos(), 2);
                    var bene2 = new BeneficiraniJedanDvadesetPet(_bruto);
                    Beneficirani2 = Math.Round(bene2.RacunajDoprinos(), 2);
                    break;
                case 1:
                    var bene3 = new BeneficiraniPetOsamdesetTri(_bruto);
                    Beneficirani1 = Math.Round(bene3.RacunajDoprinos(), 2);
                    var bene4 = new BeneficiraniDvaOsamdesetDevet(_bruto);
                    Beneficirani2 = Math.Round(bene4.RacunajDoprinos(), 2);
                    break;
                case 2:
                    var bene5 = new BeneficiraniOsamTridesetDevet(_bruto);
                    Beneficirani1 = Math.Round(bene5.RacunajDoprinos(), 2);
                    var bene6 = new BeneficiraniDvaOsamdesetDevet(_bruto);
                    Beneficirani2 = Math.Round(bene6.RacunajDoprinos(), 2);

                    break;
            }
            return 0.00m;
        }
    }
}