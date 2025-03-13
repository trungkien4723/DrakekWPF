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
        public StockView()
        {
            InitializeComponent();
            showStockPanel();
        }

        private void createStockOrderButton_Click(object sender, RoutedEventArgs e){
        }
        public void showStockPanel()
        {   
            List<Stock> stocks = stockController.getAllStocks();
            var stocksData = stocks.Select((stock, i) => new
            {
                index = i + 1,
                stock.product,
                stock.storage,
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
    }
}