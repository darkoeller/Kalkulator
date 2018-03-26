using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BiznisSloj;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ObracunPlace
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static ProcesuirajPlacu _listica;

        private static readonly IEnumerable<decimal> Popisprireza = new List<decimal>
        {
            0,
            1m,
            2m,
            3m,
            4m,
            5m,
            6m,
            6.25m,
            6.5m,
            7m,
            7.5m,
            8m,
            9m,
            10m,
            11m,
            12m,
            13m,
            14m,
            15m,
            18m
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private decimal Olaksica => decimal.Parse(OlaksicaUpDown.Value.ToString());
        private decimal Prirez => decimal.Parse(PrirezUpDown.Value.ToString());
        private bool Stup1I2 => bool.Parse(Rb1I2Stup.IsChecked.ToString());

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

        private bool ProvjeriPrirez()
        {
            var prirez = from p in Popisprireza where p.Equals(Prirez) select p;
            var postoji = prirez.Any();
            return postoji;
        }

        private void BtnIzracun_Click(object sender, RoutedEventArgs e)
        {
            if (!ProvjeriPrirez() || string.IsNullOrEmpty(TxtBruto.Text))
            {
                var metro = (MetroWindow) Application.Current.MainWindow;
                metro.ShowMessageAsync("Upisali ste prirez koji nije na listi ili nema iznosa u brutu/netu.",
                    "Provjerite!");
                return;
            }

            if (ProvjeriRadioGumb())
            {
                ProcesuirajBruto();
            }
            else
            {
                TxtBruto.Text = "";
                if (string.IsNullOrEmpty(TxtNeto.Text)) return;
                var neto = new ProcesuirajNeto(GetNeto(), Olaksica, Prirez);
                neto.Izracunaj();
                TxtBruto.Text = neto.Bruto.ToString(new CultureInfo("hr-HR"));
                ProcesuirajBruto();
            }
        }

        private void OduzmiOdbitke()
        {
            if (string.IsNullOrEmpty(TxtBoxOdbici.Value.ToString())) return;
            var neto = GetNeto();
            var odbici = decimal.Parse(TxtBoxOdbici.Value.ToString());
            var stringPrijevoz = LblPrijevoz.Content.ToString();
            stringPrijevoz = stringPrijevoz.Substring(0, stringPrijevoz.Length - 2);
            var prijevoz = decimal.Parse(stringPrijevoz);

            if (CmbPrijevoz.SelectedIndex == 0)
            {
                neto -= odbici;
                LblOdbici.Content = neto.ToString("C");
            }
            else if (CmbPrijevoz.SelectedIndex > 0)
            {
                VratiTotal(prijevoz, odbici);
            }
        }

        private void VratiTotal(decimal prijevoz, decimal odbici)
        {
            prijevoz -= odbici;
            LblOdbici.Content = prijevoz.ToString("C");
        }

        private bool ProvjeriRadioGumb()
        {
            return RbBruto.IsChecked != false;
        }

        private void ProcesuirajBruto()
        {
            var neto = GetNeto();
            var bruto = GetBruto();
            if (string.IsNullOrEmpty(TxtBruto.Text)) return;
            var placa = new ProcesuirajPlacu(bruto, Prirez, Stup1I2, Olaksica);
            placa.Izracun();
            if (RbNeto.IsChecked != true)
            {
                PopuniVrijednosti(placa);
                CmbPrijevoz_SelectionChanged(this, null);
                OduzmiOdbitke();
                return;
            }
            UsporediNeto(neto, placa);
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
            _listica = placa;
        }

        private void UsporediNeto(decimal neto, ProcesuirajPlacu placa)
        {
            var izracunatiNeto = placa.Neto;
            var izracunatiBruto = placa.Bruto;
            if (izracunatiNeto < neto)
            {
                izracunatiBruto += 0.01m;
                PonovoProcesuirajBruto(izracunatiBruto, Prirez, Stup1I2, Olaksica);
                return;
            }
            if (izracunatiNeto > neto)
            {
                izracunatiBruto -= 0.01m;
                PonovoProcesuirajBruto(izracunatiBruto, Prirez, Stup1I2, Olaksica);
            }
            else if (izracunatiNeto == neto)
            {
                PopuniVrijednosti(placa);
            }
        }

        private void PonovoProcesuirajBruto(decimal upisanineto, decimal prirez, bool stup1I2, decimal olaksica)
        {
            TxtBruto.Text = upisanineto.ToString(new CultureInfo("hr-HR"));
            var placa = new ProcesuirajPlacu(upisanineto, prirez, stup1I2, olaksica);
            placa.Izracun();
            PopuniVrijednosti(placa);
        }

        private void BtnOcisti_Clic(object sender, RoutedEventArgs e)
        {
            TxtBruto.Text = "0,00";
            TxtNeto.Text = "0,00";
            OcistiLabele();
            TxtBoxOdbici.Value = 0.00;
            CmbPrijevoz.SelectedIndex = 0;
        }

        private void OcistiLabele()
        {
            foreach (var labela in StPanel2.Children.OfType<Label>()) labela.Content = $"{0.00:C2}";
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

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
        }

        private void CmbPrijevoz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var iznos = Math.Round(Prijevoz.VratiIznosPrijevoza(CmbPrijevoz.SelectedItem.ToString()), 2);
            iznos += GetNeto();
            LblPrijevoz.Content = iznos.ToString("C", new CultureInfo("hr-HR"));
            var odbitak = decimal.Parse(TxtBoxOdbici.Value.ToString());
            VratiTotal(iznos, odbitak);
        }

        private void LblZatvori_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}