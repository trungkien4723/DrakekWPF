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
    public class PeopleController
    {
        SupportFunctions supportFunctions = new SupportFunctions();
        public List<People> getAllPeople(){
            var PeoplesData = new List<People>();
            try
            {
                using (var context = new DrakekDB())
                {
                    PeoplesData = context.people.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return PeoplesData;
        }

        public People getPeople(string id){
            People People = new People();
            try
            {
                using (var context = new DrakekDB())
                {
                    People = context.people.Where(p => p.id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return People;
        }

        public void updatePeople(string id, string name, string role, string email, string phone, DateTime birthday, string password = "")
        {   
            try
            {
                using (var context = new DrakekDB())
                {
                    if(!string.IsNullOrEmpty(id)){
                        var people = context.people.Where(p => p.id == id).FirstOrDefault();
                        people.name = name;
                        people.role = role;
                        people.email = email;
                        people.phone = phone;
                        people.birthday = birthday;
                        if(!string.IsNullOrEmpty(password)){
                            people.password = password;
                        }
                        context.SaveChanges();
                    }
                    else{
                            People newPeople = new People
                            {
                                id = supportFunctions.generateID("ppl", 5),
                                name = name,
                                role = role,
                                email = email,
                                phone = phone,
                                birthday = birthday,
                                password = !string.IsNullOrEmpty(password) ? password : "defaultpassword",
                                createdDate = DateTime.Now
                            };

                            context.people.Add(newPeople);
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
                    People.password = password;
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