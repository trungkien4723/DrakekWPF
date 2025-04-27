using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using drakek.Controller;
using System.Windows;
using drakek.Model;
using Drakek.Controller;
using System.Windows.Data;
using System.Globalization;

namespace drakek.ViewModel
{
    public partial class StockView: UserControl
    {
        private SupportFunctions supportFunctions = new SupportFunctions();
        private PeopleController peopleController = new PeopleController();
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
            if (!checkAccessPermission()){
                supportFunctions.mainWindow.show403Page();
                return;
            }
            List<Stock> stocks = stockController.getAllStocks();
            stocks = stocks.OrderBy(s => s.expiredDate).ToList();
            var stocksData = stocks.Select((stock, i) => new
            {
                index = i + 1,
                product = productController.getProduct(stock.product).name,
                storage = storageController.getStorage(stock.storage).name,
                stock.quantity,
                stock.createdDate,
                stock.expiredDate
            }).ToList();
            CollectionViewSource groupedStocks = (CollectionViewSource)FindResource("GroupedStocks");
            groupedStocks.Source = stocksData;
            groupedStocks.GroupDescriptions.Clear();
            groupedStocks.GroupDescriptions.Add(new PropertyGroupDescription("product"));
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

        public bool checkAccessPermission()
        {
            return peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_all") == true
                || peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_stock") == true;
        }
    }

    public class TotalQuantityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = value as System.Collections.IEnumerable;
            if (items == null) return 0;

            // Sum up the quantity for all items in the group
            return items.Cast<dynamic>().Sum(item => (int)item.quantity);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}