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
        
        public int ordersCount{ get; set; }
        public int productsCount{ get; set; }
        public int customersCount{ get; set; }
        private DashboardController dashboardController = new DashboardController();
        private OrderController orderController = new OrderController();
        private ProductController productController = new ProductController();
        private CustomerController customerController = new CustomerController();
        public DashboardView()
        {
            InitializeComponent();
            DataContext = this;
            ordersCount = orderController.getAllOrders().Count;
            productsCount = productController.getAllProducts().Count;
            customersCount = customerController.getAllCustomers().Count;
        }
    }
}