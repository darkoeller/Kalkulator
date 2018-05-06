namespace BiznisSloj.Datumi
{
    public class Datum
    {
        public Datum(int godine, int mjeseci, int dani, double ukupnoDana)
        {
            Godine = godine;
            Mjeseci = mjeseci;
            Dani = dani;
            UkupnoDana = ukupnoDana;
        }

        public int Godine { get; }
        public int Mjeseci { get; }
        public int Dani { get; }
        public double UkupnoDana { get; }
    }
}