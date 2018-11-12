using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BiznisSloj;
using BiznisSloj.KoefSati;
using ObracunPlace.Properties;
using Ninject;
using Ninject.Parameters;

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

        private readonly StandardKernel  _kernel = new StandardKernel();

        public UcObracun()
        {
            InitializeComponent();
        }

        
        private decimal Bruto
        {
            get => (decimal) GetValue(BrutoProperty);
            set
            {
                SetValue(BrutoProperty, value);
                OnPropertyChanged(value.ToString(CultureInfo.CurrentCulture).Replace('.', ','));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private decimal GetBodovi() => VratiIznos(BodoviUpDown.Text);

        private decimal GetMinuli() => VratiIznos(MinuliUpDown.Text);
        
        private decimal GetSatiRada() => VratiIznos(SatiRadaUpDown.Text);
 
        private decimal GetGodineStaza() => VratiIznos(GodineUpDown.Text);

        private static decimal VratiIznos(string text)
        {
            if (text.Contains('.')) text = text.Replace('.', ',');
            decimal.TryParse(text, out var iznos);
            return iznos;
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
            var sati = new ConstructorArgument("brojSati", GetSatiRada());
            var minuli = new ConstructorArgument("minuli", GetMinuli());
            var bodovi = new ConstructorArgument("bodovi", GetBodovi());
            var koeficijent = new ConstructorArgument("koeficijent", _koeficijent);
            var izracun = _kernel.Get<IzracunajKoeficijentSate>(sati, minuli, bodovi,koeficijent).Izracun();
            Bruto += izracun;
            ListBoxBruto.Items.Add($"{text}: {izracun}");
            PozoviLabelu();
        }

        private void PozoviLabelu() => LblBruto.Content = "Ukupno : " + Bruto.ToString("c");

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
            var dodatak = _kernel.Get<DodatakNaPlacu>(new ConstructorArgument("brojSati",GetSatiRada())).Izracun();
            Bruto += dodatak;
            ListBoxBruto.Items.Add("Dodatak na plaću: " + dodatak.ToString(new CultureInfo("hr-HR")));
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
            var godine = new ConstructorArgument("godine", GetGodineStaza());
            var bodovi = new ConstructorArgument("koeficijent", GetBodovi());
            var minuli = _kernel.Get<IzracunGodinaStaza>(godine, bodovi).Izracun();
            LblMinuli.Content = "Vaš minuli iznosi : " + minuli.ToString(new CultureInfo("hr-HR"));
            Bruto += minuli;
        }

        private static decimal PronadjiDecimalniBroj(string razlika)
        {
            var zadnje = razlika.Split(' ').Last();
            var broj = decimal.Parse(zadnje);
            return broj;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Mediator.GetInstance().OnNoviBruto(this, propertyName);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var svesifre = Koeficijenti2.VratiSifre();
            var sifre = svesifre.Select(s => s.Naziv);
            ChComboBoxVrsteRada.ItemsSource = sifre;
            PozoviLabelu();
            ChComboBoxVrsteRada.SelectedIndex = 0;
            BodoviUpDown.Focus();
            Keyboard.Focus(BodoviUpDown);
        }
    }
}