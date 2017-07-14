namespace BiznisSloj.Doprinosi
{
    public class RacunajDoprinoseNaPlacu
    {
        private readonly decimal _bruto;

        public RacunajDoprinoseNaPlacu(decimal bruto)
        {
            _bruto = bruto;
        }

        public decimal DoprinosZdravstveno { get; set; }
        public decimal DoprinosZaposljavanje { get; set; }
        public decimal DoprinosZastitaNaRadu { get; set; }

        public void Izracun()
        {
            DoprinosZdravstveno = Racunaj(new Zdravstveno(_bruto));
            DoprinosZaposljavanje = Racunaj(new Zaposljavanje(_bruto));
            DoprinosZastitaNaRadu = Racunaj(new ZastitaNaRadu(_bruto));
        }

        public decimal VratiDoprinoseNaPlacu()
        {
            return DoprinosZaposljavanje + DoprinosZastitaNaRadu + DoprinosZdravstveno;
        }

        public decimal UkupanTrosakPlace()
        {
            return _bruto + DoprinosZaposljavanje + DoprinosZastitaNaRadu + DoprinosZdravstveno;
        }

        private decimal Racunaj(Doprinos doprinosi)
        {
            return doprinosi.RacunajDoprinos();
        }
    }
}