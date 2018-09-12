using System.Diagnostics;
using System.Windows.Navigation;

namespace ObracunPlace
{
    /// <summary>
    ///     Interaction logic for UcPomoc.xaml
    /// </summary>
    public partial class UcPomoc
    {
        public UcPomoc()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e) => Process.Start(e.Uri.AbsoluteUri);
    }
}