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
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace drakek.Controller
{
    public class PeopleController
    {
        SupportFunctions supportFunctions = new SupportFunctions();
        RoleController roleController = new RoleController();
        public List<People> getAllPeople(Dictionary<string, string> filters = null){
            var PeopleData = new List<People>();
            try
            {
                using (var context = new DrakekDB())
                {
                    var query = context.people.AsQueryable();
                    if(filters != null){
                        IQueryable<People> orQuery = context.people.Where(c => false);
                        foreach (var filter in filters)
                        {
                            string key = filter.Key.ToLower();
                            string value = filter.Value.ToLower();
                            switch (key){
                                case "name":
                                    orQuery = orQuery.Union(context.people.Where(p => p.name != null && p.name.ToLower().Contains(value)));
                                    break;
                                case "email":
                                    orQuery = orQuery.Union(context.people.Where(p => p.phone != null && p.phone.ToLower().Contains(value)));
                                    break;
                                case "phone":
                                    orQuery = orQuery.Union(context.people.Where(p => p.email != null && p.email.ToLower().Contains(value)));
                                    break;
                            }
                        }

                        query = orQuery;

                    }
                    PeopleData = query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return PeopleData;
        }

        public People getPeople(string id){
            People people = new People();
            try
            {
                using (var context = new DrakekDB())
                {
                    people = context.people.Where(p => p.id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return people;
        }

        public People getPeopleByEmail(string email){
            People People = new People();
            try
            {
                using (var context = new DrakekDB())
                {
                    People = context.people.Where(p => p.email == email).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return People;
        }

        public void updatePeople(People peopleToUpdate)
        {   
            try
            {
                using (var context = new DrakekDB())
                {
                    if(!string.IsNullOrEmpty(peopleToUpdate.id)){
                        var people = context.people.Where(p => p.id == peopleToUpdate.id).FirstOrDefault();
                        if(!string.IsNullOrEmpty(peopleToUpdate.name)) people.name = peopleToUpdate.name;
                        if(!string.IsNullOrEmpty(peopleToUpdate.role)) people.role = peopleToUpdate.role;
                        if(!string.IsNullOrEmpty(peopleToUpdate.email)) people.email = peopleToUpdate.email;
                        if(!string.IsNullOrEmpty(peopleToUpdate.phone)) people.phone = peopleToUpdate.phone;
                        if(peopleToUpdate.birthday != null) people.birthday = peopleToUpdate.birthday;
                        if(!string.IsNullOrEmpty(peopleToUpdate.password)){
                            people.password = hashPassword(peopleToUpdate.password);
                        }
                        people.image = peopleToUpdate.image;
                        context.SaveChanges();
                    }
                    else{
                        peopleToUpdate.id = supportFunctions.generateID("ppl", 5);
                        peopleToUpdate.password = !string.IsNullOrEmpty(peopleToUpdate.password) ? hashPassword(peopleToUpdate.password) : hashPassword("defaultpassword");
                        peopleToUpdate.createdDate = DateTime.Now;
                        context.people.Add(peopleToUpdate);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void deletePeople(string id)
        {
            try
            {
                using (var context = new DrakekDB())
                {
                    var People = context.people.Where(p => p.id == id).FirstOrDefault();
                    context.people.Remove(People);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void changePassword(string id, string password)
        {
            try
            {
                using (var context = new DrakekDB())
                {
                    var People = context.people.Where(p => p.id == id).FirstOrDefault();
                    People.password = hashPassword(password);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string hashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public void resendForgotPassword(string email)
        {
            // Check if the email exists in the database
            PeopleController peopleController = new PeopleController();
            People user = peopleController.getPeopleByEmail(email);

            if (user == null)
            {
                MessageBox.Show("Email not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string tempPassword = supportFunctions.generateID("",8);
            changePassword(user.id, tempPassword);

            string subject = "Password Reset";
            string body = $"Your temporary password is: {tempPassword}";
            supportFunctions.SendEmail(email, subject, body);

            MessageBox.Show("A temporary password has been sent to your email.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public List<Permission> getPeoplePermission(People people){
            Role peopleRole = roleController.getRole(people.role);
            return roleController.convertPermissionString(peopleRole.permission);
        }

        public bool checkPeoplePermission(People people, string permission){
            return getPeoplePermission(people).FirstOrDefault(per => per.id == permission) != null;
        }
    }
}