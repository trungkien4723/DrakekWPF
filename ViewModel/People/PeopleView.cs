using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using drakek.Controller;
using System.Windows;
using drakek.Model;

namespace drakek.ViewModel
{
    public partial class PeopleView: UserControl
    {
        private PeopleController peopleController = new PeopleController();
        public PeopleView()
        {
            InitializeComponent();
            showPeoplePanel();
            PeopleUpdateForm.peopleView = this;
        }

        private void addPeopleButtonClick(object sender, RoutedEventArgs e)
        {
            PeopleUpdateForm.id = "";
            PeopleViewPanel.Visibility = Visibility.Collapsed;
            PeopleUpdateForm.Visibility = Visibility.Visible;
        }

        private void editPeopleButtonClick(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            People people = peopleController.getPeople(context.id);

            if(people == null){
                MessageBox.Show("Not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            PeopleUpdateForm.id = people.id;
            PeopleUpdateForm.showForm();
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
            List<People> allPeople = peopleController.getAllPeople();
            var allPeopleData = allPeople.Select((people, i) => new
            {
                index = i + 1,
                people.id,
                people.name,
                people.role,
                people.email,
                people.phone,
                people.birthday,
                people.createdDate
            }).ToList();
            PeopleTable.ItemsSource = allPeopleData;
            PeopleViewPanel.Visibility = Visibility.Visible;
        }

        public void closePeoplePanel()
        {
            PeopleViewPanel.Visibility = Visibility.Collapsed;
        }
    }
}