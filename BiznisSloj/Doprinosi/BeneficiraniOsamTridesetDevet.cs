namespace BiznisSloj.Doprinosi
{
  public class BeneficiraniOsamTridesetDevet : Doprinos
  {
    public BeneficiraniOsamTridesetDevet(decimal bruto) : base(bruto)
    {
    }

    public override decimal RacunajDoprinos()
    {
      return Bruto * 0.0839m;
    }
  }
}