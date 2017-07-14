namespace BiznisSloj.Doprinosi
{
    public class Zaposljavanje : Doprinos
    {
        public Zaposljavanje(decimal bruto) : base(bruto)
        {
        }

        public override decimal RacunajDoprinos()
        {
            return Bruto*0.017m;
        }
    }
}