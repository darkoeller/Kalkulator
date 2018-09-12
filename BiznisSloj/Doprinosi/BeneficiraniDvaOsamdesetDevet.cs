namespace BiznisSloj.Doprinosi
{
    public class BeneficiraniDvaOsamdesetDevet : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto)
        {
            return bruto * 0.0289m;
        }
    }
}