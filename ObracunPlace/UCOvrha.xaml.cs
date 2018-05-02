﻿using System;
using System.Globalization;
using System.Windows;
using BiznisSloj;
using BiznisSloj.Datumi;
using PostSharp.Patterns.Threading;

namespace ObracunPlace
{
    /// <summary>
    ///     Interaction logic for UCOvrha.xaml
    /// </summary>
    public partial class UcOvrha
    {
        private decimal _neto;

        public UcOvrha()
        {
            InitializeComponent();
            TextBoxNeto.Focus();
        }


        private void IzracunajNeto_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxNeto.Text)) return;
            decimal.TryParse(TextBoxNeto.Text, out _neto);
            var ovrha = new Ovrha(_neto);
            TxtNetoOstaje.Text = ovrha.IzracunajOvrhu().ToString(CultureInfo.InvariantCulture);
            TxtNetoOvrha.Text = ovrha.ZaOvrsiti.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnCisti_Click(object sender, RoutedEventArgs e)
        {
            TextBoxNeto.Text = "";
            TxtNetoOstaje.Text = "";
            TxtNetoOvrha.Text = "";
        }

        private void IzracunajDatum_Click(object sender, RoutedEventArgs e)
        {
            if (PocetniDt.SelectedDate == null || ZavrsniDt.SelectedDate == null) return;
            var pocetno =(DateTime) PocetniDt.SelectedDate;
            var zavrsno = (DateTime) ZavrsniDt.SelectedDate;
            var razlika = new RazlikaDatuma(pocetno,zavrsno).VratiIzracun();
            LblUkupnoDana.Content = razlika.UkupnoDana;
            LblGodine.Content = razlika.Godine;
            LblMjeseci.Content = razlika.Mjeseci;
            LblDani.Content = razlika.Dani;
        }

        private void PocetniDt_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            IsprazniLabele();
            IzracunajDatum_Click(this, null);
        }
        [Background]
        private void IsprazniLabele()
        {   
            LblUkupnoDana.Content = "0";
            LblGodine.Content = "0";
            LblMjeseci.Content = "0";
            LblDani.Content = "0";
        }

        private void ZavrsniDt_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            IsprazniLabele();
            IzracunajDatum_Click(this, null);
        }
    }
}