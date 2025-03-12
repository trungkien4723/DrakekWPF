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
            storageView.showStoragePanel();
        }
        private void clearForm()
        {
            StorageName.Text = "";
        }

        private void saveStorageButtonClick(object sender, RoutedEventArgs e)
        {
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
    }
}