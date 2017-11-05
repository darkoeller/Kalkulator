namespace BiznisSloj.Porezi
{
  public class Porez24 : Porezi
  {
    public Porez24(decimal bruto) : base(bruto)
    {
    }

    public override decimal Izracunaj()
    {
      return Bruto * 0.24m;
    }
  }
}