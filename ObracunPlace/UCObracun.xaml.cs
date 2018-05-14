using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BiznisSloj;
using BiznisSloj.KoefSati;

namespace ObracunPlace
{
    /// <summary>
    ///   Interaction logic for UCObracun.xaml
    /// </summary>
    public sealed partial class UcObracun
    {
        // Using a DependencyProperty as the backing store for Bruto.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BrutoProperty =
            DependencyProperty.Register("Bruto", typeof(decimal), typeof(UcObracun));

        private decimal _koeficijent;

        public UcObracun()
        {
            InitializeComponent();

            var svesifre = Koeficijenti2.VratiSifre();
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


        private decimal GetBodovi()
        {
            decimal.TryParse(BodoviUpDown.Text, out var bodovi);
            return bodovi;
        }

        private decimal GetMinuli()
        {
            decimal.TryParse(MinuliUpDown.Text, out var minuli);
            return minuli;
        }

        private decimal GetSatiRada()
        {
            decimal.TryParse(SatiRadaUpDown.Text, out var satirada);
            return satirada;
        }

        private decimal GetGodineStaza()
        {
            decimal.TryParse(GodineUpDown.Text, out var godine);
            return godine;
        }

        private void ChComboBoxVrsteRada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vrstarada = ChComboBoxVrsteRada.SelectedItem.ToString();
            _koeficijent = Koeficijenti2.VratiIznos(vrstarada);
        }

        private void BtnIzracun_Click(object sender, RoutedEventArgs e)
        {
            if (ChComboBoxVrsteRada == null || ChComboBoxVrsteRada.SelectedIndex == -1) return;
            var text = ChComboBoxVrsteRada.SelectedItem.ToString();
            var izracun = new IzracunajKoeficijentSate(GetSatiRada(), GetMinuli(), GetBodovi(), _koeficijent).Izracun();
            Bruto += izracun;
            ListBoxBruto.Items.Add($"{text}: {izracun}");
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
            PozoviLabelu();
        }

        private void BtnMinuli_Click(object sender, RoutedEventArgs e)
        {
            var minuli = new MinuliRad(GetSatiRada(), GetMinuli());
            var izracun = minuli.Izracun();
            Bruto += izracun;
            ListBoxBruto.Items.Add("Minuli rad: " + izracun.ToString(new CultureInfo("hr-HR")));
            PozoviLabelu();
        }


        private void BtnDodatak_Click(object sender, RoutedEventArgs e)
        {
            var dodatak = new DodatakNaPlacu(GetSatiRada());
            var izracun = dodatak.Izracun();
            Bruto += izracun;
            ListBoxBruto.Items.Add("Dodatak na plaću: " + izracun.ToString(new CultureInfo("hr-HR")));
            PozoviLabelu();
        }

        private void BtnOcistiOdabrano_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxBruto.SelectedIndex == -1)
            {
                MessageBox.Show("Lista je prazna ili niste ništa odabrali za brisanje.", "Provjerite!");
            }
            else
            {
                var razlika = ListBoxBruto.SelectedItem.ToString();
                var broj = PronadjiDecimalniBroj(razlika);
                Bruto -= broj;
                ListBoxBruto.Items.RemoveAt(ListBoxBruto.SelectedIndex);
                PozoviLabelu();
            }
        }

        private void BtnRacunaMinuli_Click(object sender, RoutedEventArgs e)
        {
            var minuli = new IzracunGodinaStaza(GetGodineStaza(), GetBodovi());
            var rezultat = minuli.Izracun();
            LblMinuli.Content = "Vaš minuli iznosi : " + rezultat.ToString(new CultureInfo("hr-HR"));
            Bruto += rezultat;
        }

        private static decimal PronadjiDecimalniBroj(string razlika)
        {
            var zadnje = razlika.Split(' ').Last();
            var broj = decimal.Parse(zadnje);
            return broj;
        }
    }
}