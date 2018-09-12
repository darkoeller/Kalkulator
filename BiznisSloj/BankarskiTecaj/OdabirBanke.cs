namespace BiznisSloj.BankarskiTecaj
{
    public class OdabirBanke
    {
        private readonly string _odabir;

        public OdabirBanke(string odabir) => _odabir = odabir;

        public decimal VratiIznos()
        {
            switch (_odabir)
            {
                case "HNB":
                    return Racunaj(new TecajHnBa());
                case "PBZ":
                    return Racunaj(new TecajPbZa());
            }

            return 0.0m;
        }

        private static decimal Racunaj(ITecaj tecaj) => tecaj.VratiEuro();
    }
}