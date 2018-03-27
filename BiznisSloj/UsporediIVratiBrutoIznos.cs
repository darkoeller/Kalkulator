namespace BiznisSloj
{
    public class UsporediIVratiBrutoIznos
    {
        private readonly decimal _netoIzTBoxa;
        private ProcesuirajPlacu Placa { get; set; }

        public UsporediIVratiBrutoIznos(decimal neto, ProcesuirajPlacu placa)
        {
            _netoIzTBoxa = neto;
            Placa = placa;
        }

        public ProcesuirajPlacu Usporedi()
        {
            while (_netoIzTBoxa < Placa.Neto)
            {
                Placa.Bruto += 0.01m;
                ProcesuirajPlacu(Placa);
            }
  


            //var izracunatiNeto = Placa.Neto;
            //var izracunatiBruto = Placa.Bruto;
            //if (izracunatiNeto < _neto)
            //{
            //    izracunatiBruto += 0.01m;
            //   return  Placa = PonovoProcesuirajBruto(izracunatiBruto);
            //}

            if (izracunatiNeto > _neto)
            {
                izracunatiBruto -= 0.01m;
                return Placa = PonovoProcesuirajBruto(izracunatiBruto);
            }

            return Placa;
        }

        private ProcesuirajPlacu PonovoProcesuirajBruto(decimal upisanineto)
        {
            var placa = new ProcesuirajPlacu(upisanineto);
            placa.Izracun();
            return placa;
        }
    }
}