namespace BiznisSloj
{
    public class UsporediIVratiBrutoIznos
    {
        private bool Stup1I2 { get; }
        private decimal Olaksica { get; }
        private readonly decimal _neto;
        private ProcesuirajPlacu Placa { get; set; }
        private readonly decimal _prirez;

        public UsporediIVratiBrutoIznos(decimal neto, ProcesuirajPlacu placa, decimal prirez, bool stup1I2, decimal olaksica)
        {
            Stup1I2 = stup1I2;
            Olaksica = olaksica;
            _neto = neto;
            Placa = placa;
            _prirez = prirez;
        }

        public ProcesuirajPlacu Usporedi()
        {
            var izracunatiNeto = Placa.Neto;
            var izracunatiBruto = Placa.Bruto;
            if (izracunatiNeto < _neto)
            {
                izracunatiBruto += 0.01m;
               return  Placa = PonovoProcesuirajBruto(izracunatiBruto);
            }

            if (izracunatiNeto < _neto)
            {
                izracunatiBruto -= 0.01m;
                return Placa = PonovoProcesuirajBruto(izracunatiBruto);
            }

            return Placa;
        }

        private ProcesuirajPlacu PonovoProcesuirajBruto(decimal upisanineto)
        {
            var placa = new ProcesuirajPlacu(upisanineto, _prirez, Stup1I2, Olaksica);
            placa.Izracun();
            return placa;
        }
    }
}