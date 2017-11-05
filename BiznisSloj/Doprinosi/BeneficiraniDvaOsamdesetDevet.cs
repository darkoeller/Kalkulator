namespace BiznisSloj.Doprinosi
{
  public class BeneficiraniDvaOsamdesetDevet : Doprinos
  {
    public BeneficiraniDvaOsamdesetDevet(decimal bruto) : base(bruto)
    {
    }

    public override decimal RacunajDoprinos()
    {
      return Bruto * 0.0289m;
    }
  }
}