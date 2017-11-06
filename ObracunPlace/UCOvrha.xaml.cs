using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using BiznisSloj;

namespace ObracunPlace
{
    /// <summary>
    ///     Interaction logic for UCOvrha.xaml
    /// </summary>
    public partial class UCOvrha : UserControl
    {
      private decimal _neto;
        public UCOvrha()
        {
            InitializeComponent();
            TextBoxNeto.Focus();
        }


        private void IzracunajNeto_Click(object sender, RoutedEventArgs e)
        {
          if (string.IsNullOrEmpty(TextBoxNeto.Text)) return;
          var neto = decimal.TryParse(TextBoxNeto.Text, out _neto);
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
    }
}