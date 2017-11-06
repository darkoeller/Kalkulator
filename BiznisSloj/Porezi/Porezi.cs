namespace BiznisSloj.Porezi
{
  public abstract class Porezi
  {
    protected readonly decimal Bruto;

    protected Porezi(decimal bruto)
    {
      Bruto = bruto;
    }

    public abstract decimal Izracunaj();
  }
}