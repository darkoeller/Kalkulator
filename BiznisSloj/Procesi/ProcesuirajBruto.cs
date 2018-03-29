namespace BiznisSloj.Procesi
{
    public static class ProcesuirajBruto
    {
        public static ProcesuirajPlacu VratiIzracunPlace(decimal getBruto, decimal olaksica, decimal prirez, bool stup1I2)
        {
            var placa = new ProcesuirajPlacu(getBruto, prirez, stup1I2, olaksica);
            placa.Izracun();
            return placa;
        }
    }
}