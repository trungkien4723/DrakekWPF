using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using drakek.Controller;
using System.Windows;
using drakek.Model;
using Drakek.Controller;

namespace drakek.ViewModel
{
    public partial class StorageView: UserControl
    {
        private PeopleController peopleController = new PeopleController();
        private SupportFunctions supportFunctions = new SupportFunctions();
        private StorageController storageController = new StorageController();
        public Dictionary<string, string> filters = new Dictionary<string, string>();
        public StorageView()
        {
            InitializeComponent();
            showStoragePanel();
            StorageUpdateForm.storageView = this;
        }

        private void addStorageButtonClick(object sender, RoutedEventArgs e)
        {
            StorageUpdateForm.id = "";
            StorageUpdateForm.showForm();
        }

        private void editStorageButtonClick(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Storage storage = storageController.getStorage(context.id);

            if(storage == null){
                MessageBox.Show("Storage not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StorageUpdateForm.id = storage.id;
            StorageUpdateForm.showForm();
        }

        private void deleteStorageButtonClick(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Storage storage = storageController.getStorage(context.id);

            if(storage == null){
                MessageBox.Show("Storage not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this storage?", "Delete storage", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                storageController.deleteStorage(storage.id);
                showStoragePanel(filters);
            }
        }

        private void searchStorageButton_Click(object sender, RoutedEventArgs e)
        {
            if (filters.ContainsKey("name")) filters["name"] = SearchStorage.Text;
            else filters.Add("name", SearchStorage.Text);
            showStoragePanel(filters);
        }
        public void showStoragePanel(Dictionary<string, string> searchFilters = null)
        {   
            if (!checkAccessPermission()){
                supportFunctions.mainWindow.show403Page();
                return;
            }
            List<Storage> storages = storageController.getAllStorages(searchFilters).OrderByDescending(s => s.createdDate).ToList();
            var storagesData = storages.Select((storage, i) => new
            {
                index = i + 1,
                storage.id,
                storage.name,
            }).ToList();
            StorageTable.ItemsSource = storagesData;
            StorageViewPanel.Visibility = Visibility.Visible;
        }

        public void closeStoragePanel()
        {
            StorageViewPanel.Visibility = Visibility.Collapsed;
        }

        public bool checkAccessPermission()
        {
            return peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_all") == true
                || peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_storage") == true;
        }
    }
}