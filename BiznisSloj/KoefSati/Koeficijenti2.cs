using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.KoefSati
{
    public class Koeficijenti2
    {
        static Koeficijenti2() =>  VratiSifre();


        public string Sifra { get; set; }
        public string Naziv { get; set; }
        public double Koeficijent { get; set; }

        private bool Equals(Koeficijenti2 other) => string.Equals(Sifra, other.Sifra) && string.Equals(Naziv, other.Naziv) &&
                   Koeficijent.Equals(other.Koeficijent);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Koeficijenti2 koeficijenti2 && Equals(koeficijenti2);
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

        public static decimal VratiIznos(string naziv)
        {
            var rezultat = VratiSifre()
                .AsParallel()
                .Where(r => string.Equals(r.Naziv, naziv))
                .Select(r => r.Koeficijent)
                .FirstOrDefault();
            return (decimal) rezultat;
        }

        public static IEnumerable<Koeficijenti2> VratiSifre()
        {
            var jsonObject = File.ReadAllText(@"Koeficijenti.json");
            var rss = JObject.Parse(jsonObject);
            var item = (JArray) rss["Koeficijenti"];
            IList<Koeficijenti2> listaKoeficijenata = item.Select(p => new Koeficijenti2
            {
                Sifra = (string) p["Sifra"],
                Naziv = (string) p["Naziv"],
                Koeficijent = (double) p["Koeficijent"]
            }).ToList();
            return listaKoeficijenata;
        }
    }
}