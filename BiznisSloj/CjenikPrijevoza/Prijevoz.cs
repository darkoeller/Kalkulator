using System.Collections.Generic;
using System.Linq;

namespace BiznisSloj.CjenikPrijevoza
{
    public static class Prijevoz
    {
        private static readonly Dictionary<string, decimal> ListaRelacija = new Dictionary<string, decimal>
        {
            {"BEZ PRIJEVOZA", 0},
            {"Antunovac - Kutina", 985.60m},
            {"Banova Jaruga - Kutina", 770m},
            {"Batina - Kutina", 431.20m},
            {"Borovac - Kutina", 1478.40m},
            {"Brekinska - Kutina", 800.8m},
            {"Brestača - Kutina", 1078m},
            {"Brezine - Kutina", 954.8m},
            {"Brinjani - Kutina", 616.0m},
            {"Bročice - Kutina", 1170.4m},
            {"Caginec - Kutina", 1386m},
            {"Ciglenica - Kutina", 770m},
            {"Do 50 km", 1509.2m},
            {"Donja Gračenica - Kutina", 400.4m},
            {"Donja Gračenica - Gojlo", 646.8m},
            {"Donja Vlahinička - Kutina", 561m},
            {"Dubrava, Stara Kapela - Kutina", 1509.2m},
            {"Duhovi - Kutina", 1078m},
            {"Gaj - Kutina", 954.8m},
            {"Garešnica - Kutina", 983.4m},
            {"Gornja Jelenska - Banova Jaruga", 1232m},
            {"Gojlo - Kutina", 616m},
            {"Gornja Gračenica - Kutina", 369m},
            {"Gornja Jelenska - Kutina", 985.6m},
            {"Gornja Vlahinička - Kutina", 800.8m},
            {"Grabričina - Kutina", 800.8m},
            {"Grabrov Potok - Kutina", 561m},
            {"Hrastovac - Kutina", 1047.20m},
            {"Husain - Kutina", 369.6m},
            {"Husain - Banova Jaruga", 616m},
            {"Husain - Gojlo", 554.4m},
            {"Ilova - Kutina", 554.4m},
            {"Ilova - Banova Jaruga", 554.4m},
            {"Ilova - Gojlo", 369.6m},
            {"Ivanić Grad - Kutina", 1478.4m},
            {"Jamarice - Kutina", 862.4m},
            {"Janja Lipa - Kutina", 954.8m},
            {"Jasenovac - Kutina", 1355.2m},
            {"Jazavica - Kutina", 1262.8m},
            {"Kaniška Iva - Kutina", 770m},
            {"Kapelica - Kutina", 770m},
            {"Katoličke Čaire - Kutina", 770m},
            {"Kletište - Kutina", 431.2m},
            {"Kloštar Ivanić - Kutina", 1509.2m},
            {"Kozarice - Kutina", 893.2m},
            {"Krajiška Kutinica - Kutina", 862.4m},
            {"Kraljeva Velika - Kutina", 893.2m},
            {"Krapje - Kutina", 1509.2m},
            {"Krivaj - Kutina", 862.4m},
            {"Križ - Kutina", 1170.4m},
            {"Kukunjevac - Kutina", 1078m},
            {"Kutina - Kutina", 369.6m},
            {"Kutinica - Kutina", 770m},
            {"Kutinska Ciglenica - Kutina", 369.6m},
            {"Kutinska Slatina - Kutina", 369.6m},
            {"Lipik - Kutina", 1478.4m},
            {"Lipovljani - Kutina", 893.2m},
            {"Lipovljani - Banova Jaruga", 554.4m},
            {"Mala Bršljanica - Kutina", 770m},
            {"Marino Selo - Kutina", 954.8m},
            {"Međurić - Kutina", 770m},
            {"Mikleuška - Kutina", 770m},
            {"Mišinka - Kutina", 431.2m},
            {"Moslavačka Slatina - Kutina", 492.8m},
            {"Nova Gradiška - Kutina", 1724.8m},
            {"Nova Subocka - Banova Jaruga", 708.4m},
            {"Nova Subocka - Kutina", 985.6m},
            {"Novska - Kutina", 1078m},
            {"Novska - Banova Jaruga", 862.4m},
            {"Okoli - Kutina", 1047.2m},
            {"Osekovo - Kutina", 492.8m},
            {"Osekovo - Gojlo", 862.4m},
            {"Pakrac - Kutina", 1509.2m},
            {"Pašijan Veliki - Kutina", 1078m},
            {"Piljenice - Kutina", 646.8m},
            {"Piljenice - Banova Jaruga", 369.6m},
            {"Poljana - Kutina", 954.8m},
            {"Popovača - Kutina", 528m},
            {"Popovača - Gojlo", 649.9m},
            {"Potok - Kutina", 528m},
            {"Rajić - Kutina", 1386m},
            {"Ravnik - Kutina", 492.8m},
            {"Repušnica - Kutina", 369.6m},
            {"Repušnica - Banova Jaruga", 924m},
            {"Repušnica - Gojlo", 523.6m},
            {"Rogoža - Kutina", 770m},
            {"Roždanik - Kutina", 1355.2m},
            {"Selište - Kutina", 554.4m},
            {"Sisak - Kutina", 858m},
            {"Sotin - Vukovar", 704m},
            {"Stara Subocka - Kutina", 985.6m},
            {"Stari Grabovac - Kutina", 1170.4m},
            {"Staro Petrovo Selo - Kutina", 1909.6m},
            {"Stružec - Kutina", 561m},
            {"Stupovača - Kutina", 708.4m},
            {"Šartovac - Kutina", 396m},
            {"Uljanik - Kutina", 1078m},
            {"Uštica - Kutina", 1139.6m},
            {"Velika Bršljanica - Kutina", 770m},
            {"Velika Ludina - Kutina", 561m},
            {"Veliko Brdo - Kutina", 429m},
            {"Veliko Vukovje - Kutina", 770m},
            {"Velika Gorica - Zagreb", 530m},
            {"Vidrenjak - Kutina", 1047.20m},
            {"Voloder - Kutina", 429m},
            {"Zagreb - Kutina", 1718.20m},
            {"Zbjegovača - Kutina", 616m},
            {"Zbjegovača - Gojlo", 616m}
        };

        public static IEnumerable<string> ListaStanica()
        {
            var stanice = ListaRelacija.Select(st => st.Key);
            return stanice;
        }

        public static decimal VratiIznosPrijevoza(string mjesto)
        {
            var iznos = ListaRelacija.AsParallel().Where(rel => string.Equals(rel.Key, mjesto)).Select(st => st.Value)
                .First();
            return iznos;
        }
    }
}