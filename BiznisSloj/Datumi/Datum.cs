namespace BiznisSloj.Datumi
{
    public struct Datum
    {
        public int Godine { get; }
        public int Mjeseci { get; }
        public int Dani { get; }
        public double UkupnoDana { get; }

        public Datum(int godine, int mjeseci, int dani, double ukupnoDana)
        {
            Godine = godine;
            Mjeseci = mjeseci;
            Dani = dani;
            UkupnoDana = ukupnoDana;
        }
    }
}