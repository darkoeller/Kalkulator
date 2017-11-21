namespace BiznisSloj.KoefSati
{
    public class IzracunajKoeficijentSate
    {
        public IzracunajKoeficijentSate(decimal brojSati, decimal minuli, decimal bodovi, decimal koeficijent)
        {
            BrojSati = brojSati;
            Minuli = minuli;
            Bodovi = bodovi;
            Koeficijent = koeficijent;
        }

        private decimal BrojSati { get; }
        private decimal Minuli { get; }
        private decimal Bodovi { get; }
        private decimal Koeficijent { get; }

        public decimal Izracun()
        {
            return Koeficijent > 1.0m
                ? Racunaj(new KoeficijentSatSmjene(BrojSati, Minuli, Bodovi, Koeficijent))
                : Racunaj(new KoefSatSamoPrva(BrojSati, Minuli, Bodovi, Koeficijent));
        }

        private static decimal Racunaj(KoeficSat koeficSat)
        {
            return koeficSat.RacunajKoefSat();
        }
    }
}