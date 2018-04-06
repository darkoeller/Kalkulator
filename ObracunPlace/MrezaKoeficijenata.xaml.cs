using System.Windows;
using System.Windows.Data;
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
            var collectionView = CollectionViewSource.GetDefaultView(Koeficijenti.VratiSifre());
            collectionView.GroupDescriptions.Add(new PropertyGroupDescription("Sifra"));
            KoeficijentiDataGrid.DataContext = collectionView;
        }
    }
}