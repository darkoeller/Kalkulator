namespace BiznisSloj.Doprinosi
{
    public class ZastitaNaRadu : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto) => bruto * 0.005m;
    }
}