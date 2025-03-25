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
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;
using System.Runtime.CompilerServices;

namespace drakek.ViewModel
{
    public partial class CouponUpdateForm: UserControl
    {
        public string id{get; set;}
        private Coupon couponToUpdate = new Coupon();
        private CouponController couponController = new CouponController();
        private SupportFunctions supportFunctions = new SupportFunctions();
        PeopleController peopleController = new PeopleController();
        public CouponView couponView;
        public CouponUpdateForm()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void showForm(){
            couponToUpdate = couponController.getCoupon(id);
            if(couponToUpdate != null){
                CouponName.Text = couponToUpdate.name;
                CouponValue.Text = couponToUpdate.value.ToString();
                string selectedValueType = couponToUpdate.valueType.Trim();
                CouponValueType.SelectedValue = selectedValueType;
                CouponDescription.Text = couponToUpdate.description;
                CouponStartDate.SelectedDate = couponToUpdate.startDate;
                CouponEndDate.SelectedDate = couponToUpdate.endDate;
            }    
            
            Visibility = Visibility.Visible;
            couponView.closeCouponPanel();
        }
        public void closeForm(){
            id="";
            clearForm();
            Visibility = Visibility.Collapsed;
            
            if(couponView.checkAccessPermission()) couponView.showCouponPanel();
            else supportFunctions.mainWindow.changePage("menuDashboard");
        }
        private void clearForm()
        {
            CouponName.Text = "";
            CouponValue.Text = "";
            CouponValueType.Text = "";
            CouponDescription.Text = "";
            CouponStartDate.SelectedDate = DateTime.Now;
            CouponEndDate.SelectedDate = DateTime.Now;
        }

        private void saveCouponButton_Click(object sender, RoutedEventArgs e)
        {
            if(!updateValidated()) return;
            couponToUpdate.name = CouponName.Text;
            couponToUpdate.value = Int32.Parse(CouponValue.Text);
            couponToUpdate.valueType = CouponValueType.SelectedValue.ToString();
            couponToUpdate.description = CouponDescription.Text;
            couponToUpdate.startDate = CouponStartDate.SelectedDate.Value;
            couponToUpdate.endDate = CouponEndDate.SelectedDate.Value;
            couponController.updateCoupon(couponToUpdate);
            closeForm();
        }

        private void cancelUpdateCouponButton_Click(object sender, RoutedEventArgs e)
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

        private bool updateValidated(){
            bool canUpdate = true;
            People currentUser = supportFunctions.currentUser();

            if(!peopleController.checkPeoplePermission(currentUser, "update_coupon")){
                canUpdate = false;
                ValidateMessage.Text = "You don't have permission to update";
            };
            if(peopleController.checkPeoplePermission(currentUser, "update_all") == true){
                canUpdate = true;
                ValidateMessage.Text = "";
            }
            if(string.IsNullOrEmpty(CouponName.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Name cannot be empty";
            }
            if(string.IsNullOrEmpty(CouponValue.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Coupon Value cannot be empty";
            }
            if(string.IsNullOrEmpty(CouponValueType.Text)){
                canUpdate = false;
                ValidateMessage.Text = "Value type cannot be empty";
            }
            if(CouponStartDate.SelectedDate == null){
                canUpdate = false;
                ValidateMessage.Text = "Start date cannot be empty";
            }
            if(CouponEndDate.SelectedDate == null){
                canUpdate = false;
                ValidateMessage.Text = "End date cannot be empty";
            }

            return canUpdate;
        }
    }
}