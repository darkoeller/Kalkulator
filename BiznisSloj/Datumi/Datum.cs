namespace BiznisSloj.Datumi
{
    public struct Datum
    {
        public int Godine { get; private set; }
        public int Mjeseci { get; private set; }
        public int Dani { get; private set; }

        public Datum(int godine, int mjeseci, int dani)
        {
            Godine = godine;
            Mjeseci = mjeseci;
            Dani = dani;
        }
    }
}