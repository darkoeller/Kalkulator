namespace BiznisSloj.Doprinosi
{
    public class Zaposljavanje : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto) => bruto * 0.017m;
    }
}