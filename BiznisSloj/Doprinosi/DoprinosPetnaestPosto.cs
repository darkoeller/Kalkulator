namespace BiznisSloj.Doprinosi
{
  public class DoprinosPetnaestPosto : Doprinos
  {
    public DoprinosPetnaestPosto(decimal bruto) : base(bruto)
    {
    }

    public override decimal RacunajDoprinos()
    {
      return Bruto * 0.15m;
    }
  }
}