using System.Collections;
using System.Collections.Generic;

namespace BiznisSloj.Porezi
{
    public static class Prirezi
    {
        private static readonly IEnumerable<string> Popisprireza = new List<string>
        {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "6,25",
            "6,5",
            "7",
            "7,5",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "18"
        };

        public static IEnumerable ListaPrireza()
        {
            return Popisprireza;
        }
    }
}