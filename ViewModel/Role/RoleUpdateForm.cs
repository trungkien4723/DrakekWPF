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
    public partial class RoleUpdateForm: UserControl
    {
        public string id{get; set;}

        private RoleController roleController = new RoleController();
        private SupportFunctions supportFunctions = new SupportFunctions();
        public RoleView roleView;
        public List<Permission> permissions = new List<Permission>();
        public List<Permission> selectedPermissions = new List<Permission>();
        private PeopleController peopleController = new PeopleController();
        public RoleUpdateForm()
        {
            InitializeComponent();
            permissions = roleController.getAllPermissions();
            DataContext = this;
        }

        public void showForm(){
            Role role = roleController.getRole(id);
            if(role != null){
                RoleName.Text = role.name;
                selectedPermissions = roleController.convertPermissionString(role.permission);
            }
            foreach (Permission permission in permissions){
                CheckBox permissionCheckBox = new CheckBox();
                permissionCheckBox.Content = permission.name;
                permissionCheckBox.Tag = permission.id;
                permissionCheckBox.IsChecked = selectedPermissions.Any(sp => sp.id == permission.id);
                RolePermissions.Children.Add(permissionCheckBox);
            }
            Visibility = Visibility.Visible;
            roleView.closeRolePanel();
        }
        public void closeForm(){
            id="";
            clearForm();
            Visibility = Visibility.Collapsed;
            RolePermissions.Children.Clear();

            if(roleView.checkAccessPermission()) roleView.showRolePanel();
            else supportFunctions.mainWindow.changePage("menuDashboard");
        }
        private void clearForm()
        {
            RoleName.Text = "";
            selectedPermissions.Clear();
        }

        private void saveRoleButtonClick(object sender, RoutedEventArgs e)
        {
            if(!updateValidated()) return;
            string name = RoleName.Text;
            List<string> updatedPermissionsIds = selectedPermissions.Select(sp => sp.id).ToList();
            foreach (CheckBox permissionCheckBox in RolePermissions.Children){
                if(permissionCheckBox.IsChecked == true){
                    updatedPermissionsIds.Add(permissionCheckBox.Tag.ToString());
                }else{
                    updatedPermissionsIds.Remove(permissionCheckBox.Tag.ToString());
                }
            }

            roleController.updateRole(id, name, updatedPermissionsIds);
            closeForm();
        }

        private void cancelUpdateRoleButtonClick(object sender, RoutedEventArgs e)
        {
            closeForm();
        }

        private bool updateValidated(){
            bool canUpdate = true;
            People currentUser = supportFunctions.currentUser();

            if(!peopleController.checkPeoplePermission(currentUser, "update_role")){
                canUpdate = false;
                ValidateMessage.Text = "You don't have permission to update";
            };
            if(peopleController.checkPeoplePermission(currentUser, "update_all") == true){
                canUpdate = true;
                ValidateMessage.Text = "";
            }
            if(string.IsNullOrEmpty(RoleName.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Name cannot be empty";
            }

            return canUpdate;
        }
    }
}