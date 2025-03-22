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
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;

namespace drakek.ViewModel
{
    public partial class CustomerUpdateForm: UserControl
    {
        public string id{get; set;}
        private Customer customerToUpdate = new Customer();
        private CustomerController customerController = new CustomerController();
        private SupportFunctions supportFunctions = new SupportFunctions();
        private PeopleController peopleController = new PeopleController();
        public CustomerView customerView;
        public CustomerUpdateForm()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void showForm(){
            customerToUpdate = customerController.getCustomer(id);
            if(customerToUpdate != null){
                CustomerName.Text = customerToUpdate.name;
                CustomerPhone.Text = customerToUpdate.phone;
                CustomerAddress.Text = customerToUpdate.address;
                CustomerCity.Text = customerToUpdate.city;
                CustomerDistrict.Text = customerToUpdate.district;
                CustomerWard.Text = customerToUpdate.ward;
            }    
            
            Visibility = Visibility.Visible;
            customerView.closeCustomerPanel();
        }
        public void closeForm(){
            id="";
            clearForm();
            Visibility = Visibility.Collapsed;
            customerView.showCustomerPanel();
        }
        private void clearForm()
        {
            CustomerName.Text = "";
            CustomerPhone.Text = "";
            CustomerAddress.Text = "";
            CustomerCity.Text = "";
            CustomerDistrict.Text = "";
            CustomerWard.Text = "";
        }

        private void saveCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            if(!updateValidated()) return;
            customerToUpdate.name = CustomerName.Text;
            customerToUpdate.phone = CustomerPhone.Text;
            customerToUpdate.address = CustomerAddress.Text;
            customerToUpdate.city = CustomerCity.Text;
            customerToUpdate.district = CustomerDistrict.Text;
            customerToUpdate.ward = CustomerWard.Text;
            customerController.updateCustomer(customerToUpdate);

            closeForm();
        }

        private void cancelUpdateCustomerButton_Click(object sender, RoutedEventArgs e)
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

            if(!peopleController.checkPeoplePermission(currentUser, "update_customer")){
                canUpdate = false;
                ValidateMessage.Text = "You don't have permission to update";
            };
            if(peopleController.checkPeoplePermission(currentUser, "update_all") == true){
                canUpdate = true;
                ValidateMessage.Text = "";
            }
            if(string.IsNullOrEmpty(CustomerName.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Name cannot be empty";
            }
            if(string.IsNullOrEmpty(CustomerPhone.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Phone cannot be empty";
            }
            if(string.IsNullOrEmpty(CustomerCity.Text)){
                canUpdate = false;
                ValidateMessage.Text = "City cannot be empty";
            }
            if(string.IsNullOrEmpty(CustomerDistrict.Text)){
                canUpdate = false;
                ValidateMessage.Text = "District cannot be empty";
            }
            if(string.IsNullOrEmpty(CustomerWard.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Ward cannot be empty";
            }

            return canUpdate;
        }
    }
}