using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using BiznisSloj.BankarskiTecaj;

namespace ObracunPlace
{
    /// <summary>
    ///   Interaction logic for TecajEura.xaml
    /// </summary>
    public partial class TecajEura
    {
        private decimal _euro;

        public TecajEura()
        {
            InitializeComponent();
            CmbBanke.SelectedIndex = 0;
            TxtDatum.Text = DateTime.Now.ToShortDateString();
        }

        private void CmbBanke_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = CmbBanke.SelectedIndex;
            switch (index)
            {
                case 0:
                    TxtNazivBanke.Text = " (odaberite banku) ";
                    LblTecaj.Content = "0,00 ";
                    break;
                case 1:
                    _euro = new OdabirBanke("HNB").VratiIznos();
                    LblTecaj.Content = _euro;
                    TxtNazivBanke.Text = " Hrvatskoj narodnoj banci, (HNB-u) je : ";
                    break;
                case 2:
                    _euro = new OdabirBanke("PBZ").VratiIznos();
                    LblTecaj.Content = _euro;
                    TxtNazivBanke.Text = " Privrednoj banci Zagreb, (PBZ-u) je : ";
                    break;
                default:
                    TxtNazivBanke.Text = "'Odaberite banku!'";
                    break;
            }

            TxtEuro.Focus();
        }

        private void BtnIzracun_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtEuro.Text))
            {
                LblEuri.Content = "0,00";
                return;
            }

            decimal.TryParse(TxtEuro.Text, out var rata);
            rata = Math.Round(_euro * rata, 2);
            LblEuri.Content = rata.ToString(CultureInfo.CurrentCulture);
        }

        private void OdabraniDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datum = OdabraniDatum.SelectedDate;
            var stringDatum = OdabraniDatum.SelectedDate.ToString();


            if (datum > DateTime.Today || datum < DateTime.Today.AddYears(-25) || string.IsNullOrEmpty(stringDatum))
            {
                MessageBox.Show("Odabrani datum mora biti manji od današnjeg,\n  ne stariji od 25 godina i ne prazan.",
                    "Pozor!", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
            else
            {
                var zaPoslati = new TecajEuraPoDatumu(datum).OblikujDatum();
                stringDatum = stringDatum.Replace("0:00:00", "");
                LblBivsiEuro.Content =
                    "Srednji tečaj eura na dan " + stringDatum + " iznosio je : " + zaPoslati + " kuna.";
            }
        }
    }
}