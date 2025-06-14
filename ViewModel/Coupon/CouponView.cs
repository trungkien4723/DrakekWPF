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
    public partial class CouponView: UserControl
    {
        private SupportFunctions supportFunctions = new SupportFunctions();
        private CouponController couponController = new CouponController();
        private PeopleController peopleController = new PeopleController();
        public Dictionary<string, string> filters = new Dictionary<string, string>();
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
                showCouponPanel(filters);
            }
        }
        private void searchCouponButton_Click(object sender, RoutedEventArgs e)
        {
                filters = new Dictionary<string, string>
                {
                    { "name", SearchCoupon.Text },
                    { "description", SearchCoupon.Text }
                };
                
                showCouponPanel(filters);
        }
        public void showCouponPanel(Dictionary<string, string> searchFilters = null)
        {   
            if (!checkAccessPermission()){
                supportFunctions.mainWindow.show403Page();
                return;
            }
            List<Coupon> allCoupon = couponController.getAllCoupons(searchFilters).OrderByDescending(c => c.createdDate).ToList();
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
            if(CouponViewPanel.Visibility != Visibility.Visible) CouponViewPanel.Visibility = Visibility.Visible;
        }

        public void closeCouponPanel()
        {
            CouponViewPanel.Visibility = Visibility.Collapsed;
        }

        public void showUpdateCouponForm(string updateCouponId){
            CouponUpdateForm.id = updateCouponId;
            CouponUpdateForm.showForm();
        }

        public bool checkAccessPermission()
        {
            return peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_all") == true
                || peopleController.checkPeoplePermission(supportFunctions.currentUser(), "access_coupon") == true;
        }
    }
}