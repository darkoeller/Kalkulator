namespace BiznisSloj.Datumi
{
    public struct Datum
    {
        public int Godine { get; private set; }
        public int Mjeseci { get; private set; }
        public int Dani { get; private set; }
        public double UkupnoDana { get; private set; }

        public Datum(int godine, int mjeseci, int dani, double ukupnoDana)
        {
            Godine = godine;
            Mjeseci = mjeseci;
            Dani = dani;
            UkupnoDana = ukupnoDana;
        }
    }
}