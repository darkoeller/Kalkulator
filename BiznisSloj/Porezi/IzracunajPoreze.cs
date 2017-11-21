namespace BiznisSloj.Porezi
{
    public class IzracunajPoreze
    {
        private readonly decimal _bruto;

        public IzracunajPoreze(decimal bruto)
        {
            _bruto = bruto;
        }

        public decimal Porez24Posto { get; private set; }

        public decimal Porez36Posto { get; private set; }

        //ako je bruto veći od 17500 
        public void RacunajPoreze()
        {
            var bruto = _bruto;
            if (bruto > 17500.0m)
            {
                bruto -= 17500;
                Porez36Posto = Izracunaj(new Porez36(bruto));
                OsnovicaPorez24(17500m);
            }
            else
            {
                OsnovicaPorez24(bruto);
            }
        }

        private void OsnovicaPorez24(decimal bruto)
        {
            if (bruto < 17500.01m) Porez24Posto = Izracunaj(new Porez24(bruto));
        }

        public decimal UkupniPorez()
        {
            return Porez24Posto + Porez36Posto;
        }

        private static decimal Izracunaj(Porezi porez)
        {
            return porez.Izracunaj();
        }
    }
}