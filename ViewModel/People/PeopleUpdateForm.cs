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

namespace drakek.ViewModel
{
    public partial class PeopleUpdateForm: UserControl
    {
        public string id{get; set;}
        public List<Role> roles{get; set;}
        public string selectedRole{get; set;} 
        private string newProfilePictureSourcePath;
        private string profilePictureDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/ProfilePictures");

        private PeopleController peopleController = new PeopleController();
        private SupportFunctions supportFunctions = new SupportFunctions();
        private RoleController roleController = new RoleController();
        public PeopleView peopleView;
        public PeopleUpdateForm()
        {
            InitializeComponent();
            roles = roleController.getAllRoles();
            DataContext = this;
        }

        public void showForm(){
            People people = peopleController.getPeople(id);
            try{
                Uri profileImageUri = new Uri(File.Exists(people.image) ? 
                    people.image : "pack://application:,,,/Images/ProfilePictures/defaultavatar.png", UriKind.RelativeOrAbsolute);
                BitmapImage profileImage = new BitmapImage(profileImageUri);
                ProfileImage.Source = profileImage;
            }catch (Exception ex){}

            if(people != null){
                PeopleName.Text = people.name;
                PeopleRole.SelectedValue = people.role;
                PeopleEmail.Text = people.email;
                PeoplePhone.Text = people.phone;
                PeopleBirthday.SelectedDate = people.birthday;
                PeoplePassword.Password = "";
            }
            Visibility = Visibility.Visible;
            peopleView.closePeoplePanel();
        }
        public void closeForm(){
            id="";
            clearForm();
            Visibility = Visibility.Collapsed;
            peopleView.showPeoplePanel();
        }
        private void clearForm()
        {
                PeopleName.Text = "";
                selectedRole = null;
                PeopleEmail.Text = "";
                PeoplePhone.Text = "";
                PeopleBirthday.SelectedDate = null;
                PeoplePassword.Password = "";
        }

        private void savePeopleButton_Click(object sender, RoutedEventArgs e)
        {
            string targetFilePath = Path.Combine(profilePictureDirectory, Path.GetFileName(newProfilePictureSourcePath));
            if (!Directory.Exists(profilePictureDirectory)) Directory.CreateDirectory(profilePictureDirectory);
            if(File.Exists(newProfilePictureSourcePath)) File.Copy(newProfilePictureSourcePath, targetFilePath, true);
            People peopleToUpdate = new People(){
                id = id,
                name = PeopleName.Text,
                role = selectedRole,
                email = PeopleEmail.Text,
                phone = PeoplePhone.Text,
                birthday = PeopleBirthday.SelectedDate.Value,
                password = PeoplePassword.Password,
                image = ProfileImage.Source != null ? targetFilePath : ""
            };
            peopleController.updatePeople(peopleToUpdate);
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(mw => mw.Visibility == Visibility.Visible);
            if (mainWindow != null){
                mainWindow = new MainWindow(mainWindow.currentUser);
            }
            closeForm();
        }

        private void cancelUpdatePeopleButton_Click(object sender, RoutedEventArgs e)
        {
            closeForm();
        }

        private void profileImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                newProfilePictureSourcePath = openFileDialog.FileName;
                MessageBox.Show(newProfilePictureSourcePath);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(newProfilePictureSourcePath);
                bitmap.EndInit();
                ProfileImage.Source = bitmap;
            }
        }

        private void numberInputOnlyTextbox(object sender, TextCompositionEventArgs e)
        {
            supportFunctions.previewTextInput(sender, e, "number");
        }
        private void numberPasteOnlyTextbox(object sender, DataObjectPastingEventArgs e)
        {
            supportFunctions.previewTextPasting(sender, e, "number");
        }
    }
}