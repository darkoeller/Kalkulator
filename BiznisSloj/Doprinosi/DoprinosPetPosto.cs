namespace BiznisSloj.Doprinosi
{
    public class DoprinosPetPosto : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto) => bruto * 0.05m;
    }
}