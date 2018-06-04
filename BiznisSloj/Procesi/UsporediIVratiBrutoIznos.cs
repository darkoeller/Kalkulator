﻿using System;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;

namespace BiznisSloj.Procesi
{
    [Log(AttributeTargetElements = MulticastTargets.Method, AttributeTargetMemberAttributes = MulticastAttributes.Public)]
    public class UsporediIVratiBrutoIznos
    {
        private readonly bool _miroStup;
        private readonly decimal _netoIzTBoxa;
        private readonly decimal _olaksica;
        private readonly decimal _prirez;
        private ProcesuirajPlacu _placa;

        public UsporediIVratiBrutoIznos(decimal neto, ProcesuirajPlacu placa, decimal prirez, decimal olaksica,
            bool miroStup)
        {
            _netoIzTBoxa = neto;
            _placa = placa;
            _prirez = prirez;
            _olaksica = olaksica;
            _miroStup = miroStup;
        }

        public ProcesuirajPlacu Usporedi()
        {
            var neto = Math.Round(_placa.Neto, 2);
            var bruto = Math.Round(_placa.Bruto, 2);

            while (_netoIzTBoxa != neto)
            {
                if (_netoIzTBoxa < neto)
                {
                    bruto -= 0.01m;
                    neto = ProcessNeto(bruto);
                }

                if (_netoIzTBoxa <= neto) return _placa;
                {
                    bruto += 0.01m;
                    neto = ProcessNeto(bruto);
                }
            }

            return _placa;
        }

        private decimal ProcessNeto(decimal bruto)
        {
            var novaPlaca = new ProcesuirajPlacu(bruto, _prirez, _miroStup, _olaksica);
            novaPlaca.Izracun();
            var neto = novaPlaca.Neto;
            _placa = novaPlaca;
            return neto;
        }
    }
}