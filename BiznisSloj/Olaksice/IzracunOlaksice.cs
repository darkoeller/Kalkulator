namespace BiznisSloj.Olaksice
{
    public class IzracunOlaksice
    {
        private readonly decimal _faktor;

        private const decimal Koeficijent = 3800.0m;

        public IzracunOlaksice(decimal faktor)
        {
            _faktor = faktor;
        }

        

        public decimal VratiOlaksicu()
        {
            var olaksica = _faktor;

            if (olaksica <= 1)
            {
                return _faktor*Koeficijent;
            }
            else
            {
                olaksica = _faktor -1;
                return (olaksica * 2500.0m) + Koeficijent;
            }
        }
    }
}