using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BiznisSloj;
using BiznisSloj.Datumi;

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
            Mediator.GetInstance().NoviBruto += (s, e) => TextBoxNeto.Text = e.BrutoIznos;
            TextBoxNeto.Focus();
        }

        private byte RbOvrha { get; set; }

        private void IzracunajNeto_Click(object sender, RoutedEventArgs e)
        {
            var ovrhaText = TextBoxNeto.Text;
            if (string.IsNullOrEmpty(ovrhaText)) return;
            if (ovrhaText.Contains('.')) ovrhaText = ovrhaText.Replace('.', ',');
            decimal.TryParse(ovrhaText, out _neto);
            var ovrha = new Ovrha(_neto, RbOvrha);
            TxtNetoOstaje.Text = ovrha.IzracunajOvrhu().ToString(CultureInfo.InvariantCulture);
            TxtNetoOvrha.Text = ovrha.ZaOvrsiti.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnCisti_Click(object sender, RoutedEventArgs e)
        {
            TextBoxNeto.Text = "";
            TxtNetoOstaje.Text = "";
            TxtNetoOvrha.Text = "";
        }

        private void IzracunajDatum()
        {
            if (PocetniDt.SelectedDate == null || ZavrsniDt.SelectedDate == null) return;
            var pocetno = (DateTime) PocetniDt.SelectedDate;
            var zavrsno = (DateTime) ZavrsniDt.SelectedDate;
            var razlika = new RazlikaDatuma(pocetno, zavrsno).VratiIzracun();
            LblUkupnoDana.Content = razlika.UkupnoDana;
            LblGodine.Content = razlika.Godine;
            LblMjeseci.Content = razlika.Mjeseci;
            LblDani.Content = razlika.Dani;
        }

        private void PocetniDt_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            IsprazniLabele();
            IzracunajDatum();
        }

        private void IsprazniLabele()
        {
            LblUkupnoDana.Content = "0";
            LblGodine.Content = "0";
            LblMjeseci.Content = "0";
            LblDani.Content = "0";
        }

        private void ZavrsniDt_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            IsprazniLabele();
            IzracunajDatum();
        }

        private void RbClanak_Checked(object sender, RoutedEventArgs e)  => RbOvrha = 1;

        private void RbUzdrzavanje_Checked(object sender, RoutedEventArgs e)
        {
            RbOvrha = 2;
            TxtNetoOstaje.Text = "";
            TxtNetoOvrha.Text = "";
            TextBoxNeto.Focus();
        }

        private void RbUzdrzavanjeDjeteta_Checked(object sender, RoutedEventArgs e)
        {
            RbOvrha = 3;
            TxtNetoOstaje.Text = "";
            TxtNetoOvrha.Text = "";
            TextBoxNeto.Focus();
        }
    }
}