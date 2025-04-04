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
using System.Windows.Media.Imaging;

namespace drakek.ViewModel
{
    public partial class LoginWindow: Window
    {
        PeopleController peopleController = new PeopleController();
        public LoginWindow(){
            InitializeComponent();
            LoadCredentials();
            ForgotPasswordForm.Visibility = Visibility.Collapsed;
        }

        private void LoadCredentials()
        {
            RememberMeCheckBox.IsChecked = true;
            using (var cred = new Credential())
            {
                cred.Target = "DrakekApp";
                if (cred.Load())
                {
                    UsernameInput.Text = cred.Username;
                    PasswordInput.Password = cred.Password;
                    loginProcess(cred.Username, cred.Password);
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
            if(ShowPasswordCheckBox.IsChecked == true){PasswordInput.Password = ShowedPasswordInput.Text;}
            else{ShowedPasswordInput.Text = PasswordInput.Password;}
            var username = UsernameInput.Text;
            var password = PasswordInput.Password;
            loginProcess(username, password);
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

        private void loginProcess(string username, string password){
            People user = peopleController.getPeopleByEmail(username);
            if(user == null){
                MessageBox.Show("Username not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(peopleController.hashPassword(password) == user.password){
                SaveCredentials();
                MainWindow mainWindow = new MainWindow(user);
                mainWindow.Show();
                this.Close();
            }else{
                MessageBox.Show("Username or password is incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowPasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ShowedPasswordInput.Text = PasswordInput.Password;
            PasswordInput.Visibility = Visibility.Hidden;
            ShowedPasswordInput.Visibility = Visibility.Visible;
        }

        private void ShowPasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordInput.Password = ShowedPasswordInput.Text;
            PasswordInput.Visibility = Visibility.Visible;
            ShowedPasswordInput.Visibility = Visibility.Hidden;
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e){
            ForgotPasswordForm.Visibility = Visibility.Visible;
        }
        private void CancelForgotPassword_Click(object sender, RoutedEventArgs e){
            ForgotPasswordForm.Visibility = Visibility.Collapsed;
        }

        private void ResendPassword_Click(object sender, RoutedEventArgs e){
            People forgotPasswordPeople = peopleController.getPeopleByEmail(ForgotPasswordEmailInput.Text);
            if(forgotPasswordPeople != null){
                peopleController.resendForgotPassword(forgotPasswordPeople.email);
            }
            else MessageBox.Show("User not found", "Error", MessageBoxButton.OK);
        }
    }
}