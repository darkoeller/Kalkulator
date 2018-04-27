using System;
using BiznisSloj.Doprinosi;
using BiznisSloj.Olaksice;
using BiznisSloj.Porezi;
using PostSharp.Patterns.Threading;

namespace BiznisSloj.Procesi
{
    public class ProcesuirajPlacu
    {
        private static readonly decimal Minimaldop = 3047.60m;
        private static readonly decimal Maxdoprinos1Stup = 7218.00m;
        private static readonly decimal Maxdoprinos2Stup = 2406.00m;
        private static readonly decimal Maxdoprinos1I2Stup = 9624.00m;

        public ProcesuirajPlacu(decimal bruto, decimal prirez, bool drugistup, decimal odbitak = 1.0m)
        {
            Bruto = bruto;
            Odbitak = odbitak;
            Prirez = prirez;
            DrugiStup = drugistup;
        }

        public decimal Bruto { get; set; }
        private decimal Odbitak { get; }
        public decimal Prirez { get; private set; }
        private bool DrugiStup { get; }
        public decimal DoprinosNaPlacUkupno { get; private set; }
        public decimal DoprinosZaZaposljavanje { get; private set; }
        public decimal DoprinosZaZdravstveno { get; private set; }
        public decimal DoprinosiIzPlaceUkupno { get; private set; }
        public decimal PetPostoDoprinos { get; private set; }
        public decimal PetnaestPostoDoprinos { get; private set; }
        public decimal DvadesetPostoDoprinos { get; private set; }
        public decimal DoprinosZaZnr { get; private set; }
        public decimal Olaksica { get; private set; }
        public decimal UkupniPorez { get; private set; }
        public decimal PorezDvadesetCetiriPosto { get; private set; }
        public decimal PorezTridesetSestPosto { get; private set; }
        public decimal Dohodak { get; private set; }
        public decimal PoreznaOsnovica { get; private set; }
        public decimal Neto { get; private set; }
        public decimal UkupniTrosakPlace { get; private set; }


        private void VratiUkupniPorez()
        {
            var ukupniporez = new IzracunajPoreze(PoreznaOsnovica);
            ukupniporez.RacunajPoreze();
            PorezDvadesetCetiriPosto = ukupniporez.Porez24Posto;
            PorezTridesetSestPosto = ukupniporez.Porez36Posto;
            UkupniPorez = ukupniporez.UkupniPorez();
        }
        [Background]
        private void VratiDoprinoseNaPlacu()
        {
            var doprinosinaplacu = new RacunajDoprinoseNaPlacu(Bruto);
            doprinosinaplacu.Izracun();
            DoprinosZaZaposljavanje = doprinosinaplacu.DoprinosZaposljavanje;
            DoprinosZaZdravstveno = doprinosinaplacu.DoprinosZdravstveno;
            DoprinosZaZnr = doprinosinaplacu.DoprinosZastitaNaRadu;
            DoprinosNaPlacUkupno = doprinosinaplacu.VratiDoprinoseNaPlacu();
        }

        private void VratiDoprinoseIzPlace()
        {
            if (Bruto < Minimaldop) return;
            var doprinosizplace = new RacunajDoprinoseIzPlace(Bruto);
                doprinosizplace.Izracun();

            if (doprinosizplace.VratiDoprinose() <= 9624.00m)
            {
                PetPostoDoprinos = doprinosizplace.PetPosto;
                PetnaestPostoDoprinos = doprinosizplace.PetnaestPosto;
                DvadesetPostoDoprinos = doprinosizplace.DvadesetPosto;
                DoprinosiIzPlaceUkupno = PetPostoDoprinos + PetnaestPostoDoprinos;
                Dohodak = Bruto - DoprinosiIzPlaceUkupno;
            }
            else if (doprinosizplace.VratiDoprinose() > 9624.00m)
            {
                PetnaestPostoDoprinos = Maxdoprinos1Stup;
                PetPostoDoprinos = Maxdoprinos2Stup;
                DvadesetPostoDoprinos = Maxdoprinos1I2Stup;
                DoprinosiIzPlaceUkupno = PetPostoDoprinos + PetnaestPostoDoprinos;
                Dohodak = Bruto - DoprinosiIzPlaceUkupno;
            }
            else if (doprinosizplace.VratiDoprinose() > 9624.00m)
            {
                PetPostoDoprinos = doprinosizplace.PetPosto;
                PetnaestPostoDoprinos = doprinosizplace.PetnaestPosto;
                DvadesetPostoDoprinos = doprinosizplace.DvadesetPosto;
                DoprinosiIzPlaceUkupno = PetPostoDoprinos + PetnaestPostoDoprinos;
                Dohodak = Bruto - DoprinosiIzPlaceUkupno;
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
            Olaksica = olaksica.VratiOlaksicu();
            if (Dohodak - Olaksica < 0.0m)
            {
                PoreznaOsnovica = 0.0m;
                return;
            }
            PoreznaOsnovica = Math.Round(Dohodak - Olaksica, 2);
        }

        private void RacunajDoprinosePorezePrireze()
        {
            VratiDoprinoseNaPlacu();
            VratiDoprinoseIzPlace();
            VratiOlaksicu();
            VratiUkupniPorez();
            VratiPrirez();
        }
        [Background]
        private void VratiPrirez()
        {
            Prirez = Prirez * UkupniPorez / 100;
        }

        public void Izracun()
        {
            RacunajDoprinosePorezePrireze();
            UkupniTrosakPlace = Bruto + DoprinosNaPlacUkupno;
            UkupniPorez += Prirez;
            Neto = Dohodak - UkupniPorez;
        }        
    }
}