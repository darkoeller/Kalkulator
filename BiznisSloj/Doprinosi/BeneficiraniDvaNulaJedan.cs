namespace BiznisSloj.Doprinosi
{
    public class BeneficiraniDvaNulaJedan : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto)
        {
            return bruto * 0.021m;
        }
    }
}