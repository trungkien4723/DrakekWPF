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
using System.Text.Json;

namespace drakek.Controller
{
    public class OrderController
    {
        SupportFunctions supportFunctions = new SupportFunctions();
        CouponController couponController = new CouponController();
        public List<Order> getAllOrders(){
            var ordersData = new List<Order>();
            try
            {
                using (var context = new DrakekDB())
                {
                    ordersData = context.order.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return ordersData;
        }

        public Order getOrder(string id){
            Order order = new Order();
            try
            {
                using (var context = new DrakekDB())
                {
                    order = context.order.Where(c => c.id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return order;
        }

        public void updateOrder(Order orderToUpdate)
        {   
            try
            {
                using (var context = new DrakekDB())
                {
                    if(!string.IsNullOrEmpty(orderToUpdate.id)){
                        var order = context.order.Where(c => c.id == orderToUpdate.id).FirstOrDefault();
                        if(!string.IsNullOrEmpty(orderToUpdate.products)) order.products = orderToUpdate.products;
                        if(!string.IsNullOrEmpty(orderToUpdate.people)) order.people = orderToUpdate.people;
                        if(!string.IsNullOrEmpty(orderToUpdate.customer)) order.customer = orderToUpdate.customer;
                        if(!string.IsNullOrEmpty(orderToUpdate.coupon)) order.coupon = orderToUpdate.coupon;
                        if(!string.IsNullOrEmpty(orderToUpdate.orderType)) order.orderType = orderToUpdate.orderType;
                        if(!string.IsNullOrEmpty(orderToUpdate.description)) order.description = orderToUpdate.description;
                        if(orderToUpdate.paid != null) order.paid = orderToUpdate.paid;
                        if(orderToUpdate.discount != null) order.discount = orderToUpdate.discount;
                        order.totalPrice = orderToUpdate.totalPrice;
                        context.SaveChanges();
                    }
                    else{
                        orderToUpdate.id = supportFunctions.generateID("ordr", 5);
                        orderToUpdate.createdDate = DateTime.Now;
                        context.order.Add(orderToUpdate);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void deleteOrder(string id)
        {
            try
            {
                using (var context = new DrakekDB())
                {
                    var order = context.order.Where(c => c.id == id).FirstOrDefault();
                    context.order.Remove(order);
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