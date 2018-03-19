namespace BiznisSloj.Doprinosi
{
    public class BeneficiraniJedanDvadesetPet : IDoprinosi
    {
        public  decimal RacunajDoprinos(decimal bruto)
        {
            return bruto * 0.0125m;
        }
    }
}