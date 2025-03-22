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
            couponView.showCouponPanel();
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
    }
}