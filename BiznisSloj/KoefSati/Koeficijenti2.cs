using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.KoefSati
{
    public class Koeficijenti2 : ValueObject<Koeficijenti2>
    {
        static Koeficijenti2() =>  VratiSifre();

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

        protected override bool EqualsCore(Koeficijenti2 other) => string.Equals(Sifra, other?.Sifra) && string.Equals(Naziv, other?.Naziv) &&
                                                                   Koeficijent.Equals(other?.Koeficijent);
   
        protected override int GetHashCodeCore()
        {
            unchecked
            {
                var hashCode = Sifra.GetHashCode();
                hashCode = (hashCode * 397) ^ Naziv.GetHashCode();
                hashCode = (hashCode * 397) ^ Koeficijent.GetHashCode();
                return hashCode;
            }
        }
    }
}