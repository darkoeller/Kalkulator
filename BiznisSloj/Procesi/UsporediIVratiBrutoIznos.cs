namespace BiznisSloj.Procesi
{
    public class UsporediIVratiBrutoIznos
    {
        private readonly decimal _netoIzTBoxa;
        private  ProcesuirajPlacu _placa;
        private readonly decimal _prirez;
        private readonly decimal _olaksica;
        private readonly bool _miroStup;

        public UsporediIVratiBrutoIznos(decimal neto, ProcesuirajPlacu placa, decimal  prirez, decimal olaksica, bool miroStup)
        {
            _netoIzTBoxa = neto;
            _placa = placa;
            _prirez = prirez;
            _olaksica = olaksica;
            _miroStup = miroStup;
        }

        public ProcesuirajPlacu  Usporedi()
        {
            var neto = _placa.Neto;
            var bruto = _placa.Bruto;

            while (_netoIzTBoxa != neto)
            {
                if (_netoIzTBoxa < neto)
                {
                    bruto -= 0.01m;
                    neto = ProcessNeto(bruto);
                }

                if (_netoIzTBoxa <= neto) return _placa;
                {
                    bruto += 0.01m;
                    neto = ProcessNeto(bruto);
                }
            }
            return _placa;
        }

        private decimal ProcessNeto(decimal bruto)
        {
            var novaPlaca = new ProcesuirajPlacu(bruto, _prirez, _miroStup, _olaksica);
            novaPlaca.Izracun();
            var neto = novaPlaca.Neto;
            _placa = novaPlaca;
            return neto;
        }
    }
}