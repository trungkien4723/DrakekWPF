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
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.IO;
using CredentialManagement;
using drakek.Controller;

namespace drakek.ViewModel{
    /// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
    public partial class MainWindow : Window
    {
        private PeopleController peopleController = new PeopleController();
        private People CurrentUser;
        public People currentUser{
            get { return CurrentUser; }
            set {
                if(CurrentUser != value){
                    CurrentUser = value;
                    OnPropertyChanged(nameof(currentUser));
                    OnPropertyChanged(nameof(currentUserFirstName));
                    loadProfileImage();
                }
            }
        }
        public string currentUserFirstName
        {
            get
            {
                if (CurrentUser != null && !string.IsNullOrEmpty(CurrentUser.name))
                {
                    return CurrentUser.name.Split(' ')[0];
                }
                return string.Empty;
            }
        }
        public MainWindow(People user)
        {
            InitializeComponent();
            currentUser = user;
            DataContext = this;
            loadMenuOptions();
        }

        public void loadProfileImage()
        {
            try
            {
                Uri profileImageUri = new Uri(File.Exists(currentUser.image) ? 
                    currentUser.image : "pack://application:,,,/Images/ProfilePictures/defaultavatar.png", UriKind.RelativeOrAbsolute);
                BitmapImage profileImage = new BitmapImage(profileImageUri);
                ProfileImage.Source = profileImage;
            }catch (Exception ex){}
        }

        private void loadMenuOptions()
        {
            var filteredMenuOptions = MenuOptions.AllMenuOptions
                .Where(option => string.IsNullOrEmpty(option.RequiredPermission) ||
                    peopleController.checkPeoplePermission(currentUser, option.RequiredPermission) ||
                    peopleController.checkPeoplePermission(currentUser, "access_all")
                )
                .ToList();
            List<ListViewItem> menuItems = new List<ListViewItem>();
            foreach (MenuOption option in filteredMenuOptions)
            {
                ListViewItem menuItem = new ListViewItem
                {
                    Name = option.Name,
                    Content = option.Content
                };
                menuItems.Add(menuItem);
            }
            mainMenu.ItemsSource = menuItems;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e){
            renderPages.Children.Clear();
            renderPages.Children.Add(new DashboardView());
        }
        private void Drag_Window(object sender, MouseButtonEventArgs e){
            this.DragMove();
        }
        private void Minimize_Click(object sender, RoutedEventArgs e){
            this.WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e){
            if(this.WindowState == WindowState.Maximized){
                this.WindowState = WindowState.Normal;
            }else{
                this.WindowState = WindowState.Maximized;
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e){
            Application.Current.Shutdown();
        }
        private void AccountOptions_Click(object sender, RoutedEventArgs e){
            ProfileImage.ContextMenu.IsOpen = true;
        }
        private void ViewProfile_Click(object sender, RoutedEventArgs e){
            PeopleView peopleView = new PeopleView();
            renderPages.Children.Clear();
            renderPages.Children.Add(peopleView);
            peopleView.showUpdatePeopleForm(currentUser.id);
        }
        private void Settings_Click(object sender, RoutedEventArgs e){}
        private void Logout_Click(object sender, RoutedEventArgs e){
            using (var cred = new Credential()){
                cred.Target = "DrakekApp";
                if (cred.Load()) cred.Delete();
            }
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
        private void changeSelectedMenuPage(object sender, SelectionChangedEventArgs e){
            if (mainMenu.SelectedItem is ListViewItem selectedItem)
            {
                changePage(selectedItem.Name.ToString());
            }
        }
        public void changePage(string pageName){
            renderPages.Children.Clear();
            switch (pageName)
            {
                case "menuDashboard":
                    renderPages.Children.Add(new DashboardView());
                break;
                case "menuProduct":
                    renderPages.Children.Add(new ProductView());
                break;
                case "menuPeople":
                    renderPages.Children.Add(new PeopleView());
                break;
                case "menuRole":
                    renderPages.Children.Add(new RoleView());
                break;
                case "menuStorage":
                    renderPages.Children.Add(new StorageView());
                break;
                case "menuStock":
                    renderPages.Children.Add(new StockView());
                break;
                case "menuCustomer":
                    renderPages.Children.Add(new CustomerView());
                break;
                case "menuCoupon":
                    renderPages.Children.Add(new CouponView());
                break;
                case "menuOrder":
                    renderPages.Children.Add(new OrderView());
                break;
                default:
                    renderPages.Children.Add(new DashboardView());
                break; 
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class MenuOption
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string RequiredPermission { get; set; }
    }

    public static class MenuOptions
    {
        public static List<MenuOption> AllMenuOptions = new List<MenuOption>
        {
            new MenuOption { Name = "menuDashboard", Content = "Dashboard", RequiredPermission = "" },
            new MenuOption { Name = "menuOrder", Content = "Order", RequiredPermission = "access_order" },
            new MenuOption { Name = "menuProduct", Content = "Product", RequiredPermission = "access_product" },
            new MenuOption { Name = "menuPeople", Content = "People", RequiredPermission = "access_people" },
            new MenuOption { Name = "menuCustomer", Content = "Customer", RequiredPermission = "access_customer" },
            new MenuOption { Name = "menuStorage", Content = "Storage", RequiredPermission = "access_storage" },
            new MenuOption { Name = "menuCoupon", Content = "Coupon", RequiredPermission = "access_coupon" },
            new MenuOption { Name = "menuRole", Content = "Role", RequiredPermission = "access_role" },
            new MenuOption { Name = "menuStock", Content = "Stock", RequiredPermission = "access_stock" }
        };
    }

}