using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using drakek.Model;
using drakek.Data;
using Drakek.Controller;

namespace drakek.Controller
{
    public class RoleController
    {
        SupportFunctions supportFunctions = new SupportFunctions();
        public List<Role> getAllRoles(){
            var rolesData = new List<Role>();
            try
            {
                using (var context = new DrakekDB())
                {
                    rolesData = context.role.Select(role => new Role
                        {
                            id = role.id,
                            name = role.name,
                            permission = role.permission != null ? role.permission : "No Permission"
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return rolesData;
        }

        public Role getRole(string id){
            Role role = new Role();
            try
            {
                using (var context = new DrakekDB())
                {
                    role = context.role.Where(r => r.id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return role;
        }

        public void updateRole(string id, string name)
        {   
            try
            {
                using (var context = new DrakekDB())
                {
                    if(!string.IsNullOrEmpty(id)){
                        var role = context.role.Where(r => r.id == id).FirstOrDefault();
                        role.name = name;
                        context.SaveChanges();
                    }
                    else{
                            Role newRole = new Role
                            {
                                id = supportFunctions.generateID("role", 5),
                                name = name
                            };

                            context.role.Add(newRole);
                            context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void deleteRole(string id)
        {
            try
            {
                using (var context = new DrakekDB())
                {
                    var role = context.role.Where(r => r.id == id).FirstOrDefault();
                    context.role.Remove(role);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}