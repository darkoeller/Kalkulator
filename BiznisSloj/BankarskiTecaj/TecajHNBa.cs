using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BiznisSloj.BankarskiTecaj
{
    public struct TecajHnBa : ITecaj
    {
        public decimal VratiEuro()
        {
            //try
            //{   
            //    var client = new WebClient();
            //    var web = FormirajWebString();
            //    var content = client.DownloadString(web);
            //    content = content.Replace(".", ",");
            //    if (string.IsNullOrEmpty(content))
            //    {
            //       decimal.TryParse(content, out var tecaj);
            //       return tecaj; 
            //    }
                    try
                    {
                        NadjiSaWebaHNBa();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(
            //        "Došlo je do pogreške prilikom prezimanja podatka, \n provjerite da li imate pristup internetu"
            //        , "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return 0.0m;
            //}

            return 0;
        }

        private void NadjiSaWebaHNBa()
        {
            var jsonObject = new WebClient().DownloadString(@"http://api.hnb.hr/tecajn/v1?valuta=EUR");
            var rss =JArray.Parse(jsonObject);
            //var token = rss.SelectToken()
            //var nasao = rss.SelectToken("Srednji za devize").HasValues;

        }

        private static string FormirajWebString()
        {
            var web = DateTime.Today.ToShortDateString().Remove(9,1);
            var bilder = new StringBuilder("https://www.nabava.net/labs/hnb-tecaj/p/"+web+"/srednji/eur");
            return bilder.ToString();
        }
    }
}