﻿namespace BiznisSloj.Doprinosi
{
    public class DoprinosPetnaestPosto : IDoprinosi
    {
        public decimal RacunajDoprinos(decimal bruto) => bruto* 0.15m;
    }
}