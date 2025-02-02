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

namespace drakek.Controller{
    /// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e){
        renderPages.Children.Clear();
        renderPages.Children.Add(new Controller.Dashboard());
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
        this.Close();
    }

    private void changeSelectedMenuPage(object sender, SelectionChangedEventArgs e){
        if (mainMenu.SelectedItem is ListViewItem selectedItem)
            {
                renderPages.Children.Clear();
                switch (selectedItem.Name.ToString())
                {
                    case "btnDashboard":
                        renderPages.Children.Add(new Controller.Dashboard());
                        // renderPages.Content = new Controller.Dashboard();
                    break;
                    case "btnProduct":
                        renderPages.Children.Add(new Controller.Product());
                        // renderPages.Content = new Controller.Product();
                    break;
                    default:
                        renderPages.Children.Add(new Controller.Dashboard());
                    break; 
                }
            }
    }
}
}