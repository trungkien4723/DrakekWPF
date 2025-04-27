using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using drakek.Controller;
using System.Windows;
using drakek.Model;
using Drakek.Controller;
using System.Text.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace drakek.ViewModel
{
    public partial class OrderView: UserControl
    {
        private SupportFunctions supportFunctions = new SupportFunctions();
        private PeopleController peopleController = new PeopleController();
        private CustomerController customerController = new CustomerController();
        private OrderController orderController = new OrderController();
        private CouponController couponController = new CouponController();
        private ProductController productController = new ProductController();
        private StorageController storageController = new StorageController();
        public Dictionary<string, string> filters  = new Dictionary<string, string>();

        public OrderView()
        {
            InitializeComponent();
            showOrderPanel(filters);
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
                showOrderPanel(filters);
            }
        }

        private void searchOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (filters.ContainsKey("product")) filters["product"] = SearchOrder.Text;
            else filters.Add("product", SearchOrder.Text);
            if (filters.ContainsKey("storage")) filters["storage"] = SearchOrder.Text;
            else filters.Add("storage", SearchOrder.Text);
            if (filters.ContainsKey("people")) filters["people"] = SearchOrder.Text;
            else filters.Add("people", SearchOrder.Text);
            if (filters.ContainsKey("customer")) filters["customer"] = SearchOrder.Text;
            else filters.Add("customer", SearchOrder.Text);
        
            showOrderPanel(filters);
        }

        private void orderStatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filters.ContainsKey("status")) filters["status"] = OrderStatusFilter.SelectedValue.ToString();
            else filters.Add("status", OrderStatusFilter.SelectedValue.ToString());

            showOrderPanel(filters);
        }

        private void orderStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try{
                var comboBox = sender as ComboBox;
                if (comboBox == null) return;

                var context = comboBox.DataContext as dynamic;
                if (context == null) return;

                var selectedItem = comboBox.SelectedItem as ComboBoxItem;
                if (selectedItem == null) return;

                string selectedStatus = comboBox.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(selectedStatus)) return;

                Order order = orderController.getOrder(context.id);
                if (order != null){
                    order.status = selectedStatus;
                    orderController.updateOrder(order);
                    showOrderPanel(filters);
                }
                else{
                    MessageBox.Show("Order not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }catch (Exception ex){
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void showOrderPanel(Dictionary<string, string> searchFilters = null)
        {
            if (!checkAccessPermission()){
                supportFunctions.mainWindow.show403Page();
                return;
            }
            
            if (filters.ContainsKey("orderType")) filters["orderType"] = "sell";
            else filters.Add("orderType", "sell");

            List<Order> allOrder = orderController.getAllOrders(searchFilters).OrderByDescending(o => o.createdDate).ToList();
            var allOrderData = allOrder.Select((order, i) =>
            {
                Coupon orderCoupon = couponController.getCoupon(order.coupon);
                List<OrderProduct> orderProducts = JsonSerializer.Deserialize<List<OrderProduct>>(order.products);
                string productNames = string.Join(", ", orderProducts.Select(p => productController.getProduct(p.product) != null ? productController.getProduct(p.product).name : "Unknown product"));
                return new {
                    index = i + 1,
                    order.id,
                    people = peopleController.getPeople(order.people).name,
                    customer = customerController.getCustomer(order.customer).name,
                    coupon = orderCoupon != null ? orderCoupon.name : "",
                    order.description,
                    paid = order.paid.ToString(),
                    discount = order.discount.ToString(),
                    totalPrice = order.totalPrice.ToString(),
                    createdDate = order.createdDate.ToString("d"),
                    products = !string.IsNullOrEmpty(productNames) ? productNames : "No products",
                    order.status
                };
            }).ToList();
            if(OrderTable != null) OrderTable.ItemsSource = allOrderData;
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
        public bool checkAccessPermission()
        {
            return peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_all") == true
                || peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_order") == true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}