using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using drakek.Controller;
using System.Windows;
using drakek.Model;
using Drakek.Controller;

namespace drakek.ViewModel
{
    public partial class ProductView: UserControl
    {
        private SupportFunctions supportFunctions = new SupportFunctions();
        private PeopleController peopleController = new PeopleController();
        private ProductController productController = new ProductController();
        public ProductView()
        {
            InitializeComponent();
            showProductPanel();
            ProductUpdateForm.productView = this;
        }

        private void addProductButtonClick(object sender, RoutedEventArgs e)
        {
            ProductUpdateForm.id = "";
            ProductUpdateForm.showForm();
        }

        private void editProductButtonClick(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Product product = productController.getProduct(context.id);

            if(product == null){
                MessageBox.Show("Product not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ProductUpdateForm.id = product.id;
            ProductUpdateForm.showForm();
        }

        private void deleteProductButtonClick(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Product product = productController.getProduct(context.id);

            if(product == null){
                MessageBox.Show("Product not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ProductUpdateForm.id = product.id;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                productController.deleteProduct(product.id);
                showProductPanel();
            }
        }
        public void showProductPanel()
        {   
            if (!checkAccessPermission()){
                supportFunctions.mainWindow.show403Page();
                return;
            }
            List<Product> products = productController.getAllProducts();
            var productsData = products.Select((product, i) => new
            {
                index = i + 1,
                product.id,
                product.name,
                product.price
            }).ToList();
            ProductTable.ItemsSource = productsData;
            ProductViewPanel.Visibility = Visibility.Visible;
        }

        public void closeProductPanel()
        {
            ProductViewPanel.Visibility = Visibility.Collapsed;
        }

        public bool checkAccessPermission()
        {
            return peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_all") == true
                || peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_product") == true;
        }
    }
}