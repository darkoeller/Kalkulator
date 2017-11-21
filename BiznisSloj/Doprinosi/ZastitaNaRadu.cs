namespace BiznisSloj.Doprinosi
{
    public class ZastitaNaRadu : Doprinos
    {
        public ZastitaNaRadu(decimal bruto) : base(bruto)
        {
        }

        public override decimal RacunajDoprinos()
        {
            return Bruto * 0.005m;
        }
    }
}