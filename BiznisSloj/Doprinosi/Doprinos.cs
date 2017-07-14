namespace BiznisSloj.Doprinosi
{
    public abstract class Doprinos
    {
        public decimal Bruto;

        protected Doprinos(decimal bruto)
        {
            Bruto = bruto;
        }

        public abstract decimal RacunajDoprinos();
    }
}