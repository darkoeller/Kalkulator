namespace BiznisSloj.Doprinosi
{
    public class DoprinosPetnaestPosto : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto)
        {
            return bruto * 0.15m;
        }
    }
}