namespace BiznisSloj.BankarskiTecaj
{
    public class OdabirBanke
    {
        private readonly string _odabir;

        public OdabirBanke(string odabir)
        {
            _odabir = odabir;
        }

        public decimal VratiIznos()
        {
            switch (_odabir)
            {
                case "HNB":
                    return Racunaj(new TecajHNBa());
                case "PBZ":
                    return Racunaj(new TecajPBZa());
            }

            return 0.0m;
        }

        private static decimal Racunaj(ITecaj tecaj)
        {
            return tecaj.VratiEuro();
        }
    }
}