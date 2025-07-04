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
        private People peopleToUpdate = new People();
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
            peopleToUpdate = peopleController.getPeople(id);
            if(peopleToUpdate != null){
                PeopleName.Text = peopleToUpdate.name;
                selectedRole = peopleToUpdate.role;
                PeopleRole.SelectedValue = selectedRole;
                PeopleEmail.Text = peopleToUpdate.email;
                PeoplePhone.Text = peopleToUpdate.phone;
                PeopleBirthday.SelectedDate = peopleToUpdate.birthday;
                PeoplePassword.Password = "";
            }
            else peopleToUpdate = new People();    
            try{
                Uri profileImageUri = new Uri(!string.IsNullOrEmpty(peopleToUpdate.image) && File.Exists(peopleToUpdate.image) ? 
                    peopleToUpdate.image : "pack://application:,,,/Images/ProfilePictures/defaultavatar.png", UriKind.RelativeOrAbsolute);
                BitmapImage profileImage = new BitmapImage(profileImageUri);
                ProfileImage.Source = profileImage;
            }catch (Exception ex){MessageBox.Show(ex.Message);}
            
            Visibility = Visibility.Visible;
            peopleView.closePeoplePanel();
        }
        public void closeForm(){
            id="";
            clearForm();
            Visibility = Visibility.Collapsed;
            
            if(peopleView.checkAccessPermission()) peopleView.showPeoplePanel();
            else supportFunctions.mainWindow.changePage("menuDashboard");
        }
        private void clearForm()
        {
                PeopleName.Text = "";
                selectedRole = null;
                PeopleEmail.Text = "";
                PeoplePhone.Text = "";
                PeopleBirthday.SelectedDate = DateTime.Now;
                PeoplePassword.Password = "";
                ValidateMessage.Text = "";
        }

        private void savePeopleButton_Click(object sender, RoutedEventArgs e)
        {
            if(!updateValidated()) return;
            string targetFilePath = peopleToUpdate.image;
            try
            {
                if(!string.IsNullOrEmpty(newProfilePictureSourcePath)){
                    if(newProfilePictureSourcePath == "clearProfileImage"){
                        targetFilePath = "";
                    }
                    else{
                        targetFilePath = Path.Combine(profilePictureDirectory, Path.GetFileName(newProfilePictureSourcePath));
                        if (!Directory.Exists(profilePictureDirectory)) Directory.CreateDirectory(profilePictureDirectory);
                        if (!string.IsNullOrEmpty(newProfilePictureSourcePath) && File.Exists(newProfilePictureSourcePath)){
                            using (FileStream sourceStream = new FileStream(newProfilePictureSourcePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                                using (FileStream targetStream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write, FileShare.None)){
                                    sourceStream.CopyTo(targetStream);
                                }
                            }
                        }
                    }
                }
                
                peopleToUpdate.id = id;
                peopleToUpdate.name = PeopleName.Text;
                peopleToUpdate.role = selectedRole;
                peopleToUpdate.email = PeopleEmail.Text;
                peopleToUpdate.phone = PeoplePhone.Text;
                peopleToUpdate.birthday = PeopleBirthday.SelectedDate.Value;
                peopleToUpdate.password = PeoplePassword.Password;
                peopleToUpdate.image = targetFilePath;

                newProfilePictureSourcePath = "";
                peopleController.updatePeople(peopleToUpdate);

                if(peopleToUpdate.id == supportFunctions.mainWindow.currentUser.id){
                    supportFunctions.mainWindow.currentUser = peopleToUpdate;
                }

                closeForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save profile picture: {ex.Message}");
            }
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
                using (FileStream stream = new FileStream(newProfilePictureSourcePath, FileMode.Open, FileAccess.Read)){
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    ProfileImage.Source = bitmap;
                }
            }
        }

        private void clearProfileImage_Click(object sender, RoutedEventArgs e)
        {
            newProfilePictureSourcePath = "clearProfileImage";
            ProfileImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/ProfilePictures/defaultavatar.png"));
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

            if(!peopleController.checkPeoplePermission(currentUser, "update_people") && peopleToUpdate.id != currentUser.id){
                canUpdate = false;
                ValidateMessage.Text = "You don't have permission to update";
            };
            if(peopleController.checkPeoplePermission(currentUser, "update_all") == true){
                canUpdate = true;
                ValidateMessage.Text = "";
            }
            if(string.IsNullOrEmpty(PeoplePhone.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Phone cannot be empty";
            }
            if(string.IsNullOrEmpty(PeopleEmail.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Email cannot be empty";
            }
            if(string.IsNullOrEmpty(PeopleName.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Email cannot be empty";
            }
            if(PeopleRole.SelectedValue == null){
                canUpdate = false;
                ValidateMessage.Text = "Role cannot be empty";
            }
            if(string.IsNullOrEmpty(id) && string.IsNullOrEmpty(PeoplePassword.Password)){
                canUpdate = false;
                ValidateMessage.Text = "Password cannot be empty";
            }

            return canUpdate;
        }
    }
}