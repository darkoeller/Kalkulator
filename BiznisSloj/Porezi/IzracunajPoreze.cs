namespace BiznisSloj.Porezi
{
    public class IzracunajPoreze
    {
        public IzracunajPoreze(decimal bruto) => Bruto = bruto;

        private decimal Bruto { get; set; }

        public decimal Porez24Posto { get; private set; }

        public decimal Porez36Posto { get; private set; }

        //ako je bruto veći od 17500 
        public void RacunajPoreze()
        {
            if (Bruto <= 30000.0m)
            {
                OsnovicaPorez24(Bruto);
            }
            else
            {
                Bruto -= 30000;
                Porez36Posto = Izracunaj(new Porez36());
                Porez24Posto = 7200.0m;
            }
        }

        private void OsnovicaPorez24(decimal bruto)
        {
            if (bruto < 30000.01m) Porez24Posto = Izracunaj(new Porez24());
        }

        public decimal UkupniPorez()=> Porez24Posto + Porez36Posto;
         
        private decimal Izracunaj(IPorezi porez) => porez.Izracunaj(Bruto);
    }
}