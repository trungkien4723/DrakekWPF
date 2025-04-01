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
        public List<Order> getAllOrders(Dictionary<string, string> filters = null){
            var ordersData = new List<Order>();
            try
            {
                using (var context = new DrakekDB())
                {
                    var query = context.order.AsQueryable();
                    if(filters != null){
                        IQueryable<Order> orQuery = context.order.Where(o => false);
                        foreach (var filter in filters)
                        {
                            string key = filter.Key.ToLower();
                            string value = filter.Value.ToLower();
                            switch (key){
                                case "ordertype":
                                    orQuery = query.Where(o => o.orderType != null && o.orderType.ToLower() == value);
                                    break;
                                case "product":
                                    var matchingProducts = context.product
                                        .Where(p => p.name != null && p.name.ToLower().Contains(value))
                                        .Select(p => p.id)
                                        .ToList();

                                    var ordersWithProducts = context.order
                                        .Where(o => o.products != null)
                                        .ToList();

                                    var filteredOrders = ordersWithProducts
                                        .Where(o =>
                                        {
                                            var orderProducts = JsonSerializer.Deserialize<List<OrderProduct>>(o.products);
                                            return orderProducts != null && orderProducts.Any(op => matchingProducts.Contains(op.product));
                                        });

                                    orQuery = orQuery.Union(filteredOrders.AsQueryable());
                                    break;
                                case "storage":
                                    var matchingStorage = context.storage
                                        .Where(p => p.name != null && p.name.ToLower().Contains(value))
                                        .Select(p => p.id)
                                        .ToList();

                                    var ordersWithStorage = context.order
                                        .Where(o => o.products != null)
                                        .ToList();

                                        filteredOrders = ordersWithStorage
                                        .Where(o =>
                                        {
                                            var orderStorage = JsonSerializer.Deserialize<List<OrderProduct>>(o.products);
                                            return orderStorage != null && orderStorage.Any(op => matchingStorage.Contains(op.storage));
                                        });
                                    break;
                                case "people":
                                    var matchingPeople = context.people
                                        .Where(p => p.name != null && p.name.ToLower().Contains(value))
                                        .Select(p => p.id)
                                        .ToList();
                                    orQuery = orQuery.Union(context.order.Where(o => o.people != null && matchingPeople.Contains(o.people)));
                                    break;
                                case "customer":
                                    var matchingCustomers = context.customer
                                        .Where(c => c.name != null && c.name.ToLower().Contains(value))
                                        .Select(c => c.id)
                                        .ToList();
                                    orQuery = orQuery.Union(context.order.Where(o => o.customer != null && matchingCustomers.Contains(o.customer)));
                                    break;
                                case "status":
                                    if(value != "all"){
                                        query = query.Where(o => o.status != null && o.status.ToLower() == value);
                                    }
                                    else{
                                        query = query.Where(o => o.status != null);
                                    }
                                    break;
                            }
                        }
                        query = orQuery;
                    }
                    ordersData = query.ToList();
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
                        order.description = orderToUpdate.description;
                        if(orderToUpdate.paid != null) order.paid = orderToUpdate.paid;
                        if(orderToUpdate.discount != null) order.discount = orderToUpdate.discount;
                        order.totalPrice = orderToUpdate.totalPrice;
                        if(!string.IsNullOrEmpty(orderToUpdate.status)) order.status = orderToUpdate.status;
                        order.completedDate = orderToUpdate.completedDate;
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