using System;
using System.Net;
using System.Text;

namespace BiznisSloj.BankarskiTecaj
{
    public struct TecajHnBa : ITecaj
    {
        public decimal VratiEuro()
        {
            using (var client = new WebClient())
            {
                var web = FormirajWebString();
                var content = client.DownloadString(web);
                content = content.Replace(".", ",");
                decimal.TryParse(content, out var tecaj);
                return tecaj;
            }
        }

        private static string FormirajWebString()
        {
            var bilder = new StringBuilder("https://www.nabava.net/labs/hnb-tecaj/p/");
            var web = DateTime.Today.ToShortDateString().Remove(9,1);
            bilder.Append(web + "/srednji/eur");
            return bilder.ToString();
        }
    }
}