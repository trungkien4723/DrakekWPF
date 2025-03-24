using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using drakek.Controller;
using System.Windows;
using drakek.Model;
using Drakek.Controller;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace drakek.ViewModel
{
    public partial class OrderUpdateForm: UserControl
    {
        public string id{get; set;}
        public string orderType{get; set;}
        private Order orderToUpdate {get; set;}
        private OrderController orderController = new OrderController();
        private PeopleController peopleController = new PeopleController();
        private CouponController couponController = new CouponController();
        private CustomerController customerController = new CustomerController();
        private ProductController productController = new ProductController();
        private StorageController storageController = new StorageController();
        private StockController stockController = new StockController();
        private SupportFunctions supportFunctions = new SupportFunctions();
        public OrderView? orderView;
        public StockView? stockView;
        public List<People> staffs {get; set;}
        public List<Coupon> coupons {get; set;}
        public List<Storage> storages {get; set;}
        public string selectedStaff {get; set;}
        public string selectedCoupon {get; set;}
        public List<OrderProduct> orderProducts = new List<OrderProduct>();

        public OrderUpdateForm()
        {
            InitializeComponent();
            DataContext = this;
            staffs = peopleController.getAllPeople();
            coupons = couponController.getAllCoupons();
            coupons.Add(new Coupon(){id = "noCoupon", name = "Don't use coupon"});
            storages = storageController.getAllStorages();
        }

        public void showForm(){
            orderToUpdate = orderController.getOrder(id);
            if(orderToUpdate != null){
                orderProducts = JsonSerializer.Deserialize<List<OrderProduct>>(orderToUpdate.products);
                if(orderProducts != null){
                    foreach(var orderProduct in orderProducts){
                        addProductInput(orderProduct.product, orderProduct.quantity, orderProduct.price, orderProduct.storage, orderProduct.expiredDate);
                    }
                }
                selectedCoupon = orderToUpdate.coupon ?? "noCoupon";
                selectedStaff = orderToUpdate.people;
                OrderPeople.SelectedValue = selectedStaff;
                Customer orderCustomer = customerController.getCustomer(orderToUpdate.customer);
                if(orderCustomer != null){
                    CustomerNameInput.Text = orderCustomer.name;
                    CustomerPhoneInput.Text = orderCustomer.phone;
                    CustomerAddressInput.Text = orderCustomer.address;
                    CustomerCityInput.Text = orderCustomer.city;
                    CustomerDistrictInput.Text = orderCustomer.district;
                    CustomerWardInput.Text = orderCustomer.ward;
                }
                OrderPaid.Text = orderToUpdate.paid.ToString();
            }    
            else{
                orderToUpdate = new Order();
                addProductInput();
                selectedCoupon = "noCoupon";
            }
            OrderCoupon.SelectedValue = selectedCoupon;
            Visibility = Visibility.Visible;
            if(orderView != null) orderView.closeOrderPanel();
            else if(stockView != null) stockView.closeStockPanel();
        }
        public void closeForm(){
            id="";
            clearForm();
            Visibility = Visibility.Collapsed;
            if(orderType == "buy"){
                StockView stockView = new StockView(); 
                if(stockView.checkAccessPermission()){
                    stockView.showStockPanel();
                }
                else supportFunctions.mainWindow.changePage("menuDashboard");
            }
            else{
                if(orderView.checkAccessPermission()) orderView.showOrderPanel();
                else supportFunctions.mainWindow.changePage("menuDashboard");
            }
        }
        private void clearForm()
        {
            OrderProductsForm.Children.Clear();
            selectedStaff = "";
            CustomerNameInput.Text = "";
            CustomerPhoneInput.Text = "";
            CustomerAddressInput.Text = "";
            CustomerCityInput.Text = "";
            CustomerDistrictInput.Text = "";
            CustomerWardInput.Text = "";
            OrderPaid.Text = "";
        }

        private void addProductInput(string productName = "", int productQuantity = 1, int productPrice = 0, string productStorage = "", DateTime? productExpiredDate = null)
        {
            Grid inputGrid = new Grid();
            inputGrid.ColumnDefinitions.Add(new ColumnDefinition());
            inputGrid.ColumnDefinitions.Add(new ColumnDefinition());
            inputGrid.ColumnDefinitions.Add(new ColumnDefinition());
            inputGrid.ColumnDefinitions.Add(new ColumnDefinition());
            inputGrid.ColumnDefinitions.Add(new ColumnDefinition());
            inputGrid.ColumnDefinitions.Add(new ColumnDefinition());

            inputGrid.RowDefinitions.Add(new RowDefinition());
            inputGrid.RowDefinitions.Add(new RowDefinition());

            switch(orderType.ToLower()){
                case "buy":
                    Label nameLabel = new Label(){Content = "Name"};
                    Grid.SetColumn(nameLabel, 0);
                    Grid.SetRow(nameLabel, 0);
                    ComboBox productCombobox = new ComboBox()
                    {
                        Width = 100,
                        Margin = new Thickness(5)
                    };
                    Grid.SetColumn(productCombobox, 0);
                    Grid.SetRow(productCombobox, 1);
                    List<Product> products = productController.getAllProducts();
                    productCombobox.ItemsSource = products;
                    productCombobox.SelectedValuePath = "id";
                    productCombobox.DisplayMemberPath = "name";
                    productCombobox.SelectedValue = productName.Trim();
                    inputGrid.Children.Add(nameLabel);
                    inputGrid.Children.Add(productCombobox);

                    Label storageLabel = new Label(){Content = "Storage"};
                    Grid.SetColumn(storageLabel, 1);
                    Grid.SetRow(storageLabel, 0);
                    ComboBox storageCombobox = new ComboBox()
                    {
                        Width = 100,
                        Margin = new Thickness(5)
                    };
                    Grid.SetColumn(storageCombobox, 1);
                    Grid.SetRow(storageCombobox, 1);
                    List<Storage> storages = storageController.getAllStorages();
                    storageCombobox.ItemsSource = storages;
                    storageCombobox.SelectedValuePath = "id";
                    storageCombobox.DisplayMemberPath = "name";
                    storageCombobox.SelectedValue = productStorage.Trim();
                    inputGrid.Children.Add(storageLabel);
                    inputGrid.Children.Add(storageCombobox);
                break;
                case "sell":
                    Label productLabel = new Label(){Content = "Choose product"};
                    Grid.SetColumn(productLabel, 0);
                    Grid.SetRow(productLabel, 0);
                    ComboBox sellProductCombobox = new ComboBox()
                    {
                        Width = 200,
                        Margin = new Thickness(5)
                    };
                    Grid.SetColumn(sellProductCombobox, 0);
                    Grid.SetRow(sellProductCombobox, 1);
                    List<Stock> stocks = stockController.getAllStocks();
                    List<ProductOnStock> sellProducts = new List<ProductOnStock>();
                    foreach(Stock stock in stocks){
                        sellProducts.Add(new ProductOnStock(){
                            id = stock.product + "-" + stock.storage,
                            name = productController.getProduct(stock.product).name + " - " + storageController.getStorage(stock.storage).name
                        });
                    }
                    sellProductCombobox.ItemsSource = sellProducts;
                    sellProductCombobox.SelectedValuePath = "id";
                    sellProductCombobox.DisplayMemberPath = "name";
                    sellProductCombobox.SelectedValue = productName.Trim() + "-" + productStorage.Trim();
                    inputGrid.Children.Add(productLabel);
                    inputGrid.Children.Add(sellProductCombobox);
                break;
            }

            Label quantityLabel = new Label(){Content = "Quantity"};
            Grid.SetColumn(quantityLabel, 2);
            Grid.SetRow(quantityLabel, 0);
            TextBox quantityTextBox = new TextBox()
            {
                Text = productQuantity.ToString(),
                Width = 50,
                Margin = new Thickness(5, 10, 5, 10),
            };
            quantityTextBox.PreviewTextInput += numberInputOnlyTextbox;
            DataObject.AddPastingHandler(quantityTextBox, numberPasteOnlyTextbox);
            Grid.SetColumn(quantityTextBox, 2);
            Grid.SetRow(quantityTextBox, 1);
            inputGrid.Children.Add(quantityLabel);
            inputGrid.Children.Add(quantityTextBox);

            Label priceLabel = new Label(){Content = "Price"};
            Grid.SetColumn(priceLabel, 3);
            Grid.SetRow(priceLabel, 0);
            TextBox priceTextBox = new TextBox()
            {
                Text = productPrice.ToString(),
                Width = 70,
                Margin = new Thickness(5, 10, 5, 10),
            };
            priceTextBox.PreviewTextInput += numberInputOnlyTextbox;
            DataObject.AddPastingHandler(priceTextBox, numberPasteOnlyTextbox);
            Grid.SetColumn(priceTextBox, 3);
            Grid.SetRow(priceTextBox, 1);
            inputGrid.Children.Add(priceLabel);
            inputGrid.Children.Add(priceTextBox);

            if(orderType.ToLower() == "buy"){
                Label expiredDateLabel = new Label(){Content = "Expired Date"};
                Grid.SetColumn(expiredDateLabel, 4);
                Grid.SetRow(expiredDateLabel, 0);
                DatePicker expiredDateDatePicker = new DatePicker()
                {
                    SelectedDate = productExpiredDate,
                    Width = 100,
                    Margin = new Thickness(5)
                };
                Grid.SetColumn(expiredDateDatePicker, 4);
                Grid.SetRow(expiredDateDatePicker, 1);
                inputGrid.Children.Add(expiredDateLabel);
                inputGrid.Children.Add(expiredDateDatePicker);
            }

            Button removeButton = new Button()
            {
                Content = "-",
                Width = 25,
                Height= 25,
                Margin = new Thickness(5)
            };
            removeButton.Click += (sender, e) => {
                if(OrderProductsForm.Children.Count > 1){ 
                    OrderProductsForm.Children.Remove(inputGrid);
                }
            };
            Grid.SetColumn(removeButton, 5);
            Grid.SetRow(removeButton, 1);
            inputGrid.Children.Add(removeButton);

            OrderProductsForm.Children.Add(inputGrid);
        }

        private void saveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if(!updateValidated()) return;
            try{
                int totalPrice = 0;
                orderProducts = new List<OrderProduct>();
                foreach (Grid productInputGrid in OrderProductsForm.Children)
                {
                    string productId = "";
                    int productQuantity = 0;
                    int productPrice = 0;
                    string productStorage = "";
                    DateTime? productExpiredDate = null;
                    switch(orderType.ToLower()){
                        case "buy":
                            foreach (var control in productInputGrid.Children)
                            {
                                if (control is TextBox textBox)
                                {
                                    if (Grid.GetColumn(textBox) == 2)
                                    {
                                        int.TryParse(textBox.Text, out productQuantity);
                                    }
                                    else if (Grid.GetColumn(textBox) == 3)
                                    {
                                        int.TryParse(textBox.Text, out productPrice);
                                    }
                                }
                                else if (control is ComboBox comboBox)
                                {
                                    if(Grid.GetColumn(comboBox) == 0){
                                        productId = comboBox.SelectedValue.ToString();
                                    }
                                    else if (Grid.GetColumn(comboBox) == 1){
                                        productStorage = comboBox.SelectedValue.ToString();
                                    }
                                }
                                else if (control is DatePicker datePicker && Grid.GetColumn(datePicker) == 4)
                                {
                                    productExpiredDate = datePicker.SelectedDate;
                                }
                            }
                        break;
                        case "sell":
                            foreach (var control in productInputGrid.Children)
                            {
                                if (control is TextBox textBox)
                                {
                                    if (Grid.GetColumn(textBox) == 2)
                                    {
                                        int.TryParse(textBox.Text, out productQuantity);
                                    }
                                    else if (Grid.GetColumn(textBox) == 3)
                                    {
                                        int.TryParse(textBox.Text, out productPrice);
                                    }
                                }
                                else if (control is ComboBox comboBox && Grid.GetColumn(comboBox) == 0)
                                {
                                    productId = comboBox.Text.Split("-")[0];
                                    productStorage = comboBox.Text.Split("-")[1];
                                }
                                else if (control is DatePicker datePicker && Grid.GetColumn(datePicker) == 4)
                                {
                                    productExpiredDate = datePicker.SelectedDate;
                                }
                            }
                        break;
                    }
                    orderProducts.Add(new OrderProduct
                    {
                        product = productId,
                        quantity = productQuantity,
                        storage = productStorage,
                        expiredDate = productExpiredDate
                    });
                    if(orderType == "buy"){
                        Stock stock = stockController.getStock(productId, productStorage);
                        if(stock == null || stock.expiredDate != productExpiredDate){
                            stock = new Stock(){
                                product = productId,
                                storage = productStorage,
                                quantity = productQuantity,
                                expiredDate = productExpiredDate
                            };
                        }
                        else{
                            stock.quantity += productQuantity;
                        }
                        stockController.updateStock(stock);
                    }
                    totalPrice += productPrice;
                }

                int discount = int.TryParse(OrderDiscount.Text, out int d) ? d : 0;
                totalPrice -= discount;

                orderToUpdate.id = id;
                orderToUpdate.products = JsonSerializer.Serialize(orderProducts);
                orderToUpdate.people = selectedStaff;

                Customer customer = customerController.getCustomerByPhone(CustomerPhoneInput.Text);
                if(customer == null){
                    customer = new Customer(){
                        id = "",
                        name = CustomerNameInput.Text,
                        phone = CustomerPhoneInput.Text,
                        address = CustomerAddressInput.Text,
                        city = CustomerCityInput.Text,
                        district = CustomerDistrictInput.Text,
                        ward = CustomerWardInput.Text,
                    };
                    customer = customerController.updateCustomer(customer);
                }
                orderToUpdate.customer = customer.id;
                Coupon orderCoupon = couponController.getCoupon(selectedCoupon);
                if(orderCoupon != null){
                    int couponValue = orderCoupon.valueType == "%" ? totalPrice * orderCoupon.value / 100 : orderCoupon.value;
                    totalPrice -= couponValue;
                }
                
                orderToUpdate.coupon = selectedCoupon;
                orderToUpdate.orderType = orderType;
                orderToUpdate.description = OrderDescription.Text;
                orderToUpdate.paid = int.TryParse(OrderPaid.Text, out int p) ? p : 0;
                orderToUpdate.discount = discount;
                orderToUpdate.totalPrice = totalPrice;
                
                orderController.updateOrder(orderToUpdate);
                closeForm();
            }
            catch(Exception ex){
                MessageBox.Show($"Failed to save: {ex.Message}");
            }
        }

        private void cancelUpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            closeForm();
        }

        private void addProductButton_Click(object sender, RoutedEventArgs e){
            addProductInput();
        }

        private void numberInputOnlyTextbox(object sender, TextCompositionEventArgs e)
        {
            supportFunctions.previewTextInput(sender, e, "number");
        }
        private void numberPasteOnlyTextbox(object sender, DataObjectPastingEventArgs e)
        {
            supportFunctions.previewTextPasting(sender, e, "number");
        }

        private bool updateValidated(){
            bool canUpdate = true;
            People currentUser = supportFunctions.currentUser();

            if(!peopleController.checkPeoplePermission(currentUser, "update_order")){
                canUpdate = false;
                ValidateMessage.Text = "You don't have permission to update";
            };
            if(peopleController.checkPeoplePermission(currentUser, "update_all") == true){
                canUpdate = true;
                ValidateMessage.Text = "";
            }
            foreach (Grid productInputGrid in OrderProductsForm.Children)
            {
                switch(orderType.ToLower()){
                    case "buy":
                        foreach (var control in productInputGrid.Children)
                        {
                            if (control is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
                            {
                                if (Grid.GetColumn(textBox) == 2)
                                {
                                    canUpdate = false;
                                    ValidateMessage.Text = "Quantity cannot be empty";
                                }
                                else if (Grid.GetColumn(textBox) == 3)
                                {
                                    canUpdate = false;
                                    ValidateMessage.Text = "Price cannot be empty";
                                }
                            }
                            else if (control is ComboBox comboBox && comboBox.SelectedValue == null)
                            {
                                if(Grid.GetColumn(comboBox) == 0){
                                    canUpdate = false;
                                    ValidateMessage.Text = "Product cannot be empty";
                                }
                                else if (Grid.GetColumn(comboBox) == 1){
                                    canUpdate = false;
                                    ValidateMessage.Text = "Storage cannot be empty";
                                }
                            }
                        }
                    break;
                    case "sell":
                        foreach (var control in productInputGrid.Children)
                        {
                            if (control is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
                            {
                                if (Grid.GetColumn(textBox) == 2)
                                {
                                    canUpdate = false;
                                    ValidateMessage.Text = "Quantity cannot be empty";
                                }
                                else if (Grid.GetColumn(textBox) == 3)
                                {
                                    canUpdate = false;
                                    ValidateMessage.Text = "Price cannot be empty";
                                }
                            }
                            else if (control is ComboBox comboBox && Grid.GetColumn(comboBox) == 0 && comboBox.SelectedValue == null)
                            {
                                canUpdate = false;
                                ValidateMessage.Text = "Product cannot be empty";
                            }
                        }
                    break;
                }
            }
            if(string.IsNullOrEmpty(selectedStaff)){
                canUpdate = false;
                ValidateMessage.Text = "Staff cannot be empty";
            }
            if(string.IsNullOrEmpty(CustomerNameInput.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Customer name cannot be empty";
            }
            if(string.IsNullOrEmpty(CustomerPhoneInput.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Customer phone cannot be empty";
            }
            if(string.IsNullOrEmpty(CustomerCityInput.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Customer city cannot be empty";
            }

            return canUpdate;
        }
    }
}