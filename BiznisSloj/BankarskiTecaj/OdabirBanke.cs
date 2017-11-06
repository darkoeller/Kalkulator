namespace BiznisSloj.BankarskiTecaj
{
  public class OdabirBanke
  {
    private readonly string _odabir;

    public OdabirBanke(string odabir)
    {
      _odabir = odabir;
    }

    public decimal VratiIznos()
    {
      decimal euro;
      switch (_odabir)
      {
        case "HNB":
          var hnb = new TecajHNBa();
          euro = hnb.VratiEuro();
          return euro;
        case "PBZ":
          var pbz = new TecajPBZa();
          euro = pbz.VratiEuro();
          return euro;
      }
      return 0.0m;
    }
  }
}