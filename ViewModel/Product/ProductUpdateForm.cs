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
        PeopleController peopleController = new PeopleController();
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
            if(!updateValidated()) return; 
            Product productToUpdate = new Product(){
                id = id,
                name = ProductName.Text,
                price = int.TryParse(ProductPrice.Text, out int p) ? p : 0,
            };
            productController.updateProduct(productToUpdate);
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

        private bool updateValidated(){
            bool canUpdate = true;
            People currentUser = supportFunctions.currentUser();

            if(!peopleController.checkPeoplePermission(currentUser, "update_product")){
                canUpdate = false;
                ValidateMessage.Text = "You don't have permission to update";
            };
            if(peopleController.checkPeoplePermission(currentUser, "update_all") == true){
                canUpdate = true;
                ValidateMessage.Text = "";
            }
            if(string.IsNullOrEmpty(ProductName.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Name cannot be empty";
            }
            if(string.IsNullOrEmpty(ProductPrice.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Price cannot be empty";
            }

            return canUpdate;
        }
    }
}