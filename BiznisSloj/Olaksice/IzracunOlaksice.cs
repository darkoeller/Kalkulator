namespace BiznisSloj.Olaksice
{
    public class IzracunOlaksice
    {
        private const decimal Koeficijent = 3800.0m;
        private readonly decimal _faktor;

        public IzracunOlaksice(decimal faktor)
        {
            _faktor = faktor;
        }


        public decimal VratiOlaksicu()
        {
            var olaksica = _faktor;

            if (olaksica <= 1)
                return _faktor * Koeficijent;
            olaksica = _faktor - 1;
            return olaksica * 2500.0m + Koeficijent;
        }
    }
}