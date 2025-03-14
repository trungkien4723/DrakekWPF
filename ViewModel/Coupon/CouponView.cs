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
    public partial class CouponView: UserControl
    {
        private CouponController couponController = new CouponController();
        public CouponView()
        {
            InitializeComponent();
            showCouponPanel();
            CouponUpdateForm.couponView = this;
        }

        private void addCouponButtonClick(object sender, RoutedEventArgs e)
        {
            showUpdateCouponForm("");
        }

        private void editCouponButton_Click(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Coupon coupon = couponController.getCoupon(context.id);

            if(coupon == null){
                MessageBox.Show("Not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            showUpdateCouponForm(coupon.id);
        }

        private void deleteCouponButton_Click(object sender, RoutedEventArgs e)
        {
            var context = (dynamic)((Button)sender).DataContext;
            Coupon coupon = couponController.getCoupon(context.id);

            if(coupon == null){
                MessageBox.Show("Not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                couponController.deleteCoupon(coupon.id);
                showCouponPanel();
            }
        }
        public void showCouponPanel()
        {   
            List<Coupon> allCoupon = couponController.getAllCoupons();
            var allCouponData = allCoupon.Select((coupon, i) => new
            {
                index = i + 1,
                coupon.id,
                coupon.name,
                coupon.value,
                coupon.valueType,
                coupon.description,
                createdDate = coupon.createdDate.ToString("d"),
                startDate = coupon.startDate.ToString(),
                endDate = coupon.endDate.ToString()
            }).ToList();
            CouponTable.ItemsSource = allCouponData;
            CouponViewPanel.Visibility = Visibility.Visible;
        }

        public void closeCouponPanel()
        {
            CouponViewPanel.Visibility = Visibility.Collapsed;
        }

        public void showUpdateCouponForm(string updateCouponId){
            CouponUpdateForm.id = updateCouponId;
            CouponUpdateForm.showForm();
        }
    }
}