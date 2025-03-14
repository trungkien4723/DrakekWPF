using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using drakek.Controller;
using System.Windows;
using drakek.Model;

namespace drakek.ViewModel
{
    public partial class CustomerView: UserControl
    {
        private CustomerController customerController = new CustomerController();
        public CustomerView()
        {
            InitializeComponent();
            showCustomerPanel();
            CustomerUpdateForm.customerView = this;
        }

        private void addCustomerButtonClick(object sender, RoutedEventArgs e)
        {
            showUpdateCustomerForm("");
        }

        private void editCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Customer customer = customerController.getCustomer(context.id);

            if(customer == null){
                MessageBox.Show("Not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            showUpdateCustomerForm(customer.id);
        }

        private void deleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Customer customer = customerController.getCustomer(context.id);

            if(customer == null){
                MessageBox.Show("Not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                customerController.deleteCustomer(customer.id);
                showCustomerPanel();
            }
        }
        public void showCustomerPanel()
        {   
            List<Customer> allCustomer = customerController.getAllCustomers();
            var allCustomerData = allCustomer.Select((customer, i) => new
            {
                index = i + 1,
                customer.id,
                customer.name,
                customer.phone,
                customer.city,
                createdDate = customer.createdDate.ToString("d"),
            }).ToList();
            CustomerTable.ItemsSource = allCustomerData;
            CustomerViewPanel.Visibility = Visibility.Visible;
        }

        public void closeCustomerPanel()
        {
            CustomerViewPanel.Visibility = Visibility.Collapsed;
        }

        public void showUpdateCustomerForm(string updateCustomerId){
            CustomerUpdateForm.id = updateCustomerId;
            CustomerUpdateForm.showForm();
        }
    }
}