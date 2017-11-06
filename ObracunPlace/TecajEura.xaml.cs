using BiznisSloj.BankarskiTecaj;
using System;
using System.Globalization;
using System.Windows.Controls;
using PostSharp.Patterns.Threading;

namespace ObracunPlace
{
  /// <summary>
  /// Interaction logic for TecajEura.xaml
  /// </summary>
  public partial class TecajEura : UserControl
  {
    private decimal _euro;

    public TecajEura()
    {
      InitializeComponent();
      CmbBanke.SelectedIndex = 0;
      TxtDatum.Text = DateTime.Now.ToShortDateString();
    }

    [Background]
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
      }
    }

    private void BtnIzracun_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (string.IsNullOrEmpty(TxtEuro.Text)) return;
      var prolaz = decimal.TryParse(TxtEuro.Text, out decimal rata);
      rata = Math.Round(d: _euro * rata, decimals: 2);
      LblEuri.Content = rata.ToString(CultureInfo.CurrentCulture);
    }
  }
}
