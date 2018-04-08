using BiznisSloj.Procesi;

namespace BiznisSloj.Ispis
{
    public class PodaciZaIspisPlace
    {
        public ProcesuirajPlacu Placa { get; set; }
        public decimal Prijevoz { get; set; }
        public double? TxtOdbiciIznos { get; set; }
        public string LblOdbici { get; set; }
        public string LblPrijevoz { get; set; }
        public string NaslovniText { get; set; }
    }
}