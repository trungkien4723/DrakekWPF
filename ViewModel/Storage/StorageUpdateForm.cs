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

namespace drakek.ViewModel
{
    public partial class StorageUpdateForm: UserControl
    {
        public string id{get; set;}
        private StorageController storageController = new StorageController();
        private SupportFunctions supportFunctions = new SupportFunctions();
        public StorageView storageView;
        private PeopleController peopleController = new PeopleController();
        public StorageUpdateForm()
        {
            InitializeComponent();
        }

        public void showForm(){
            Storage storage = storageController.getStorage(id);
            if(storage != null){
                StorageName.Text = storage.name;
            }
            Visibility = Visibility.Visible;
            storageView.closeStoragePanel();
        }
        public void closeForm(){
            id="";
            clearForm();
            Visibility = Visibility.Collapsed;
            
            if(storageView.checkAccessPermission()) storageView.showStoragePanel();
            else supportFunctions.mainWindow.changePage("menuDashboard");
        }
        private void clearForm()
        {
            StorageName.Text = "";
        }

        private void saveStorageButtonClick(object sender, RoutedEventArgs e)
        {
            if(!updateValidated()) return;
            Storage storageToUpdate = new Storage(){
                id = id,
                name = StorageName.Text
            };

            storageController.updateStorage(storageToUpdate);
            closeForm();
        }

        private void cancelUpdateStorageButtonClick(object sender, RoutedEventArgs e)
        {
            closeForm();
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

            if(!peopleController.checkPeoplePermission(currentUser, "update_storage")){
                canUpdate = false;
                ValidateMessage.Text = "You don't have permission to update";
            };
            if(peopleController.checkPeoplePermission(currentUser, "update_all") == true){
                canUpdate = true;
                ValidateMessage.Text = "";
            }
            if(string.IsNullOrEmpty(StorageName.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Name cannot be empty";
            }

            return canUpdate;
        }
    }
}