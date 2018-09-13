namespace BiznisSloj.Doprinosi
{
    public class RacunajDoprinoseIzPlace
    {
        public RacunajDoprinoseIzPlace(decimal bruto) => Bruto = bruto;

        private decimal Bruto { get; }

        public decimal PetPosto { get; private set; }
        public decimal PetnaestPosto { get; private set; }
        public decimal DvadesetPosto { get; private set; }

        public void Izracun()
        {
            PetPosto = Racunaj(new DoprinosPetPosto());
            PetnaestPosto = Racunaj(new DoprinosPetnaestPosto());
            DvadesetPosto = Racunaj(new DvadestPostotniDoprinos());
        }

        public decimal VratiDoprinose() => PetPosto + PetnaestPosto;

        private decimal Racunaj(IDoprinosi doprinosi) => doprinosi.RacunajDoprinos(Bruto);
    }
}