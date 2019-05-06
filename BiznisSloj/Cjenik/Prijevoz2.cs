using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.Cjenik
{
    [JsonObject("Cjenik")]
    public class Prijevoz2 : ValueObject<Prijevoz2>
    {
        [JsonProperty("Relacija")]
        private string Relacija { get; set; }
        [JsonProperty("Iznos")]
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
            Assembly assembly = Assembly.GetExecutingAssembly();
            try
            {
                using (var stream = assembly.GetManifestResourceStream(@"BiznisSloj.Cjenik.CjenikPrijevoza.json"))
                using (var reader = new StreamReader(stream))
                using (var jsonTextRider = new JsonTextReader(reader))
                {
                 var rss = (JObject) JToken.ReadFrom(jsonTextRider);
                 var item = (JArray) rss[nameof(Cjenik)];
                 IEnumerable<Prijevoz2> listaRelacija = item.AsParallel().AsOrdered().Select(p => new Prijevoz2
                 {
                     Relacija = (string) p[nameof(Relacija)],
                     Iznos = (double) p[nameof(Iznos)]
                 }).ToList().AsReadOnly();
                 return listaRelacija;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
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