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
    public partial class StockView: UserControl
    {
        private StockController stockController = new StockController();
        private ProductController productController = new ProductController();
        private StorageController storageController = new StorageController();
        public StockView()
        {
            InitializeComponent();
            showStockPanel();
            OrderUpdateForm.stockView = this;
        }

        private void createStockOrderButton_Click(object sender, RoutedEventArgs e){
            showUpdateOrderForm("","buy");
        }
        public void showStockPanel()
        {   
            List<Stock> stocks = stockController.getAllStocks();
            var stocksData = stocks.Select((stock, i) => new
            {
                index = i + 1,
                product = productController.getProduct(stock.product).name,
                storage = storageController.getStorage(stock.storage).name,
                stock.quantity,
                stock.createdDate,
                stock.expiredDate
            }).ToList();
            StockTable.ItemsSource = stocksData;
            StockViewPanel.Visibility = Visibility.Visible;
        }

        public void closeStockPanel()
        {
            StockViewPanel.Visibility = Visibility.Collapsed;
        }

        public void showUpdateOrderForm(string updateOrderId, string orderType){
            OrderUpdateForm.id = updateOrderId;
            OrderUpdateForm.orderType = orderType.ToLower();
            OrderUpdateForm.showForm();
        }
    }
}