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
using Microsoft.EntityFrameworkCore;

namespace drakek.Controller
{
    public class CouponController
    {
        SupportFunctions supportFunctions = new SupportFunctions();
        public List<Coupon> getAllCoupons(Dictionary<string, string> filters = null){
            var couponsData = new List<Coupon>();
            try
            {
                using (var context = new DrakekDB())
                {
                    var query = context.coupon.AsQueryable();
                    if (filters != null){
                        IQueryable<Coupon> orQuery = context.coupon.Where(c => false);
                        foreach (var filter in filters)
                        {
                            string key = filter.Key.ToLower();
                            string value = filter.Value.ToLower();
                            switch (key){
                                case "name":
                                    orQuery = orQuery.Union(context.coupon.Where(c => c.name != null && c.name.ToLower().Contains(value)));
                                    break;
                                case "description":
                                    orQuery = orQuery.Union(context.coupon.Where(c => c.description != null && c.description.ToLower().Contains(value)));
                                    break;
                                case "valuetype":
                                    orQuery = orQuery.Union(context.coupon.Where(c => c.valueType != null && c.valueType.ToLower().Contains(value)));
                                    break;
                                case "startdate":
                                    if (DateTime.TryParse(value, out DateTime startDate)){
                                        orQuery = orQuery.Union(context.coupon.Where(c => c.startDate >= startDate));
                                    }
                                    break;
                                case "enddate":
                                    if (DateTime.TryParse(value, out DateTime endDate)){
                                        orQuery = orQuery.Union(context.coupon.Where(c => c.endDate <= endDate));
                                    }
                                    break;
                            }
                        }

                        query = orQuery;
                    }
                    couponsData = query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return couponsData;
        }

        public Coupon getCoupon(string id){
            Coupon coupon = new Coupon();
            try
            {
                using (var context = new DrakekDB())
                {
                    coupon = context.coupon.Where(c => c.id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return coupon;
        }

        public void updateCoupon(Coupon couponToUpdate)
        {   
            try
            {
                using (var context = new DrakekDB())
                {
                    if(!string.IsNullOrEmpty(couponToUpdate.id)){
                        var coupon = context.coupon.Where(c => c.id == couponToUpdate.id).FirstOrDefault();
                        if(!string.IsNullOrEmpty(couponToUpdate.name)) coupon.name = couponToUpdate.name;
                        if(couponToUpdate.value != null) coupon.value = couponToUpdate.value;
                        if(!string.IsNullOrEmpty(couponToUpdate.valueType)) coupon.valueType = couponToUpdate.valueType;
                        if(!string.IsNullOrEmpty(couponToUpdate.description)) coupon.description = couponToUpdate.description;
                        if(couponToUpdate.startDate != null) coupon.startDate = couponToUpdate.startDate;
                        if(couponToUpdate.endDate != null) coupon.endDate = couponToUpdate.endDate;
                        context.SaveChanges();
                    }
                    else{
                        couponToUpdate.id = supportFunctions.generateID("cpn", 6);
                        couponToUpdate.createdDate = DateTime.Now;
                        context.coupon.Add(couponToUpdate);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void deleteCoupon(string id)
        {
            try
            {
                using (var context = new DrakekDB())
                {
                    var coupon = context.coupon.Where(c => c.id == id).FirstOrDefault();
                    context.coupon.Remove(coupon);
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