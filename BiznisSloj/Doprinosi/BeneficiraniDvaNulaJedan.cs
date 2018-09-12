namespace BiznisSloj.Doprinosi
{
    public class BeneficiraniDvaNulaJedan : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto) => bruto * 0.021M;
 
    }
}