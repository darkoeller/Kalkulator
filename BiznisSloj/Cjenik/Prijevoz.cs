using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BiznisSloj.Cjenik
{
    public struct Prijevoz
    {

        public static IEnumerable<string> VratiListu()
        {
            var listaRelacija = new List<string>();
            var xelement = VratiCjenik();
            var elemList = xelement.Descendants("Relacije")
                .Where(r => r.Element("Relacija") != null)
                .Elements("Relacija").ToList();
            foreach (var rel in elemList)
            {
                listaRelacija.Add(rel.Value);
            } 
            return listaRelacija;
        }

        public static decimal VratiIznosPrijevoza(string mjesto)
        {
            var xelement = VratiCjenik();
            var prevoz = xelement.Descendants("Relacije").AsParallel()
                .Where(a => (string) a.Element("Relacija") == mjesto)
                .Elements("Iznos")
                .First();
            var iznos = decimal.Parse(prevoz.Value);
            iznos = iznos / 10;
            return iznos;
        }

        private static XDocument VratiCjenik()
        {
            var xelement = XDocument.Load("Cjenik.xml");
            return xelement;
        }
    }
}