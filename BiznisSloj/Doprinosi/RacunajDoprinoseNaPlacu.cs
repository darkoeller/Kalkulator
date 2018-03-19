namespace BiznisSloj.Doprinosi
{
    public class RacunajDoprinoseNaPlacu
    {
        private decimal Bruto { get; }

        public RacunajDoprinoseNaPlacu(decimal bruto)
        {
            Bruto = bruto;
        }

        public decimal DoprinosZdravstveno { get; private set; }
        public decimal DoprinosZaposljavanje { get; private set; }
        public decimal DoprinosZastitaNaRadu { get; private set; }

        public void Izracun()
        {
            DoprinosZdravstveno = Racunaj(new Zdravstveno());
            DoprinosZaposljavanje = Racunaj(new Zaposljavanje());
            DoprinosZastitaNaRadu = Racunaj(new ZastitaNaRadu());
        }

        public decimal VratiDoprinoseNaPlacu()
        {
            return DoprinosZaposljavanje + DoprinosZastitaNaRadu + DoprinosZdravstveno;
        }

        public decimal UkupanTrosakPlace()
        {
            return Bruto + DoprinosZaposljavanje + DoprinosZastitaNaRadu + DoprinosZdravstveno;
        }

        private decimal Racunaj(IDoprinosi doprinosi)
        {
            return doprinosi.RacunajDoprinos(Bruto);
        }
    }
}