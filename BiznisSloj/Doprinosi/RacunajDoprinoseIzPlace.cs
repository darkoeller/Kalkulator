namespace BiznisSloj.Doprinosi
{
    public class RacunajDoprinoseIzPlace
    {
        private  decimal Bruto { get; }

        public RacunajDoprinoseIzPlace(decimal bruto)
        {
            Bruto = bruto;
        }

        public decimal PetPosto { get; private set; }
        public decimal PetnaestPosto { get; private set; }

        public void Izracun()
        {
            PetPosto = Racunaj(new DoprinosPetPosto());
            PetnaestPosto = Racunaj(new DoprinosPetnaestPosto());
        }

        public decimal VratiDoprinose()
        {
            return PetPosto + PetnaestPosto;
        }

        private decimal Racunaj(IDoprinosi doprinosi)
        {
            return doprinosi.RacunajDoprinos(Bruto);
        }
    }
}