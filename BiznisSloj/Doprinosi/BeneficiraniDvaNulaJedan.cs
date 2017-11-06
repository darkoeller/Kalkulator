namespace BiznisSloj.Doprinosi
{
  public class BeneficiraniDvaNulaJedan : Doprinos
  {
    public BeneficiraniDvaNulaJedan(decimal bruto) : base(bruto)
    {
    }

    public override decimal RacunajDoprinos()
    {
      return Bruto * 0.021m;
    }
  }
}