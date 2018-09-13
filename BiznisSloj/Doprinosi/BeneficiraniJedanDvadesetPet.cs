namespace BiznisSloj.Doprinosi
{
    public class BeneficiraniJedanDvadesetPet : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto)=> bruto * 0.0125m;
    }
}