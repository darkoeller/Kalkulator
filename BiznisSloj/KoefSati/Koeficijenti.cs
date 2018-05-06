using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using PostSharp.Patterns.Threading;

namespace BiznisSloj.KoefSati
{
    public class Koeficijenti
    {
        private bool Equals(Koeficijenti other)
        {
            return string.Equals(Sifra, other.Sifra) && string.Equals(Naziv, other.Naziv) &&
                   Koeficijent.Equals(other.Koeficijent);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Koeficijenti && Equals((Koeficijenti) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Sifra.GetHashCode();
                hashCode = (hashCode * 397) ^ Naziv.GetHashCode();
                hashCode = (hashCode * 397) ^ Koeficijent.GetHashCode();
                return hashCode;
            }
        }

        static Koeficijenti()
        {
            VratiSifre();
        }

        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public double Koeficijent { get; set; }

        public static decimal VratiIznos(string naziv)
        {
            var rezultat = VratiSifre()
                .AsParallel()
                .Where(r => string.Equals(r.Naziv, naziv))
                .Select(r => r.Koeficijent)
                .FirstOrDefault();
            return (decimal) rezultat;
        }

        [Background]
        public static IEnumerable<Koeficijenti> VratiSifre()
        {
            var jsonObject = File.ReadAllText(@"Koeficijenti.json");
            var rss = JObject.Parse(jsonObject);
            var item = (JArray) rss["Koeficijenti"];
            IList<Koeficijenti> listaKoeficijenata = item.Select(p => new Koeficijenti
            {
                Sifra = (string) p["Sifra"],
                Naziv = (string) p["Naziv"],
                Koeficijent = (double) p["Koeficijent"]
            }).ToList();
            return listaKoeficijenata;
        }
    }
}