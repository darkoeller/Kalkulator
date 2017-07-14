namespace BiznisSloj.KoefSati
{
    public abstract class KoeficSat
    {
        protected const decimal Vrijednostboda = 11.0172m;

        protected KoeficSat(decimal brojsati, decimal minuli, decimal bodovi, decimal koeficijent)
        {
            BrojSati = brojsati;
            Minuli = minuli;
            Bodovi = bodovi;
            Koeficijent = koeficijent;
        }

        protected decimal BrojSati { get; set; }
        protected decimal Minuli { get; set; }
        protected decimal Bodovi { get; set; }
        protected decimal Koeficijent { get; set; }

        public abstract decimal RacunajKoefSat();
    }
}