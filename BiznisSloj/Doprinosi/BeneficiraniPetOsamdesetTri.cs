namespace BiznisSloj.Doprinosi
{
    public class BeneficiraniPetOsamdesetTri : Doprinos
    {
        public BeneficiraniPetOsamdesetTri(decimal bruto) : base(bruto)
        {
        }

        public override decimal RacunajDoprinos()
        {
            return Bruto*0.0583m;
        }
    }
}