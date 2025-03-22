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
    public partial class OrderView: UserControl
    {
        private OrderController orderController = new OrderController();
        public OrderView()
        {
            InitializeComponent();
            showOrderPanel();
            OrderUpdateForm.orderView = this;
        }

        private void addOrderButtonClick(object sender, RoutedEventArgs e)
        {
            showUpdateOrderForm("", "sell");
        }

        private void editOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Order order = orderController.getOrder(context.id);

            if(order == null){
                MessageBox.Show("Not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            showUpdateOrderForm(order.id, "sell");
        }

        private void deleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Order order = orderController.getOrder(context.id);

            if(order == null){
                MessageBox.Show("Not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                orderController.deleteOrder(order.id);
                showOrderPanel();
            }
        }
        public void showOrderPanel()
        {   
            List<Order> allOrder = orderController.getAllOrders();
            var allOrderData = allOrder.Select((order, i) => new
            {
                index = i + 1,
                order.id,
            }).ToList();
            OrderTable.ItemsSource = allOrderData;
            OrderViewPanel.Visibility = Visibility.Visible;
        }

        public void closeOrderPanel()
        {
            OrderViewPanel.Visibility = Visibility.Collapsed;
        }

        public void showUpdateOrderForm(string updateOrderId, string orderType){
            OrderUpdateForm.id = updateOrderId;
            OrderUpdateForm.orderType = orderType.ToLower();
            OrderUpdateForm.showForm();
        }
    }
}