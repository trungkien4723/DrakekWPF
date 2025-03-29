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
        public List<Product> getAllProducts(Dictionary<string, string> filters = null){
            var productsData = new List<Product>();
            try
            {
                using (var context = new DrakekDB())
                {
                    var query = context.product.AsQueryable();
                    if(filters != null){
                        IQueryable<Product> orQuery = context.product.Where(c => false);
                        foreach (var filter in filters)
                        {
                            string key = filter.Key.ToLower();
                            string value = filter.Value.ToLower();
                            switch (key){
                                case "name":
                                    orQuery = orQuery.Union(context.product.Where(p => p.name != null && p.name.ToLower().Contains(value)));
                                    break;
                            }
                        }

                        query = orQuery;

                    }
                    productsData = query.ToList();
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

        public void updateProduct(Product productToUpdate)
        {   
            try
            {
                using (var context = new DrakekDB())
                {
                    if(!string.IsNullOrEmpty(productToUpdate.id)){
                        Product product = context.product.Where(p => p.id == productToUpdate.id).FirstOrDefault();
                        product.name = productToUpdate.name;
                        product.price = productToUpdate.price;
                        context.SaveChanges();
                    }
                    else{
                        productToUpdate.id = supportFunctions.generateID("prdct", 5);
                        productToUpdate.createdDate = DateTime.Now;
                        context.product.Add(productToUpdate);
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