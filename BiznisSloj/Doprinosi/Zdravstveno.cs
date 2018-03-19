namespace BiznisSloj.Doprinosi
{
    public class Zdravstveno : IDoprinosi
    {
        public  decimal RacunajDoprinos(decimal bruto)
        {
            return bruto * 0.15m;
        }
    }
}