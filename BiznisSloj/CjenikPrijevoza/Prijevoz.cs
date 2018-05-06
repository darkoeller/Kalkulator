using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using PostSharp.Patterns.Threading;

namespace BiznisSloj.CjenikPrijevoza
{
    public struct Prijevoz
    {
        public string Relacija { get; set; }
        public double Iznos { get; set; }

        public static bool operator ==(Prijevoz left, Prijevoz right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Prijevoz left, Prijevoz right)
        {
            return !left.Equals(right);
        }

        private bool Equals(Prijevoz other)
        {
            return string.Equals(Relacija, other.Relacija) && Iznos.Equals(other.Iznos);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Prijevoz && Equals((Prijevoz) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Relacija.GetHashCode() * 397) ^ Iznos.GetHashCode();
            }
        }

        [Background]
        public static IEnumerable<Prijevoz> ListaRelacija()
        {
            var jsonObject = File.ReadAllText(@"CjenikPrijevoza.json");
            var rss = JObject.Parse(jsonObject);
            var item = (JArray) rss["Cjenik"];
            IList<Prijevoz> listaRelacija = item.Select(p => new Prijevoz
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