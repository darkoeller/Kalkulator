namespace BiznisSloj.Doprinosi
{
    public class Zdravstveno : Doprinos
    {
        public Zdravstveno(decimal bruto) : base(bruto)
        {
        }

        public override decimal RacunajDoprinos()
        {
            return Bruto * 0.15m;
        }
    }
}