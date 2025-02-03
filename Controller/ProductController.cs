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
using drakek.Model;

namespace drakek.Controller
{
    public partial class Product : UserControl
    {
        public Product()
        {
            InitializeComponent();
            showProduct();
        }

        private void showProduct(){
            List<Products> products = new List<Products>{
                new Products {id = "1", name = "Product 1", price = 10000},
                new Products {id = "2", name = "Product 2", price = 20000},
            };

            List<object> productsData = new List<object>();
            for(int i = 0; i < products.Count; i++){
                productsData.Add(new {
                    index = i + 1,
                    id = products[i].id,
                    name = products[i].name,
                    price = products[i].price
                });
            }

            ProductTable.ItemsSource = productsData;
        }
    }
}