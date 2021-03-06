﻿using System;
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

            if (doprinosizplace.VratiDoprinose() <= 9624.00m)
            {
                PetPostoDoprinos = doprinosizplace.PetPosto;
                PetnaestPostoDoprinos = doprinosizplace.PetnaestPosto;
                DoprinosiIzPlaceUkupno = PetPostoDoprinos + PetnaestPostoDoprinos;
                Dohodak = Math.Round(Bruto - DoprinosiIzPlaceUkupno, 2);
            }
            else if (doprinosizplace.VratiDoprinose() > 9624.00m)
            {
                PetnaestPostoDoprinos = Maxdoprinos1Stup;
                PetPostoDoprinos = Maxdoprinos2Stup;
                DoprinosiIzPlaceUkupno = PetPostoDoprinos + PetnaestPostoDoprinos;
                Dohodak = Math.Round(Bruto - DoprinosiIzPlaceUkupno, 2);
            }
            else if (doprinosizplace.VratiDoprinose() > 9624.00m)
            {
                PetPostoDoprinos = doprinosizplace.PetPosto;
                PetnaestPostoDoprinos = doprinosizplace.PetnaestPosto;
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
            Prirez = Math.Round(Prirez * UkupniPorez / 100, 2);
        }

        public void Izracun()
        {
            RacunajDoprinosePorezePrireze();
            UkupniTrosakPlace = Math.Round(Bruto + DoprinosNaPlacUkupno, 2);
            UkupniPorez += Prirez;
            Neto = Math.Round(Dohodak - UkupniPorez, 2);
        }        
    }
}