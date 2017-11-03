using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BiznisSloj;
using BiznisSloj.KoefSati;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PostSharp.Patterns.Model;

namespace ObracunPlace
{
  /// <summary>
  ///     Interaction logic for UCObracun.xaml
  /// </summary>

  [NotifyPropertyChanged]
  public sealed partial class UcObracun 
    {
        // Using a DependencyProperty as the backing store for Bruto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrutoProperty =
            DependencyProperty.Register("Bruto", typeof(decimal), typeof(UcObracun));

        private decimal _koeficijent;

        public UcObracun()
        {
            InitializeComponent();
            var svesifre = Koeficijenti.VratiSifre();
            var sifre = svesifre.Select(s => s.Naziv);
            ChComboBoxVrsteRada.ItemsSource = sifre;
            BodoviUpDown.Focus();
            PozoviLabelu();
            ChComboBoxVrsteRada.SelectedIndex = 0;
        }
        public decimal Bruto
        {
            private get { return (decimal) GetValue(BrutoProperty); }
            set { SetValue(BrutoProperty, value); }
        }


        private decimal Bodovi => decimal.Parse(BodoviUpDown.Value.ToString());
        private decimal Minuli => decimal.Parse(MinuliUpDown.Value.ToString());
        private decimal BrojSati => decimal.Parse(SatiRadaUpDown.Value.ToString());
        private decimal GodineStaza => decimal.Parse(GodineUpDown.Value.ToString());


        private void ChComboBoxVrsteRada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vrstarada = ChComboBoxVrsteRada.SelectedItem.ToString();
            _koeficijent = Koeficijenti.VratiIznos(vrstarada);
            StatusLbl.Content = "promjena vrste rada";
    }

        private void BtnIzracun_Click(object sender, RoutedEventArgs e)
        {
            if (ChComboBoxVrsteRada == null || ChComboBoxVrsteRada.SelectedIndex == -1) return;
            var text = ChComboBoxVrsteRada.SelectedItem.ToString();
            var izracun = new IzracunajKoeficijentSate(BrojSati, Minuli, Bodovi, _koeficijent);
            var rezultat = izracun.Izracun();
            Bruto += rezultat;
            ListBoxBruto.Items.Add($"{text}: {rezultat}");
            PozoviLabelu();
           StatusLbl.Content = "izračun satnice";
        }

        private void PozoviLabelu()
        {
            LblBruto.Content = "Ukupno : " + Bruto.ToString("c");
        }

        private void BtnOcisti_Click(object sender, RoutedEventArgs e)
        {
            ListBoxBruto.Items.Clear();
            Bruto = 0.0m;
            PozoviLabelu();
            StatusLbl.Content = "očišćeno !";
        }

        private void BtnMinuli_Click(object sender, RoutedEventArgs e)
        {
            var minuli = new MinuliRad(BrojSati, Minuli);
            var izracun = minuli.Izracun();
            Bruto += izracun;
            ListBoxBruto.Items.Add("Minuli rad: " + izracun.ToString(new CultureInfo("hr-HR")));
            PozoviLabelu();
            StatusLbl.Content = "izračun minulog rada";
        }


        private void BtnDodatak_Click(object sender, RoutedEventArgs e)
        {
            var dodatak = new DodatakNaPlacu(BrojSati);
            var izracun = dodatak.Izracun();
            Bruto += izracun;
            ListBoxBruto.Items.Add("Dodatak na plaću: " + izracun.ToString(new CultureInfo("hr-HR")));
            PozoviLabelu();
            StatusLbl.Content = "izračun dodatka";
    }

        private void BtnOcistiOdabrano_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxBruto.SelectedIndex == -1)
            {
                var metro = (MetroWindow) Application.Current.MainWindow;
                metro.ShowMessageAsync("Lista je prazna ili niste ništa odabrali za brisanje.", "Provjerite!");
            }
            else
            {
                var razlika = ListBoxBruto.SelectedItem.ToString();
                var broj = PronadjiDecimalniBroj(razlika);
                Bruto -= broj;
                ListBoxBruto.Items.RemoveAt(ListBoxBruto.SelectedIndex);
                PozoviLabelu();
               StatusLbl.Content = "obrisan red";
            }
        }

        private void BtnRacunaMinuli_Click(object sender, RoutedEventArgs e)
        {
            var minuli = new IzracunGodinaStaza(GodineStaza, Bodovi);
            var rezultat = minuli.Izracun();
            LblMinuli.Content = "Vaš minuli iznosi : " + rezultat.ToString(new CultureInfo("hr-HR"));
            Bruto += rezultat;
            StatusLbl.Content = "izračun minulog";
        }

        private static decimal PronadjiDecimalniBroj(string razlika)
        {
            var zadnje = razlika.Split(' ').Last();
            var broj = decimal.Parse(zadnje);
            return broj;
        }
    private void BodoviUpDown_ValueDecremented(object sender, NumericUpDownChangedRoutedEventArgs args)
    {
      StatusLbl.Content = "smanjujem bodove";
    }
    private void MinuliUpDown_ValueDecremented(object sender, NumericUpDownChangedRoutedEventArgs args)
    {
      StatusLbl.Content = "smanjujem minuli";
    }
    private void BodoviUpDown_ValueIncremented(object sender, NumericUpDownChangedRoutedEventArgs args)
    {
      StatusLbl.Content = "povećavam bodove";
    }
    private void MinuliUpDown_ValueIncremented(object sender, NumericUpDownChangedRoutedEventArgs args)
    {
      StatusLbl.Content = "povećavam minuli";
    }
    private void SatiRadaUpDown_ValueDecremented(object sender, NumericUpDownChangedRoutedEventArgs args)
    {
      StatusLbl.Content = "smanjujem sate rada";
    }

    private void SatiRadaUpDown_ValueIncremented(object sender, NumericUpDownChangedRoutedEventArgs args)
    {
      StatusLbl.Content = "povećavam sate rada";
    }
  }
}