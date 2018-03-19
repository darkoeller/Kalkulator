using System;

namespace BiznisSloj.Doprinosi
{
    public class OdabireVrstuBeneficirano
    {
        private  decimal Bruto { get; }
        private readonly int _vrsta;

        public OdabireVrstuBeneficirano(decimal bruto, int vrsta)
        {
             Bruto = bruto;
            _vrsta = vrsta;
        }

        public decimal Beneficirani1 { get; private set; }
        public decimal Beneficirani2 { get; private set; }

        public decimal VratiBeneficirani()
        {
            switch (_vrsta)
            {
                case 0:
                    var bene1 = new BeneficiraniTriSedesetJedan();
                    Beneficirani1 = Math.Round(bene1.RacunajDoprinos(Bruto), 2);
                    var bene2 = new BeneficiraniJedanDvadesetPet();
                    Beneficirani2 = Math.Round(bene2.RacunajDoprinos(Bruto), 2);
                    break;
                case 1:
                    var bene3 = new BeneficiraniPetOsamdesetTri();
                    Beneficirani1 = Math.Round(bene3.RacunajDoprinos(Bruto), 2);
                    var bene4 = new BeneficiraniDvaOsamdesetDevet();
                    Beneficirani2 = Math.Round(bene4.RacunajDoprinos(Bruto), 2);
                    break;
                case 2:
                    var bene5 = new BeneficiraniOsamTridesetDevet();
                    Beneficirani1 = Math.Round(bene5.RacunajDoprinos(Bruto), 2);
                    var bene6 = new BeneficiraniDvaOsamdesetDevet();
                    Beneficirani2 = Math.Round(bene6.RacunajDoprinos(Bruto), 2);

                    break;
            }
            return 0.00m;
        }
    }
}