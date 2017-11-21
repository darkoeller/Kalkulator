using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace BiznisSloj.BankarskiTecaj
{
    public class TecajHNBa : ITecaj
    {
        public decimal VratiEuro()
        {
            try
            {
                var xelement = XDocument.Load("http://api.hnb.hr/tecajn?valuta=EUR&format=xml");
                var tecaj = xelement.Descendants().First(x => x.Name == "srednji_tecaj");
                var euro = decimal.Parse(tecaj.Value);
                return euro;
            }

            catch (Exception)
            {
                MessageBox.Show(
                    "Došlo je do pogreške prilikom prezimanja podataka,\n provjerite da li imate pristup internetu."
                    , "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return 0.0m;
            }
        }
    }
}