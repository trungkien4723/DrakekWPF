using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using drakek.Controller;

namespace drakek.ViewModel
{
    public partial class DashboardView : UserControl
    {
        private DashboardController dashboardController = new DashboardController();
        public DashboardView()
        {
            InitializeComponent();
        }
    }
}