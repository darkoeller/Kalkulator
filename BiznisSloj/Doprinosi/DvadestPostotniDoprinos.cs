namespace BiznisSloj.Doprinosi
{
    public class DvadestPostotniDoprinos : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto)
        {
            return bruto * 0.20m;
        }
    }
}