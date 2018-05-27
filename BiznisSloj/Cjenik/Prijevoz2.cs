using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.Cjenik
{
    public struct Prijevoz2
    {
        private string Relacija { get; set; }
        private double Iznos { get; set; }

        public static bool operator ==(Prijevoz2 left, Prijevoz2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Prijevoz2 left, Prijevoz2 right)
        {
            return !left.Equals(right);
        }

        private bool Equals(Prijevoz2 other)
        {
            return string.Equals(Relacija, other.Relacija) && Iznos.Equals(other.Iznos);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Prijevoz2 prijevoz2 && Equals(prijevoz2);
        }

        public override int GetHashCode()
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
            var item = (JArray) rss["Cjenik"];
            IEnumerable<Prijevoz2> listaRelacija = item.Select(p => new Prijevoz2
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

        public override string ToString()
        {
            return Relacija;
        }
    }
}