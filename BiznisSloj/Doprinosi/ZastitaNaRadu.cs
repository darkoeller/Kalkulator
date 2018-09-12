namespace BiznisSloj.Doprinosi
{
    public class ZastitaNaRadu : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto)
        {
            return bruto * 0.005m;
        }
    }
}