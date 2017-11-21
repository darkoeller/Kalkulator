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
<<<<<<< HEAD
                    var hnb = new TecajHNBa().VratiEuro();
                    return hnb;
                case "PBZ":
                    var pbz = new TecajPBZa().VratiEuro();
                    return pbz;
                    
=======
                    return Racunaj(new TecajHNBa());
                case "PBZ":
                    return Racunaj(new TecajPBZa());
>>>>>>> origin/develop
            }
            return 0.0m;
        }

        private static decimal Racunaj(ITecaj tecaj)
        {
            return tecaj.VratiEuro();
        }
    }
}