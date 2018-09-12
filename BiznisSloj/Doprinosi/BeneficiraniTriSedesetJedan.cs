namespace BiznisSloj.Doprinosi
{
    public class BeneficiraniTriSedesetJedan : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto) => bruto * 0.0361m;
    }
}