using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.Cjenik
{
    public class Prijevoz2 : ValueObject<Prijevoz2>
    {
        private string Relacija { get; set; }
        private double Iznos { get; set; }

        public override string ToString() => Relacija;

        protected override bool EqualsCore(Prijevoz2 other) =>
            string.Equals(Relacija, other.Relacija) && Iznos.Equals(other.Iznos);
  

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                return (Relacija.GetHashCode() * 397) ^ Iznos.GetHashCode();
            }
        }

        public static IEnumerable<Prijevoz2> ListaRelacija()
        {
            var jsonObject = File.ReadAllText(@"CjenikPrijevoza.json");
            var rss = JObject.Parse(jsonObject);
            var item = (JArray) rss[nameof(Cjenik)];
            IEnumerable<Prijevoz2> listaRelacija = item.AsParallel().AsOrdered().Select(p => new Prijevoz2
            {
                Relacija = (string) p["Relacija"],
                Iznos = (double) p["Iznos"]
            }).ToList();
            return listaRelacija;
        }

        public static decimal VratiIznosPrijevoza(string mjesto)
        {
            var iznos = ListaRelacija()
                .AsParallel()
                .Where(r => string.Equals(r.Relacija, mjesto))
                .Select(r => r.Iznos)
                .FirstOrDefault();
            return (decimal) iznos;
        }
    }
}