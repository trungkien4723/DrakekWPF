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
using drakek.Data;
using Drakek.Controller;

namespace drakek.Controller
{
    public class CustomerController
    {
        SupportFunctions supportFunctions = new SupportFunctions();
        public List<Customer> getAllCustomers(){
            var customersData = new List<Customer>();
            try
            {
                using (var context = new DrakekDB())
                {
                    customersData = context.customer.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return customersData;
        }

        public Customer getCustomer(string id){
            Customer customer = new Customer();
            try
            {
                using (var context = new DrakekDB())
                {
                    customer = context.customer.Where(c => c.id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return customer;
        }

        public Customer getCustomerByPhone(string phone){
            Customer customer = new Customer();
            try
            {
                using (var context = new DrakekDB())
                {
                    customer = context.customer.Where(c => c.phone == phone).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return customer;
        }

        public Customer updateCustomer(Customer customerToUpdate)
        {   
            try
            {
                using (var context = new DrakekDB())
                {
                    if(!string.IsNullOrEmpty(customerToUpdate.id)){
                        var customer = context.customer.Where(c => c.id == customerToUpdate.id).FirstOrDefault();
                        if(!string.IsNullOrEmpty(customerToUpdate.name)) customer.name = customerToUpdate.name;
                        if(!string.IsNullOrEmpty(customerToUpdate.phone)) customer.phone = customerToUpdate.phone;
                        if(!string.IsNullOrEmpty(customerToUpdate.address)) customer.address = customerToUpdate.address;
                        if(!string.IsNullOrEmpty(customerToUpdate.city)) customer.city = customerToUpdate.city;
                        if(!string.IsNullOrEmpty(customerToUpdate.district)) customer.district = customerToUpdate.district;
                        if(!string.IsNullOrEmpty(customerToUpdate.ward)) customer.ward = customerToUpdate.ward;
                        context.SaveChanges();
                        return customer;
                    }
                    else{
                        customerToUpdate.id = supportFunctions.generateID("cstm", 5);
                        customerToUpdate.createdDate = DateTime.Now;
                        context.customer.Add(customerToUpdate);
                        context.SaveChanges();
                        return customerToUpdate;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void deleteCustomer(string id)
        {
            try
            {
                using (var context = new DrakekDB())
                {
                    var customer = context.customer.Where(c => c.id == id).FirstOrDefault();
                    context.customer.Remove(customer);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}