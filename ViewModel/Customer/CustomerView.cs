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
    public partial class CustomerView: UserControl
    {
        private SupportFunctions supportFunctions = new SupportFunctions();
        private PeopleController peopleController = new PeopleController();
        private CustomerController customerController = new CustomerController();
        public Dictionary<string, string> filters = new Dictionary<string, string>();
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
                showCustomerPanel(filters);
            }
        }

        private void searchCustomerButton_Click(object sender, RoutedEventArgs e)
        {
                filters = new Dictionary<string, string>
                {
                    { "name", SearchCustomer.Text },
                    { "phone", SearchCustomer.Text },
                    { "city", SearchCustomer.Text }
                };
                
                showCustomerPanel(filters);
        }

        public void showCustomerPanel(Dictionary<string, string> searchFilters = null)
        {   
            if (!checkAccessPermission()){
                supportFunctions.mainWindow.show403Page();
                return;
            }
            List<Customer> allCustomer = customerController.getAllCustomers(searchFilters).OrderByDescending(c => c.createdDate).ToList();
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
            if(CustomerViewPanel.Visibility != Visibility.Visible) CustomerViewPanel.Visibility = Visibility.Visible;
        }

        public void closeCustomerPanel()
        {
            CustomerViewPanel.Visibility = Visibility.Collapsed;
        }

        public void showUpdateCustomerForm(string updateCustomerId){
            CustomerUpdateForm.id = updateCustomerId;
            CustomerUpdateForm.showForm();
        }

        public bool checkAccessPermission()
        {
            return peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_all") == true
                || peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_customer") == true;
        }
    }
}