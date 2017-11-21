namespace BiznisSloj.Porezi
{
    public class Porez36 : Porezi
    {
        public Porez36(decimal bruto) : base(bruto)
        {
        }

        public override decimal Izracunaj()
        {
            return Bruto * 0.36m;
        }
    }
}