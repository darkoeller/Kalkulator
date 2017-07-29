using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BiznisSloj;
using BiznisSloj.KoefSati;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ObracunPlace
{
    /// <summary>
    ///     Interaction logic for UCObracun.xaml
    /// </summary>
    public sealed partial class UcObracun : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void ChComboBoxVrsteRada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vrstarada = ChComboBoxVrsteRada.SelectedItem.ToString();
            _koeficijent = Koeficijenti.VratiIznos(vrstarada);
        }

        private void BtnIzracun_Click(object sender, RoutedEventArgs e)
        {
            if (ChComboBoxVrsteRada == null || ChComboBoxVrsteRada.SelectedIndex == -1) return;
            var text = ChComboBoxVrsteRada.SelectedItem.ToString();
            var izracun = new IzracunajKoeficijentSate(BrojSati, Minuli, Bodovi, _koeficijent);
            var rezultat = izracun.Izracun();
            Bruto += rezultat;
            OnPropertyChanged(Bruto);
            ListBoxBruto.Items.Add($"{text}: {rezultat}");
            PozoviLabelu();
        }

        private void PozoviLabelu()
        {
            LblBruto.Content = "Ukupno : " + Bruto.ToString("c");
        }

        private void BtnOcisti_Click(object sender, RoutedEventArgs e)
        {
            ListBoxBruto.Items.Clear();
            Bruto = 0.0m;
            OnPropertyChanged(Bruto);
            PozoviLabelu();
        }

        private void BtnMinuli_Click(object sender, RoutedEventArgs e)
        {
            var minuli = new MinuliRad(BrojSati, Minuli);
            var izracun = minuli.Izracun();
            Bruto += izracun;
            OnPropertyChanged(Bruto);
            ListBoxBruto.Items.Add("Minuli rad: " + izracun.ToString(new CultureInfo("hr-HR")));
            PozoviLabelu();
        }


        private void BtnDodatak_Click(object sender, RoutedEventArgs e)
        {
            var dodatak = new DodatakNaPlacu(BrojSati);
            var izracun = dodatak.Izracun();
            Bruto += izracun;
            OnPropertyChanged(Bruto);
            ListBoxBruto.Items.Add("Dodatak na plaću: " + izracun.ToString(new CultureInfo("hr-HR")));
            PozoviLabelu();
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
                OnPropertyChanged(Bruto);
                ListBoxBruto.Items.RemoveAt(ListBoxBruto.SelectedIndex);
                PozoviLabelu();
            }
        }

        private void BtnRacunaMinuli_Click(object sender, RoutedEventArgs e)
        {
            var minuli = new IzracunGodinaStaza(GodineStaza, Bodovi);
            var rezultat = minuli.Izracun();
            LblMinuli.Content = "Vaš minuli iznosi : " + rezultat.ToString(new CultureInfo("hr-HR"));
            Bruto += rezultat;
            OnPropertyChanged(Bruto);
        }

        private void OnPropertyChanged(decimal propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName.ToString(new CultureInfo("HR-hr"))));
        }

        private static decimal PronadjiDecimalniBroj(string razlika)
        {
            var zadnje = razlika.Split(' ').Last();
            var broj = decimal.Parse(zadnje);
            return broj;
        }
    }
}