namespace BiznisSloj.Doprinosi
{
    public class Zdravstveno : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto) => bruto * 0.165m;
    }
}