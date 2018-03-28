namespace BiznisSloj
{
    public class UsporediIVratiBrutoIznos
    {
        private readonly decimal _netoIzTBoxa;
        private readonly ProcesuirajPlacu _placa;

        public UsporediIVratiBrutoIznos(decimal neto, ProcesuirajPlacu placa)
        {
            _netoIzTBoxa = neto;
            _placa = placa;
        }

        public decimal Usporedi()
        {
            var neto = _placa.Neto;
            var bruto = _placa.Bruto;

            if(_netoIzTBoxa < neto)
            {
                bruto += 0.01m;
            }
            if(_netoIzTBoxa > neto)
            {
                bruto -= 0.01m;
            }
            return bruto;
        }
    }
}