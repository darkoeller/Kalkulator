using System.Windows;
using System.Windows.Input;

namespace ObracunPlace
{
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        

        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            TabKontrola.SelectedIndex += 1;
        }
        private void LblZatvori_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

    }
}