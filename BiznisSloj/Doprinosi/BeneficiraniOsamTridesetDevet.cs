namespace BiznisSloj.Doprinosi
{
    public class BeneficiraniOsamTridesetDevet : IDoprinosi
    {
        public  decimal RacunajDoprinos(decimal bruto)
        {
            return bruto * 0.0839m;
        }
    }
}