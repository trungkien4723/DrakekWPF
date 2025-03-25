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
    public partial class RoleView: UserControl
    {
        private PeopleController peopleController = new PeopleController();
        private SupportFunctions supportFunctions = new SupportFunctions();
        private RoleController roleController = new RoleController();
        public RoleView()
        {
            InitializeComponent();
            showRolePanel();
            RoleUpdateForm.roleView = this;
        }

        private void addRoleButtonClick(object sender, RoutedEventArgs e)
        {
            RoleUpdateForm.id = "";
            RoleUpdateForm.showForm();
        }

        private void editRoleButtonClick(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Role role = roleController.getRole(context.id);

            if(role == null){
                MessageBox.Show("Product not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RoleUpdateForm.id = role.id;
            RoleUpdateForm.showForm();
        }

        private void deleteRoleButtonClick(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Role role = roleController.getRole(context.id);

            if(role == null){
                MessageBox.Show("Product not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                roleController.deleteRole(role.id);
                showRolePanel();
            }
        }
        public void showRolePanel()
        {   
            if (!checkAccessPermission()){
                supportFunctions.mainWindow.show403Page();
                return;
            }
            List<Role> roles = roleController.getAllRoles();
            var rolesData = roles.Select((role, i) => new
            {
                index = i + 1,
                role.id,
                role.name,
                role.permission
            }).ToList();
            RoleTable.ItemsSource = rolesData;
            RoleViewPanel.Visibility = Visibility.Visible;
        }

        public void closeRolePanel()
        {
            RoleViewPanel.Visibility = Visibility.Collapsed;
        }

        public bool checkAccessPermission()
        {
            return peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_all") == true
                || peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_role") == true;
        }
    }
}