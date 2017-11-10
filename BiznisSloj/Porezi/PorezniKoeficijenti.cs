using System.Collections.Generic;
using System.Linq;

namespace BiznisSloj.Porezi
{
  public class PorezniKoeficijenti
  {
    public PorezniKoeficijenti(decimal prirez)
    {
      Stopa = prirez;
      OdrediKoeficijente(Stopa);
    }

    private PorezniKoeficijenti()
    {
    }

    private decimal Stopa { get; set; }
    public decimal KoefPrireza { get; private set; }
    public decimal KoefPorezaPrireza24 { get; private set; }
    public decimal KoefPorezaPrireza36 { get; private set; }

    private void OdrediKoeficijente(decimal stopa)
    {
      var rezultat = PoreznoPrirezniKoef();

      var porezniKoeficijentis = rezultat as IList<PorezniKoeficijenti> ?? rezultat.ToList();
      KoefPrireza = porezniKoeficijentis.Where(r => r.Stopa == stopa).Select(r => r.KoefPrireza).FirstOrDefault();
      KoefPorezaPrireza24 = porezniKoeficijentis.Where(r => r.Stopa == stopa)
        .Select(r => r.KoefPorezaPrireza24)
        .FirstOrDefault();
      KoefPorezaPrireza36 = porezniKoeficijentis.Where(r => r.Stopa == stopa)
        .Select(r => r.KoefPorezaPrireza36)
        .FirstOrDefault();
    }

    private static IEnumerable<PorezniKoeficijenti> PoreznoPrirezniKoef()
    {
      var porezniKoef = new List<PorezniKoeficijenti>
      {
        new PorezniKoeficijenti
        {
          Stopa = 0m
          , KoefPrireza = 1m
          , KoefPorezaPrireza24 = 1.315789m
          , KoefPorezaPrireza36 = 1.562500m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 1m
          , KoefPrireza = 1.01m
          , KoefPorezaPrireza24 = 1.319958m
          , KoefPorezaPrireza36 = 1.571339m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 2m
          , KoefPrireza = 1.02m
          , KoefPorezaPrireza24 = 1.324152m
          , KoefPorezaPrireza36 = 1.580278m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 3m
          , KoefPrireza = 1.03m
          , KoefPorezaPrireza24 = 1.328374m
          , KoefPorezaPrireza36 = 1.589320m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 4m
          , KoefPrireza = 1.04m
          , KoefPorezaPrireza24 = 1.332623m
          , KoefPorezaPrireza36 = 1.599040m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 5m
          , KoefPrireza = 1.05m
          , KoefPorezaPrireza24 = 1.336898m
          , KoefPorezaPrireza36 = 1.607717m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 6m
          , KoefPrireza = 1.06m
          , KoefPorezaPrireza24 = 1.341202m
          , KoefPorezaPrireza36 = 1.617076m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 6.25m
          , KoefPrireza = 1.0625m
          , KoefPorezaPrireza24 = 1.342282m
          , KoefPorezaPrireza36 = 1.619433m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 6.5m
          , KoefPrireza = 1.065m
          , KoefPorezaPrireza24 = 1.343364m
          , KoefPorezaPrireza36 = 1.621797m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 7m
          , KoefPrireza = 1.07m
          , KoefPorezaPrireza24 = 1.345533m
          , KoefPorezaPrireza36 = 1.626545m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 7.5m
          , KoefPrireza = 1.075m
          , KoefPorezaPrireza24 = 1.347709m
          , KoefPorezaPrireza36 = 1.631321m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 8m
          , KoefPrireza = 1.08m
          , KoefPorezaPrireza24 = 1.349989m
          , KoefPorezaPrireza36 = 1.636126m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 9m
          , KoefPrireza = 1.09m
          , KoefPorezaPrireza24 = 1.354279m
          , KoefPorezaPrireza36 = 1.645820m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 10m
          , KoefPrireza = 1.1m
          , KoefPorezaPrireza24 = 1.358696m
          , KoefPorezaPrireza36 = 1.655629m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 12m
          , KoefPrireza = 1.12m
          , KoefPorezaPrireza24 = 1.367515m
          , KoefPorezaPrireza36 = 1.675603m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 13m
          , KoefPrireza = 1.13m
          , KoefPorezaPrireza24 = 1.372118m
          , KoefPorezaPrireza36 = 1.685772m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 14m
          , KoefPrireza = 1.14m
          , KoefPorezaPrireza24 = 1.376652m
          , KoefPorezaPrireza36 = 1.696065m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 15m
          , KoefPrireza = 1.15m
          , KoefPorezaPrireza24 = 1.381215m
          , KoefPorezaPrireza36 = 1.706485m
        }
        , new PorezniKoeficijenti
        {
          Stopa = 18m
          , KoefPrireza = 1.18m
          , KoefPorezaPrireza24 = 1.395089m
          , KoefPorezaPrireza36 = 1.738526m
        }
      };
      return porezniKoef;
    }
  }
}