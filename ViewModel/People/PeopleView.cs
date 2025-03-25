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
    public partial class PeopleView: UserControl
    {
        private PeopleController peopleController = new PeopleController();
        private RoleController roleController = new RoleController();
        private SupportFunctions supportFunctions = new SupportFunctions();
        public PeopleView()
        {
            InitializeComponent();
            showPeoplePanel();
            PeopleUpdateForm.peopleView = this;
        }

        private void addPeopleButtonClick(object sender, RoutedEventArgs e)
        {
            showUpdatePeopleForm("");
        }

        private void editPeopleButtonClick(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            People people = peopleController.getPeople(context.id);

            if(people == null){
                MessageBox.Show("Not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            showUpdatePeopleForm(people.id);
        }

        private void deletePeopleButtonClick(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            People people = peopleController.getPeople(context.id);

            if(people == null){
                MessageBox.Show("Not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PeopleUpdateForm.id = people.id;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                peopleController.deletePeople(people.id);
                showPeoplePanel();
            }
        }
        public void showPeoplePanel()
        {   
            if (!checkAccessPermission()){
                supportFunctions.mainWindow.show403Page();
                return;
            }
            List<People> allPeople = peopleController.getAllPeople();
            var allPeopleData = allPeople.Select((people, i) => new
            {
                index = i + 1,
                people.id,
                people.name,
                role = roleController.getRole(people.role).name,
                people.email,
                people.phone,
                birthday = people.birthday.ToString("d"),
                people.createdDate,
                people.image
            }).ToList();
            PeopleTable.ItemsSource = allPeopleData;
            PeopleViewPanel.Visibility = Visibility.Visible;
        }

        public void closePeoplePanel()
        {
            PeopleViewPanel.Visibility = Visibility.Collapsed;
        }

        public void showUpdatePeopleForm(string updatePeopleId){
            PeopleUpdateForm.id = updatePeopleId;
            PeopleUpdateForm.showForm();
        }

        public bool checkAccessPermission()
        {
            return peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_all") == true
                || peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_people") == true;
        }
    }
}