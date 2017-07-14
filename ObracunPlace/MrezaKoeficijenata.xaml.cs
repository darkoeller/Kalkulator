using System.Windows;
using BiznisSloj.KoefSati;

namespace ObracunPlace
{
    /// <summary>
    ///     Interaction logic for MrezaKoeficijenata.xaml
    /// </summary>
    public partial class MrezaKoeficijenata
    {
        public MrezaKoeficijenata()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            KoeficijentiDataGrid.ItemsSource = Koeficijenti.VratiSifre();
        }
    }
}