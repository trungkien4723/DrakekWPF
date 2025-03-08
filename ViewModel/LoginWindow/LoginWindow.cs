using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using drakek.Controller;
using drakek.Model;
using CredentialManagement;

namespace drakek.ViewModel
{
    public partial class LoginWindow: Window
    {
        PeopleController peopleController = new PeopleController();
        public LoginWindow(){
            InitializeComponent();
            LoadCredentials();
        }

        private void LoadCredentials()
        {
            using (var cred = new Credential())
            {
                cred.Target = "DrakekApp";
                if (cred.Load())
                {
                    UsernameInput.Text = cred.Username;
                    PasswordInput.Password = cred.Password;
                    RememberMeCheckBox.IsChecked = true;
                }
            }
        }

        private void SaveCredentials()
        {
            using (var cred = new Credential())
            {
                cred.Target = "DrakekApp";
                cred.Username = UsernameInput.Text;
                cred.Password = PasswordInput.Password;
                cred.Type = CredentialType.Generic;

                if (RememberMeCheckBox.IsChecked == true)
                {
                    cred.PersistanceType = PersistanceType.LocalComputer;
                    cred.Save();
                }
                else
                {
                    cred.Delete();
                }
            }
        }

        public void LoginButton_Click(object sender, RoutedEventArgs e){
            var username = UsernameInput.Text;
            var password = PasswordInput.Password;
            People user = peopleController.getPeopleByEmail(username);
            if(user == null){
                MessageBox.Show("Username not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(peopleController.hashPassword(password) == user.password){
                SaveCredentials();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                mainWindow.user = user;
                this.Close();
            }else{
                MessageBox.Show("Username or password is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
    }
}