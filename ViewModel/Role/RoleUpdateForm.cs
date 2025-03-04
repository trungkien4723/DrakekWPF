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
        public RoleUpdateForm()
        {
            InitializeComponent();
        }

        public void showForm(){
            Role role = roleController.getRole(id);
            if(role != null){
                RoleName.Text = role.name;
            }
            Visibility = Visibility.Visible;
            roleView.closeProductPanel();
        }
        public void closeForm(){
            id="";
            clearForm();
            Visibility = Visibility.Collapsed;
            roleView.showRolePanel();
        }
        private void clearForm()
        {
            RoleName.Text = "";
        }

        private void saveRoleButtonClick(object sender, RoutedEventArgs e)
        {
            string name = RoleName.Text;

            roleController.updateRole(id, name);
            closeForm();
        }

        private void cancelUpdateRoleButtonClick(object sender, RoutedEventArgs e)
        {
            closeForm();
        }
    }
}