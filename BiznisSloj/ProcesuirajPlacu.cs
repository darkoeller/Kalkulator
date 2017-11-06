using System;
using System.Threading.Tasks;
using BiznisSloj.Doprinosi;
using BiznisSloj.Olaksice;
using BiznisSloj.Porezi;

namespace BiznisSloj
{
  public class ProcesuirajPlacu
  {
    private const decimal Minimaldop = 2940.82m;
    private const decimal Maxdoprinos1Stup = 6965.10m;
    private const decimal Maxdoprinos2Stup = 2321.70m;

    public ProcesuirajPlacu(decimal bruto, decimal prirez, bool drugistup, bool chdoprinosi, decimal odbitak = 1.0m)
    {
      Bruto = bruto;
      Odbitak = odbitak;
      Prirez = prirez;
      CheckDoprinosi = chdoprinosi;
      DrugiStup = drugistup;
    }

    public decimal Bruto { get; }
    private decimal Odbitak { get; }
    public decimal Prirez { get; private set; }
    private bool DrugiStup { get; }
    public decimal DoprinosNaPlacUkupno { get; private set; }
    public decimal DoprinosZaZaposljavanje { get; private set; }
    public decimal DoprinosZaZdravstveno { get; private set; }
    public decimal DoprinosiIzPlaceUkupno { get; private set; }
    public decimal PetPostoDoprinos { get; private set; }
    public decimal PetnaestPostoDoprinos { get; private set; }
    public decimal DoprinosZaZnr { get; private set; }
    public decimal Olaksica { get; private set; }
    public decimal UkupniPorez { get; private set; }
    public decimal PorezDvadesetCetiriPosto { get; private set; }
    public decimal PorezTridesetSestPosto { get; private set; }
    public decimal Dohodak { get; private set; }
    public decimal PoreznaOsnovica { get; private set; }
    public decimal Neto { get; private set; }
    public decimal UkupniTrosakPlace { get; private set; }
    public decimal DvadesetPostoDoprinos { get; private set; }

    private bool CheckDoprinosi { get; }

    private void VratiUkupniPorez()
    {
      var ukupniporez = new IzracunajPoreze(PoreznaOsnovica);
      ukupniporez.RacunajPoreze();
      PorezDvadesetCetiriPosto = Math.Round(ukupniporez.Porez24Posto, 2);
      PorezTridesetSestPosto = Math.Round(ukupniporez.Porez36Posto, 2);
      UkupniPorez = Math.Round(ukupniporez.UkupniPorez(), 2);
    }

    private void VratiDoprinoseNaPlacu()
    {
      var doprinosinaplacu = new RacunajDoprinoseNaPlacu(Bruto);
      doprinosinaplacu.Izracun();
      DoprinosZaZaposljavanje = Math.Round(doprinosinaplacu.DoprinosZaposljavanje, 2);
      DoprinosZaZdravstveno = Math.Round(doprinosinaplacu.DoprinosZdravstveno, 2);
      DoprinosZaZnr = Math.Round(doprinosinaplacu.DoprinosZastitaNaRadu, 2);
      DoprinosNaPlacUkupno = Math.Round(doprinosinaplacu.VratiDoprinoseNaPlacu(), 2);
    }

    private void VratiDoprinoseIzPlace()
    {
      if (Bruto < Minimaldop) return;
      var doprinosizplace = new RacunajDoprinoseIzPlace(Bruto);
      doprinosizplace.Izracun();

      if (doprinosizplace.VratiDoprinose() <= 9286.80m)
      {
        PetPostoDoprinos = Math.Round(doprinosizplace.PetPosto, 2);
        PetnaestPostoDoprinos = Math.Round(doprinosizplace.PetnaestPosto, 2);
        DoprinosiIzPlaceUkupno = PetPostoDoprinos + PetnaestPostoDoprinos;
        Dohodak = Math.Round(Bruto - DoprinosiIzPlaceUkupno, 2);
      }
      else if (CheckDoprinosi && doprinosizplace.VratiDoprinose() > 9286.80m)
      {
        PetnaestPostoDoprinos = Maxdoprinos1Stup;
        PetPostoDoprinos = Maxdoprinos2Stup;
        DoprinosiIzPlaceUkupno = PetPostoDoprinos + PetnaestPostoDoprinos;
        Dohodak = Math.Round(Bruto - DoprinosiIzPlaceUkupno, 2);
      }
      else if (CheckDoprinosi != true && doprinosizplace.VratiDoprinose() > 9286.80m)
      {
        PetPostoDoprinos = Math.Round(doprinosizplace.PetPosto, 2);
        PetnaestPostoDoprinos = Math.Round(doprinosizplace.PetnaestPosto, 2);
        DoprinosiIzPlaceUkupno = PetPostoDoprinos + PetnaestPostoDoprinos;
        Dohodak = Math.Round(Bruto - DoprinosiIzPlaceUkupno, 2);
      }
      ProvjeriDrugiStup();
    }

    private void ProvjeriDrugiStup()
    {
      if (DrugiStup) return;
      PetPostoDoprinos = 0.00m;
      PetnaestPostoDoprinos = 0.00m;
    }

    private void VratiOlaksicu()
    {
      var olaksica = new IzracunOlaksice(Odbitak);
      Olaksica = Math.Round(olaksica.VratiOlaksicu(), 2);
      if (Dohodak - Olaksica < 0.0m)
      {
        PoreznaOsnovica = 0.0m;
        return;
      }
      PoreznaOsnovica = Math.Round(Dohodak - Olaksica, 2);
    }

    private void VratiPrirez()
    {
      Prirez = Math.Round(Prirez * UkupniPorez / 100, 2);
    }


    public void Izracun()
    {
      Task.Factory.StartNew(VratiDoprinoseNaPlacu);
      VratiDoprinoseIzPlace();
      VratiOlaksicu();
      VratiUkupniPorez();
      VratiPrirez();
      UkupniTrosakPlace = Math.Round(Bruto + DoprinosNaPlacUkupno, 2);
      UkupniPorez += Prirez;
      Neto = Math.Round(Dohodak - UkupniPorez, 2);
    }
  }
}