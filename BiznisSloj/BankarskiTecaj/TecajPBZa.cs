using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace BiznisSloj.BankarskiTecaj
{
    public class TecajPBZa
    {
        public decimal VratiEuro()
        {
            try
            {
                var xelement = XDocument.Load("http://www.pbz.hr/Downloads/PBZteclist.xml");
                var tecaj = xelement.Descendants("Currency")
                    .Where(a => (int) a.Attribute("Code") == 978)
                    .Elements("MeanRate")
                    .First();
                var euro = decimal.Parse(tecaj.Value);
                return euro;
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Došlo je do pogreške prilikom prezimanja podatka, \n provjerite da li imate pristup internetu"
                    , "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return 0.0m;
            }
        }
    }
}