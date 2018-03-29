using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;

namespace BiznisSloj.Porezi
{
    public static  class Prirezi
    {
        private static readonly IEnumerable<string> Popisprireza = new List<string>
        {
            "0","1","2",
            //2m,
            //3m,
            //4m,
            //5m,
            //6m,
            //6.25m,
            //6.5m,
            //7m,
            //7.5m,
            //8m,
            //9m,
            //10m,
            //11m,
            //12m,
            //13m,
            //14m,
            //15m,
            //18m
        };

        public static IEnumerable ListaPrireza()
        {
            var prirezi = Popisprireza;
            return prirezi.ToString();
        }
    }
}