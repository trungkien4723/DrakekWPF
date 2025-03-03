using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using drakek.Controller;
using System.Windows;
using drakek.Model;
using Drakek.Controller;
using System.Windows.Input;

namespace drakek.ViewModel
{
    public partial class ProductUpdateForm: UserControl
    {
        public string id{get; set;}

        private ProductController productController = new ProductController();
        private SupportFunctions supportFunctions = new SupportFunctions();
        public ProductView productView;
        public ProductUpdateForm()
        {
            InitializeComponent();
        }

        public void showForm(){
            Product product = productController.getProduct(id);
            if(product != null){
                ProductName.Text = product.name;
                ProductPrice.Text = product.price.ToString();
            }
            Visibility = Visibility.Visible;
            productView.closeProductPanel();
        }
        public void closeForm(){
            id="";
            clearForm();
            Visibility = Visibility.Collapsed;
            productView.showProductPanel();
        }
        private void clearForm()
        {
            ProductName.Text = "";
            ProductPrice.Text = "";
        }

        private void saveProductButtonClick(object sender, RoutedEventArgs e)
        {
            string name = ProductName.Text;
            int price = int.Parse(ProductPrice.Text);

            productController.updateProduct(id, name, price);
            closeForm();
        }

        private void cancelUpdateProductButtonClick(object sender, RoutedEventArgs e)
        {
            closeForm();
        }

        private void numberInputOnlyTextbox(object sender, TextCompositionEventArgs e)
        {
            supportFunctions.previewTextInput(sender, e, "number");
        }
        private void numberPasteOnlyTextbox(object sender, DataObjectPastingEventArgs e)
        {
            supportFunctions.previewTextPasting(sender, e, "number");
        }
    }
}