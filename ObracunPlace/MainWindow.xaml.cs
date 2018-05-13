using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BiznisSloj;
using BiznisSloj.Cjenik;
using BiznisSloj.Ispis;
using BiznisSloj.Porezi;
using BiznisSloj.Procesi;

namespace ObracunPlace
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ProcesuirajPlacu _listica;

        public MainWindow()
        {
            InitializeComponent();
        }

        private decimal Prirez { get; set; }
        private bool Stup1I2 => bool.Parse(Rb1I2Stup.IsChecked.ToString());
        private decimal IznosPrijevoza { get; set; }

        private decimal GetOlaksica()
        {
            decimal.TryParse(OlaksicaUpDown.Text, out var olaksica);
            return olaksica;
        }

        private decimal GetBruto()
        {
            decimal.TryParse(TxtBruto.Text, out var bruto);
            return bruto;
        }

        private decimal GetNeto()
        {
            decimal.TryParse(TxtNeto.Text, out var neto);
            return neto;
        }

        private decimal GetOdbici()
        {
            decimal.TryParse(TxtBoxOdbici.Text, out var odbici);
            return odbici;
        }

        private void BtnIzracun_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBruto.Text))
            {
                MessageBox.Show("Nema iznosa u brutu/netu.",
                    "Provjerite!");
                return;
            }

            if (ProvjeriRadioGumb())
            {
                if (string.IsNullOrEmpty(TxtBruto.Text)) return;
                var placa = ProcesuirajBruto.VratiIzracunPlace(GetBruto(), GetOlaksica(), Prirez, Stup1I2);
                PopuniVrijednosti(placa);
                OduzmiOdbitke();
            }
            else
            {
                TxtBruto.Text = string.Empty;
                if (string.IsNullOrEmpty(TxtNeto.Text)) return;
                var izracunPlaceIzNeta = new ProcesuirajNeto(GetNeto(), GetOlaksica(), Prirez, Stup1I2).Izracunaj();
                TxtBruto.Text = izracunPlaceIzNeta.Bruto.ToString(new CultureInfo("hr-HR"));
                PopuniVrijednosti(izracunPlaceIzNeta);
            }
        }

        private void OduzmiOdbitke()
        {
            if (string.IsNullOrEmpty(TxtBoxOdbici.Text)) return;
            var neto = GetNeto();
            var odbici = GetOdbici();
            var prijevoz = GetPrijevoz();
            if (CmbPrijevoz.SelectedIndex < 1)
            {
                neto -= odbici;
                LblPrijevoz.Content = Math.Round(neto, 2).ToString("C");
                LblOdbici.Content = Math.Round(neto, 2).ToString("C");
            }
            else if (CmbPrijevoz.SelectedIndex > 0)
            {
                VratiTotal(prijevoz, odbici);
            }
        }

        private decimal GetPrijevoz()
        {
            var stringPrijevoz = LblPrijevoz.Content.ToString();
            stringPrijevoz = stringPrijevoz.Substring(0, stringPrijevoz.Length - 2);
            decimal.TryParse(stringPrijevoz, out var prijevoz);
            return prijevoz;
        }

        private bool ProvjeriRadioGumb()
        {
            return RbBruto.IsChecked != false;
        }

        private void PopuniVrijednosti(ProcesuirajPlacu placa)
        {
            TxtNeto.Text = Math.Round(placa.Neto, 2).ToString(new CultureInfo("hr-HR"));
            Lbl1Stup.Content = Math.Round(placa.PetnaestPostoDoprinos, 2).ToString("C");
            Lbl2Stup.Content = Math.Round(placa.PetPostoDoprinos, 2).ToString("C");
            LblDopUkupno.Content = Math.Round(placa.DoprinosiIzPlaceUkupno, 2).ToString("C");
            LblBruto.Content = Math.Round(placa.Bruto, 2).ToString("C");
            LblDohodak.Content = Math.Round(placa.Dohodak, 2).ToString("C");
            LblOdbitak.Content = Math.Round(placa.Olaksica, 2).ToString("C");
            LblPorOsnovica.Content = Math.Round(placa.PoreznaOsnovica, 2).ToString("C");
            LblPorezUkupno.Content = Math.Round(placa.UkupniPorez, 2).ToString("C");
            LblPorez25.Content = Math.Round(placa.PorezDvadesetCetiriPosto, 2).ToString("C");
            LblPorez40.Content = Math.Round(placa.PorezTridesetSestPosto, 2).ToString("C");
            LblPrirez.Content = Math.Round(placa.Prirez, 2).ToString("C");
            LblZdravstveno.Content = Math.Round(placa.DoprinosZaZdravstveno, 2).ToString("C");
            LblZnr.Content = Math.Round(placa.DoprinosZaZnr, 2).ToString("C");
            LblZaposljavanje.Content = Math.Round(placa.DoprinosZaZaposljavanje, 2).ToString("C");
            LblDoprinosiUkupno.Content = Math.Round(placa.DoprinosNaPlacUkupno, 2).ToString("C");
            LblTrosakPlace.Content = Math.Round(placa.UkupniTrosakPlace, 2).ToString("C");
            _listica = placa;
        }

        private void BtnOcisti_Clic(object sender, RoutedEventArgs e)
        {
            TxtBruto.Text = "0,00";
            TxtNeto.Text = "0,00";
            OcistiLabele();
            TxtBoxOdbici.Text = "0.00";
            CmbPrijevoz.SelectedIndex = 0;
        }

        private void OcistiLabele()
        {
            foreach (var labela in StPanel2.Children.OfType<Label>()) labela.Content = $"{0.00:C2}";
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            UkljuciGumb();
            TabKontrola.SelectedIndex += 1;
            CmbPrirez.ItemsSource = Prirezi.ListaPrireza();
            CmbPrirez.SelectedIndex = 0;
            CmbPrijevoz.ItemsSource = Prijevoz.VratiListu();
            CmbPrijevoz.SelectedIndex = 0;
        }

        private void UkljuciGumb()
        {
            RbBruto.IsChecked = true;
        }

        private void RbBruto_Checked(object sender, RoutedEventArgs e)
        {
            OcistiLabele();
            var kontrole = new List<Control> {TxtBoxOdbici, TxtBruto, CmbPrijevoz};
            OmoguciKontrole(kontrole, true);
            TxtBruto.Focus();
            TxtNeto.IsEnabled = false;
            TxtNeto.Text = "0,00";
        }

        private static void OmoguciKontrole(IEnumerable<Control> kontrole, bool omoguci)
        {
            foreach (var kontrolu in kontrole) kontrolu.IsEnabled = omoguci;
        }


        private void RbNeto_Checked(object sender, RoutedEventArgs e)
        {
            BtnOcisti_Clic(this, null);
            var kontrole = new List<Control> {TxtBoxOdbici, TxtBruto, CmbPrijevoz};
            OmoguciKontrole(kontrole, false);
            TxtNeto.IsEnabled = true;
            TxtNeto.Text = string.Empty;
            TxtNeto.Focus();
            TxtBruto.Text = "0,00";
        }

        private void CmbPrijevoz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IznosPrijevoza = Math.Round(Prijevoz.VratiIznosPrijevoza(CmbPrijevoz.SelectedItem.ToString()), 2);
            var prijevoz = IznosPrijevoza;
            prijevoz += GetNeto();
            LblPrijevoz.Content = Math.Round(prijevoz, 2).ToString("C", new CultureInfo("hr-HR"));
            var odbitak = Convert.ToDecimal(TxtBoxOdbici.Text);
            VratiTotal(prijevoz, odbitak);
        }

        private void VratiTotal(decimal prijevoz, decimal odbici)
        {
            prijevoz -= odbici;
            LblOdbici.Content = Math.Round(prijevoz, 2).ToString("C");
        }

        private void LblZatvori_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void CmbPrirez_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Prirez = decimal.Parse(CmbPrirez.SelectedItem.ToString());
        }

        private void Ispis_Click(object sender, RoutedEventArgs e)
        {
            if (_listica == null)
            {
                MessageBox.Show("Provjerite da li ste izračunali iznose", "Pozor", MessageBoxButton.OK
                    , MessageBoxImage.Information);
                return;
            }

            var podaciZaIspis = new PodaciZaIspisPlace
            {
                Placa = _listica,
                Prijevoz = IznosPrijevoza,
                TxtOdbiciIznos = double.Parse(TxtBoxOdbici.Text),
                LblOdbici = LblOdbici.Content.ToString(),
                LblPrijevoz = LblPrijevoz.Content.ToString(),
                NaslovniText = NaslovniText.Text,
                PrirezTxtB = CmbPrirez.SelectedValue.ToString()
            };
            new IspisListicePlace(podaciZaIspis).Ispis();
        }

        private void Rb1Stup_Checked(object sender, RoutedEventArgs e)
        {
            LblDopUkupno.Content = Math.Round(_listica.DvadesetPostoDoprinos, 2).ToString("C");
        }
    }
}