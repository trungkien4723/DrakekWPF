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
    public class ProductController
    {
        SupportFunctions supportFunctions = new SupportFunctions();
        public List<Product> getAllProducts(){
            var productsData = new List<Product>();
            try
            {
                using (var context = new DrakekDB())
                {
                    productsData = context.product.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return productsData;
        }

        public Product getProduct(string id){
            Product product = new Product();
            try
            {
                using (var context = new DrakekDB())
                {
                    product = context.product.Where(p => p.id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return product;
        }

        public void updateProduct(string id, string name, int price)
        {   
            try
            {
                using (var context = new DrakekDB())
                {
                    if(!string.IsNullOrEmpty(id)){
                        var product = context.product.Where(p => p.id == id).FirstOrDefault();
                        product.name = name;
                        product.price = price;
                        context.SaveChanges();
                    }
                    else{
                            Product newProduct = new Product
                            {
                                id = supportFunctions.generateID("prdct", 5),
                                name = name,
                                price = price,
                                createdDate = DateTime.Now
                            };

                            context.product.Add(newProduct);
                            context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void deleteProduct(string id)
        {
            try
            {
                using (var context = new DrakekDB())
                {
                    var product = context.product.Where(p => p.id == id).FirstOrDefault();
                    context.product.Remove(product);
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