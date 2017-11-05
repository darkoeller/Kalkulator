namespace BiznisSloj.Doprinosi
{
  public abstract class Doprinos
  {
    protected readonly decimal Bruto;

    protected Doprinos(decimal bruto)
    {
      Bruto = bruto;
    }

    public abstract decimal RacunajDoprinos();
  }
}