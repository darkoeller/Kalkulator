namespace BiznisSloj.Doprinosi
{
    public class DoprinosPetPosto : Doprinos
    {
        public DoprinosPetPosto(decimal bruto) : base(bruto)
        {
        }

        public override decimal RacunajDoprinos()
        {
            return Bruto * 0.05m;
        }
    }
}