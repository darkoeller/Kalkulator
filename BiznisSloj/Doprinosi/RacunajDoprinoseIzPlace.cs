using System;

namespace BiznisSloj.Doprinosi
{
  public class RacunajDoprinoseIzPlace
  {
    private readonly decimal _bruto;

    public RacunajDoprinoseIzPlace(decimal bruto)
    {
      _bruto = bruto;
    }

    public decimal PetPosto { get; private set; }
    public decimal PetnaestPosto { get; private set; }

    public void Izracun()
    {
      PetPosto = Racunaj(new DoprinosPetPosto(_bruto));
      PetnaestPosto = Racunaj(new DoprinosPetnaestPosto(_bruto));
    }

    public decimal VratiDoprinose()
    {
      return Math.Round(PetPosto + PetnaestPosto, 2);
    }

    private static decimal Racunaj(Doprinos doprinosi)
    {
      return doprinosi.RacunajDoprinos();
    }
  }
}