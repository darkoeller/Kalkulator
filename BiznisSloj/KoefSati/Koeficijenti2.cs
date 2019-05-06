using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.KoefSati
{
    [JsonObject("Koeficijenti")]
    public class Koeficijenti2 : ValueObject<Koeficijenti2>
    {
        static Koeficijenti2() =>  VratiSifre();
        [JsonProperty("Sifra")]
        public string Sifra { get; set; }
        [JsonProperty("Naziv")]
        public string Naziv { get; set; }
        [JsonProperty("Iznos")]
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
            Assembly assembly = Assembly.GetExecutingAssembly();
            //var imena = assembly.GetManifestResourceNames();
            try
            {
                using (var stream = assembly.GetManifestResourceStream(@"BiznisSloj.KoefSati.Koeficijenti.json"))
                using (var reader = new StreamReader(stream))
                using (var jsnonTextRider = new JsonTextReader(reader))
                {
                     var rss = (JObject) JToken.ReadFrom(jsnonTextRider);
                     var item = (JArray) rss["Koeficijenti"];
                     IList<Koeficijenti2> listaKoeficijenata = item
                   .Select(p => new Koeficijenti2
                   {
                        Sifra = (string) p[nameof(Sifra)],
                        Naziv = (string) p[nameof(Naziv)],
                        Koeficijent = (double) p[nameof(Koeficijent)]
                   }).ToList().AsReadOnly();
                return listaKoeficijenata;
                }                
            }
            catch (System.Exception)
            {
                throw;
            } 
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