using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using BiznisSloj;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Label = System.Windows.Controls.Label;

namespace ObracunPlace
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static ProcesuirajPlacu Listica;
        private static readonly IEnumerable<decimal> Popisprireza = new List<decimal>{0, 1m, 2m, 3m, 4m, 5m, 6m, 6.25m, 6.5m, 7m, 7.5m, 8m, 9m, 10m, 11m, 12m, 13m, 14m, 15m, 18m};


        public MainWindow()
        {
            InitializeComponent();
        }

        private decimal Bruto => decimal.Parse(TxtBruto.Text);
        private decimal Olaksica => decimal.Parse(OlaksicaUpDown.Value.ToString());
        private decimal Prirez => decimal.Parse(PrirezUpDown.Value.ToString());
        private decimal Neto => decimal.Parse(TxtNeto.Text);
        private bool Stup1I2 => bool.Parse(Rb1I2Stup.IsChecked.ToString());
        private bool CheckDoprinosi => bool.Parse(CheckBoxDoprinosi.IsChecked.ToString());


        private bool ProvjeriPrirez()
        {
            var prirez = from p in Popisprireza
                         where p.Equals(Prirez)
                         select p;
            var postoji = prirez.Any();
            return postoji;
        }
        private void BtnIzracun_Click(object sender, RoutedEventArgs e)
        {
            if (!ProvjeriPrirez() || string.IsNullOrEmpty(TxtBruto.Text))
            {
                var metro = (MetroWindow)Application.Current.MainWindow;
                metro.ShowMessageAsync("Upisali ste prirez koji nije na listi ili nema iznosa u brutu.", "Provjerite!");
                return;
            }
            var odabranbruto = ProvjeriRadioGumb();

            if (odabranbruto) ProcesuirajBruto();
            else
            {
                TxtBruto.Text = "";
                if (string.IsNullOrEmpty(TxtNeto.Text)) return;
                var neto = new ProcesuirajNeto(Neto, Olaksica, Prirez, CheckDoprinosi);
                neto.Izracunaj();
                TxtBruto.Text = neto.Bruto.ToString(new CultureInfo("hr-HR"));
                ProcesuirajBruto();
            }
        }
        private bool ProvjeriRadioGumb()
        {
            return RbBruto.IsChecked != false;
        }
        private void ProcesuirajBruto()
        {
            var upisanineto = Neto;
            var bruto = Bruto;
            if (string.IsNullOrEmpty(TxtBruto.Text)) return;
            var placa = new ProcesuirajPlacu(Bruto, Prirez, Stup1I2,CheckDoprinosi, Olaksica);
            placa.Izracun();
            if (RbNeto.IsChecked == true && VratiNeto(placa, upisanineto, bruto)) return;
            PopuniVrijednosti(placa);
            CmbPrijevoz_SelectionChanged(this, null);
        }

        private void PopuniVrijednosti(ProcesuirajPlacu placa)
        {
            TxtNeto.Text = placa.Neto.ToString(new CultureInfo("hr-HR"));
            Lbl1Stup.Content = placa.PetnaestPostoDoprinos.ToString("C");
            Lbl2Stup.Content = placa.PetPostoDoprinos.ToString("C");
            LblDopUkupno.Content = placa.DoprinosiIzPlaceUkupno.ToString("C");
            LblBruto.Content = placa.Bruto.ToString("C");
            LblDohodak.Content = placa.Dohodak.ToString("C");
            LblOdbitak.Content = placa.Olaksica.ToString("C");
            LblPorOsnovica.Content = placa.PoreznaOsnovica.ToString("C");
            LblPorezUkupno.Content = placa.UkupniPorez.ToString("C");
            LblPorez25.Content = placa.PorezDvadesetCetiriPosto.ToString("C");
            LblPorez40.Content = placa.PorezTridesetSestPosto.ToString("C");
            LblPrirez.Content = placa.Prirez.ToString("C");
            LblZdravstveno.Content = placa.DoprinosZaZdravstveno.ToString("C");
            LblZnr.Content = placa.DoprinosZaZnr.ToString("C");
            LblZaposljavanje.Content = placa.DoprinosZaZaposljavanje.ToString("C");
            LblDoprinosiUkupno.Content = placa.DoprinosNaPlacUkupno.ToString("C");
            LblTrosakPlace.Content = placa.UkupniTrosakPlace.ToString("C");
            Listica = placa;
        }

        private bool VratiNeto(ProcesuirajPlacu placa, decimal upisanineto, decimal bruto)
        {
            if (placa.Neto < upisanineto)
            {
                bruto += 0.01m;
                PonovoProcesuirajBruto(bruto, Prirez, Stup1I2, CheckDoprinosi, Olaksica);
                return true;
            }
            else if (placa.Neto > upisanineto)
            {
                bruto -= 0.01m;
                PonovoProcesuirajBruto(bruto, Prirez, Stup1I2, CheckDoprinosi, Olaksica);
                return true;
            }
            return false;
        }

        private void PonovoProcesuirajBruto(decimal upisanineto, decimal prirez, bool stup1I2, bool checkDoprinosi, decimal olaksica)
        {
            TxtBruto.Text = upisanineto.ToString(new CultureInfo("hr-HR"));
            var placu = new ProcesuirajPlacu(upisanineto,prirez,stup1I2,checkDoprinosi,olaksica);
            placu.Izracun();
             PopuniVrijednosti(placu);
        }


        private void BtnOcisti_Clic(object sender, RoutedEventArgs e)
        {
            TxtBruto.Text = "0,00";
            TxtNeto.Text = "0,00";
            OcistiLabele();
            CmbPrijevoz.SelectedIndex = 0;
        }

        private void OcistiLabele()
        {
            foreach (var labela in StPanel2.Children.OfType<Label>())
            {
                labela.Content = $"{0.00:C2}";
            }
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            UkljuciGumb();
            TxtBruto.Focus();
            TabKontrola.SelectedIndex += 1;
            CmbPrijevoz.ItemsSource = Prijevoz.ListaStanica();
            CmbPrijevoz.SelectedIndex = 0;
        }
        private void UkljuciGumb()
        {
            RbBruto.IsChecked = true;
        }
        private void RbBruto_Checked(object sender, RoutedEventArgs e)
        {
            OcistiLabele();
            CmbPrijevoz.IsEnabled = true;
            TxtBruto.IsEnabled = true;
            TxtBruto.Focus();
            TxtNeto.IsEnabled = false;
            TxtNeto.Text = "0,00";
        }
        private void RbNeto_Checked(object sender, RoutedEventArgs e)
        {
            BtnOcisti_Clic(this,null);
            CmbPrijevoz.IsEnabled = false;
            TxtBruto.IsEnabled = false;
            TxtNeto.IsEnabled = true;
            TxtNeto.Text = string.Empty;
            TxtNeto.Focus();
            TxtBruto.Text = "0,00";
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
        }

        private void CmbPrijevoz_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var iznos = Math.Round(Prijevoz.VratiIznosPrijevoza(CmbPrijevoz.SelectedItem.ToString()), 2);
            iznos += Neto;
            LblPrijevoz.Content = iznos.ToString("C", new CultureInfo("hr-HR"));
        }
    }
}