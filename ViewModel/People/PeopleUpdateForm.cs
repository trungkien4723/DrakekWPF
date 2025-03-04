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
    public partial class PeopleUpdateForm: UserControl
    {
        public string id{get; set;}
        public List<Role> roles{get; set;}
        public string selectedRole{get; set;} 

        private PeopleController peopleController = new PeopleController();
        private SupportFunctions supportFunctions = new SupportFunctions();
        private RoleController roleController = new RoleController();
        public PeopleView peopleView;
        public PeopleUpdateForm()
        {
            InitializeComponent();
            roles = roleController.getAllRoles();
            DataContext = this;
        }

        public void showForm(){
            
            People people = peopleController.getPeople(id);
            if(people != null){
                PeopleName.Text = people.name;
                selectedRole = people.role;
                PeopleEmail.Text = people.email;
                PeoplePhone.Text = people.phone;
                PeopleBirthday.SelectedDate = people.birthday;
            }
            Visibility = Visibility.Visible;
            peopleView.closePeoplePanel();
        }
        public void closeForm(){
            id="";
            clearForm();
            Visibility = Visibility.Collapsed;
            peopleView.showPeoplePanel();
        }
        private void clearForm()
        {
                PeopleName.Text = "";
                selectedRole = "";
                PeopleEmail.Text = "";
                PeoplePhone.Text = "";
                PeopleBirthday.SelectedDate = null;
                PeoplePassword.Password = "";
        }

        private void savePeopleButtonClick(object sender, RoutedEventArgs e)
        {
            string name = PeopleName.Text;
            string role = selectedRole;
            string email = PeopleEmail.Text;
            string phone = PeoplePhone.Text;
            DateTime birthday = PeopleBirthday.SelectedDate.Value;
            string password = PeoplePassword.Password;
            peopleController.updatePeople(id, name, role, email, phone, birthday, password);
            closeForm();
        }

        private void cancelUpdatePeopleButtonClick(object sender, RoutedEventArgs e)
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