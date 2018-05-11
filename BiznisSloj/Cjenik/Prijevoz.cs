using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace BiznisSloj.Cjenik
{
    public struct Prijevoz
    {
        private static readonly List<string> ListaRelacija = new List<string>();

        public static IEnumerable<string> VratiListu()
        {
            var doc = new XmlDocument();
            doc.Load("Cjenik.xml");
            var elemList = doc.GetElementsByTagName("Relacija");
            for (var i = 0; i < elemList.Count; i++)
            {
               ListaRelacija.Add(elemList[i].InnerXml);
            }
            return ListaRelacija;
        }
        


        //public static IEnumerable<string> ListaStanica()
        //{
        //    var stanice = ListaRelacija.Select(st => st.Key);
        //    return stanice;
        //}

        //public static decimal VratiIznosPrijevoza(string mjesto)
        //{
        //    var iznos = ListaRelacija.AsParallel().Where(rel => string.Equals(rel.Key, mjesto)).Select(st => st.Value)
        //        .First();
        //    return iznos;
        //}
    }
}