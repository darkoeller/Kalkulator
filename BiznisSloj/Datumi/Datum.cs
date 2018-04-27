namespace BiznisSloj.Datumi
{
    public struct Datum
    {
        public int Godine { get; set; }
        public int Mjeseci { get; set; }
        public int Dani { get; set; }

        public Datum(int godine, int mjeseci, int dani)
        {
            Godine = godine;
            Mjeseci = mjeseci;
            Dani = dani;
        }
    }
}