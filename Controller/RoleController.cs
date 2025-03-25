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
                    if(role != null){
                        if(role.permission == null) role.permission = "No Permission";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return role;
        }

        public void updateRole(string id, string name, List<string> permissionIds)
        {   
            try
            {
                using (var context = new DrakekDB())
                {
                    if(!string.IsNullOrEmpty(id)){
                        var role = context.role.Where(r => r.id == id).FirstOrDefault();
                        role.name = name;
                        role.permission = string.Join(",", permissionIds);
                        context.SaveChanges();
                    }
                    else{
                            Role newRole = new Role
                            {
                                id = supportFunctions.generateID("role", 5),
                                name = name,
                                permission = string.Join(",", permissionIds),
                                createdDate = DateTime.Now
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

        public void updateRolePermission(string id, string permission)
        {
            try
            {
                using (var context = new DrakekDB())
                {
                    var role = context.role.Where(r => r.id == id).FirstOrDefault();
                    role.permission = permission;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<Permission> getAllPermissions(){
            var permissionsData = new List<Permission>();
            try
            {
                using (var context = new DrakekDB())
                {
                    permissionsData = context.permission.Select(permission => new Permission
                        {
                            id = permission.id,
                            name = permission.name
                        }).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return permissionsData;
        }

        public Permission getPermission(string id){
            Permission permission = new Permission();
            try
            {
                using (var context = new DrakekDB())
                {
                    permission = context.permission.Where(p => p.id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return permission;
        }

        public List<Permission> convertPermissionString(string permissionIds)
        {
            List<Permission> permissions = new List<Permission>();
            try
            {
                using (var context = new DrakekDB())
                {
                    List<string> permissionIdList = permissionIds.Split(',').ToList();
                    permissions = context.permission
                        .Where(p => permissionIdList.Contains(p.id))
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return permissions;
        }
    }
}