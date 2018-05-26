using System.Windows;
using System.Windows.Threading;

namespace ObracunPlace
{
    /// <summary>
    ///   Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var friendlyMsg = $"Pa nešto je pošlo po zlu! Greška je u: [{e.Exception.Message}]";
            var caption = "Pogreška!";
            MessageBox.Show(friendlyMsg, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}