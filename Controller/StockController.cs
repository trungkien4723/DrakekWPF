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
    public class StockController
    {
        public List<Stock> getAllStocks(){
            var stockData = new List<Stock>();
            try
            {
                using (var context = new DrakekDB())
                {
                    stockData = context.stock.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return stockData;
        }

        public Stock getStock(string productId, string storageId){
            Stock stock = new Stock();
            try
            {
                using (var context = new DrakekDB())
                {
                    Stock getstock = context.stock.Where(s => s.product == productId && s.storage == storageId).FirstOrDefault();
                    if(getstock != null) stock = getstock;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return stock;
        }

        public void updateStock(Stock stockToUpdate)
        {   
            try
            {
                using (var context = new DrakekDB())
                {
                    if(!string.IsNullOrEmpty(stockToUpdate.product) && !string.IsNullOrEmpty(stockToUpdate.storage)){
                        var stock = context.stock.Where(s => s.product == stockToUpdate.product && s.storage == stockToUpdate.storage).FirstOrDefault();
                        stock.quantity = stockToUpdate.quantity;
                        stock.expiredDate = stockToUpdate.expiredDate;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void deleteStock(string productId, string storageId)
        {
            try
            {
                using (var context = new DrakekDB())
                {
                    var stock = context.stock.Where(s => s.product == productId && s.storage == storageId).FirstOrDefault();
                    context.stock.Remove(stock);
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