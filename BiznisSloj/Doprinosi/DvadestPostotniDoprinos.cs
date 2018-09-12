namespace BiznisSloj.Doprinosi
{
    public class DvadestPostotniDoprinos : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto) => bruto * 0.20m;
    }
}