namespace BiznisSloj.BankarskiTecaj
{
    public class OdabirBanke
    {
        private readonly string _odabir;
        //private readonly ITecaj _tecaj;

        public OdabirBanke(string odabir)
        {
            _odabir = odabir;
        }

        public decimal VratiIznos()
        {
            decimal euro;
            switch (_odabir)
            {
                case "HNB":
                    var hnb = new TecajHNBa().VratiEuro();
                    return hnb;
                case "PBZ":
                    var pbz = new TecajPBZa().VratiEuro();
                    return pbz;
                    
            }
            return 0.0m;
        }
    }
}