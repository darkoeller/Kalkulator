using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BiznisSloj.Doprinosi;
using BiznisSloj.Olaksice;
using BiznisSloj.Porezi;

namespace BiznisSloj.Procesi
{
    public class ProcesuirajPlacu
    {
        private static readonly decimal Minimaldop = 3210.24m;
        private static readonly decimal Maxdoprinos1Stup = 7603.20m;
        private static readonly decimal Maxdoprinos2Stup = 2534.40m;
        private static readonly decimal Maxdoprinos1I2Stup = 10137.60m;

        public ProcesuirajPlacu(decimal bruto, decimal prirez, bool drugistup, decimal odbitak = 1.0m)
        {
            Bruto = bruto;
            Odbitak = odbitak;
            Prirez = prirez;
            DrugiStup = drugistup;
        }

        public decimal Bruto { get; }
        private decimal Odbitak { get; }
        public decimal Prirez { get; private set; }
        private bool DrugiStup { get; }
        public decimal DoprinosNaPlacUkupno { get; private set; }
        public decimal DoprinosZaZdravstveno { get; private set; }
        public decimal DoprinosiIzPlaceUkupno { get; private set; }
        public decimal PetPostoDoprinos { get; private set; }
        public decimal PetnaestPostoDoprinos { get; private set; }
        public decimal DvadesetPostoDoprinos { get; private set; }
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

        private void VratiDoprinoseNaPlacu()
        {
            var doprinosinaplacu = new RacunajDoprinoseNaPlacu(Bruto);
            doprinosinaplacu.Izracun();
            DoprinosZaZdravstveno = doprinosinaplacu.DoprinosZdravstveno;
            DoprinosNaPlacUkupno = doprinosinaplacu.VratiDoprinoseNaPlacu();
        }

        private void VratiDoprinoseIzPlace()
        {
            if (Bruto < Minimaldop) return;
            var doprinosizplace = new RacunajDoprinoseIzPlace(Bruto);
            doprinosizplace.Izracun();
            var doprinosi = doprinosizplace.VratiDoprinose();

            if (doprinosi <= Maxdoprinos1I2Stup)
            {
                PopuniDoprinose(doprinosizplace);
            }
            else if (doprinosi > Maxdoprinos1I2Stup)
            {
                PetnaestPostoDoprinos = Maxdoprinos1Stup;
                PetPostoDoprinos = Maxdoprinos2Stup;
                DvadesetPostoDoprinos = Maxdoprinos1I2Stup;
                DoprinosiIzPlaceUkupno = PetPostoDoprinos + PetnaestPostoDoprinos;
                Dohodak = Bruto - DoprinosiIzPlaceUkupno;
            }
            else if (doprinosi > Maxdoprinos1I2Stup)
            {
                PopuniDoprinose(doprinosizplace);
            }
            ProvjeriDrugiStup();
        }

        private void PopuniDoprinose(RacunajDoprinoseIzPlace doprinosizplace)
        {
            PetPostoDoprinos = doprinosizplace.PetPosto;
            PetnaestPostoDoprinos = doprinosizplace.PetnaestPosto;
            DvadesetPostoDoprinos = doprinosizplace.DvadesetPosto;
            DoprinosiIzPlaceUkupno = PetPostoDoprinos + PetnaestPostoDoprinos;
            Dohodak = Bruto - DoprinosiIzPlaceUkupno;
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
            var t = Task.Factory.StartNew(() => VratiDoprinoseNaPlacu());
            var t2 = Task.Factory.StartNew(() => VratiDoprinoseIzPlace())
            .ContinueWith((a) => VratiOlaksicu())
            .ContinueWith((a) => VratiUkupniPorez())
            .ContinueWith((a) => VratiPrirez());
            Task[] zadaci = { t, t2 };
            Task.WaitAll(zadaci);
        }

        private void VratiPrirez() => Prirez = Prirez * UkupniPorez / 100;

        public void Izracun()
        {
            RacunajDoprinosePorezePrireze();
            UkupniTrosakPlace = Bruto + DoprinosNaPlacUkupno;
            UkupniPorez += Prirez;
            Neto = Dohodak - UkupniPorez;
        }
    }
}