namespace BiznisSloj.Doprinosi
{
    public class BeneficiraniJedanDvadesetPet : Doprinos
    {
        public BeneficiraniJedanDvadesetPet(decimal bruto) : base(bruto)
        {
        }

        public override decimal RacunajDoprinos()
        {
            return Bruto * 0.0125m;
        }
    }
}