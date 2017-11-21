namespace BiznisSloj.Doprinosi
{
    public class BeneficiraniTriSedesetJedan : Doprinos
    {
        public BeneficiraniTriSedesetJedan(decimal bruto) : base(bruto)
        {
        }

        public override decimal RacunajDoprinos()
        {
            return Bruto * 0.0361m;
        }
    }
}