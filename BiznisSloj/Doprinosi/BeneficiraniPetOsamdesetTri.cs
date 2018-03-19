namespace BiznisSloj.Doprinosi
{
    public class BeneficiraniPetOsamdesetTri : IDoprinosi
    {
        public  decimal RacunajDoprinos(decimal bruto)
        {
            return bruto * 0.0583m;
        }
    }
}