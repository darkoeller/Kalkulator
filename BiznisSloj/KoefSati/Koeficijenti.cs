using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BiznisSloj.KoefSati
{
  public class Koeficijenti
  {
    public byte Id { get; set; }
    public string Sifra { get; set; }
    public string Naziv { get; set; }
    public decimal Koeficijent { get; set; }

    public static decimal VratiIznos(string naziv)
    {
      var rezultat = VratiSifre().Where(r => string.Equals(r.Naziv, naziv)).Select(r => r.Koeficijent).First();
      return rezultat;
    }

    public static IEnumerable<Koeficijenti> VratiSifre()
    {
      var sifreRada = new ObservableCollection<Koeficijenti>
      {
        new Koeficijenti {Id = 1, Sifra = "01", Naziv = @"Redovni rad 1.smjena", Koeficijent = 1.00m}
        , new Koeficijenti {Id = 2, Sifra = "02", Naziv = @"Redovni rad 2.smjena", Koeficijent = 1.15m}
        , new Koeficijenti {Id = 3, Sifra = "03", Naziv = @"Redovni rad noću", Koeficijent = 1.50m}
        , new Koeficijenti {Id = 4, Sifra = "04", Naziv = @"Rad nedjeljom 1.smjena", Koeficijent = 1.35m}
        , new Koeficijenti {Id = 5, Sifra = "04", Naziv = @"Rad nedjeljom 2.smjena", Koeficijent = 1.50m}
        , new Koeficijenti {Id = 6, Sifra = "04", Naziv = @"Rad nedjeljom noću", Koeficijent = 1.85m}
        , new Koeficijenti {Id = 7, Sifra = "05", Naziv = @"Prekovremeni rad 1.smjena", Koeficijent = 1.50m}
        , new Koeficijenti {Id = 8, Sifra = "05", Naziv = @"Prekovremeni rad 2.smjena", Koeficijent = 1.65m}
        , new Koeficijenti {Id = 9, Sifra = "05", Naziv = @"Prekovremeni rad noću", Koeficijent = 2.00m}
        , new Koeficijenti {Id = 10, Sifra = "05", Naziv = @"Prekovremeni rad nedjeljom 1.smjena", Koeficijent = 1.85m}
        , new Koeficijenti {Id = 11, Sifra = "05", Naziv = @"Prekovremeni rad nedjeljom 2.smjena", Koeficijent = 2.00m}
        , new Koeficijenti {Id = 12, Sifra = "05", Naziv = @"Prekovremeni rad nedjeljom noću", Koeficijent = 2.35m}
        , new Koeficijenti {Id = 13, Sifra = "05", Naziv = @"Prekovremeni rad blagdan 1.smjena", Koeficijent = 2.00m}
        , new Koeficijenti {Id = 14, Sifra = "05", Naziv = @"Prekovremeni rad blagdan 2.smjena", Koeficijent = 2.15m}
        , new Koeficijenti {Id = 15, Sifra = "05", Naziv = @"Prekovremeni rad blagdan noću", Koeficijent = 2.50m}
        , new Koeficijenti
        {
          Id = 16
          , Sifra = "05"
          , Naziv = @"Prekovremeni rad blagdan ned. 1.smjena"
          , Koeficijent = 2.35m
        }
        , new Koeficijenti
        {
          Id = 17
          , Sifra = "05"
          , Naziv = @"Prekovremeni rad blagdan ned. 2.smjena"
          , Koeficijent = 2.50m
        }
        , new Koeficijenti {Id = 18, Sifra = "05", Naziv = @"Prekovremeni rad blagdan ned. noću", Koeficijent = 2.85m}
        , new Koeficijenti {Id = 19, Sifra = "06", Naziv = @"Rad na blagdan 1.smjena", Koeficijent = 1.50m}
        , new Koeficijenti {Id = 20, Sifra = "06", Naziv = @"Rad na blagdan 2.smjena", Koeficijent = 1.65m}
        , new Koeficijenti {Id = 21, Sifra = "06", Naziv = @"Rad na blagdan noću", Koeficijent = 2.00m}
        , new Koeficijenti {Id = 22, Sifra = "07", Naziv = @"Rad pod uvjetima", Koeficijent = 1.00m}
        , new Koeficijenti {Id = 23, Sifra = "09", Naziv = @"Naknada za G.O. 1.smjena", Koeficijent = 1.00m}
        , new Koeficijenti {Id = 24, Sifra = "09", Naziv = @"Naknada za G.O. 2.smjena", Koeficijent = 1.15m}
        , new Koeficijenti {Id = 25, Sifra = "09", Naziv = @"Naknada za G.O. noću", Koeficijent = 1.50m}
        , new Koeficijenti {Id = 26, Sifra = "09", Naziv = @"Naknada za G.O. nedjeljom 1.smjena", Koeficijent = 1.35m}
        , new Koeficijenti {Id = 27, Sifra = "09", Naziv = @"Naknada za G.O. nedjeljom 2.smjena", Koeficijent = 1.50m}
        , new Koeficijenti {Id = 28, Sifra = "09", Naziv = @"Naknada za G.O. nedjeljom noću", Koeficijent = 1.85m}
        , new Koeficijenti {Id = 29, Sifra = "09", Naziv = @"Naknada za G.O. blagdan 1.smjena", Koeficijent = 1.50m}
        , new Koeficijenti {Id = 30, Sifra = "09", Naziv = @"Naknada za G.O. blagdan 2.smjena", Koeficijent = 1.65m}
        , new Koeficijenti {Id = 31, Sifra = "09", Naziv = @"Naknada za G.O. blagdan noću", Koeficijent = 2.00m}
        , new Koeficijenti
        {
          Id = 32
          , Sifra = "09"
          , Naziv = @"Naknada za G.O. blagdan ned. 1.smjena"
          , Koeficijent = 1.80m
        }
        , new Koeficijenti
        {
          Id = 33
          , Sifra = "09"
          , Naziv = @"Naknada za G.O. blagdan ned. 2.smjena"
          , Koeficijent = 2.00m
        }
        , new Koeficijenti {Id = 34, Sifra = "09", Naziv = @"Naknada za G.O. blagdan ned. noću", Koeficijent = 2.35m}
        , new Koeficijenti {Id = 35, Sifra = "10", Naziv = @"Naknada za  blagdan ", Koeficijent = 1.00m}
        , new Koeficijenti {Id = 36, Sifra = "11", Naziv = @"Naknada za pl.dopust 1.smjena", Koeficijent = 1.00m}
        , new Koeficijenti {Id = 37, Sifra = "11", Naziv = @"Naknada za pl.dopust 2.smjena", Koeficijent = 1.15m}
        , new Koeficijenti {Id = 38, Sifra = "11", Naziv = @"Naknada za pl.dopust noću", Koeficijent = 1.50m}
        , new Koeficijenti
        {
          Id = 39
          , Sifra = "11"
          , Naziv = @"Naknada za pl.dopust nedjeljom 1.smjena"
          , Koeficijent = 1.35m
        }
        , new Koeficijenti
        {
          Id = 40
          , Sifra = "11"
          , Naziv = @"Naknada za pl.dopust nedjeljom 2.smjena"
          , Koeficijent = 1.50m
        }
        , new Koeficijenti {Id = 41, Sifra = "11", Naziv = @"Naknada za pl.dopust nedjeljom noću", Koeficijent = 1.85m}
        , new Koeficijenti
        {
          Id = 42
          , Sifra = "11"
          , Naziv = @"Naknada za pl.dopust blagdan 1.smjena"
          , Koeficijent = 1.50m
        }
        , new Koeficijenti
        {
          Id = 43
          , Sifra = "11"
          , Naziv = @"Naknada za pl.dopust blagdan 2.smjena"
          , Koeficijent = 1.65m
        }
        , new Koeficijenti {Id = 44, Sifra = "11", Naziv = @"Naknada za pl.dopust blagdan noću", Koeficijent = 2.00m}
        , new Koeficijenti {Id = 45, Sifra = "32", Naziv = @"Rad na blagdan nedjeljom 1.smjena", Koeficijent = 1.80m}
        , new Koeficijenti {Id = 46, Sifra = "32", Naziv = @"Rad na blagdan nedjeljom 2.smjena", Koeficijent = 2.00m}
        , new Koeficijenti {Id = 47, Sifra = "32", Naziv = @"Rad na blagdan nedjeljom noću", Koeficijent = 2.35m}
      };
      return sifreRada;
    }
  }
}