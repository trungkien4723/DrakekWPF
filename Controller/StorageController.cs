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
    public class StorageController
    {
        SupportFunctions supportFunctions = new SupportFunctions();
        public List<Storage> getAllStorages(Dictionary<string, string> filters = null){
            var storagesData = new List<Storage>();
            try
            {
                using (var context = new DrakekDB())
                {
                    var query = context.storage.AsQueryable();
                    if(filters != null){
                        IQueryable<Storage> orQuery = context.storage.Where(c => false);
                        foreach (var filter in filters)
                        {
                            string key = filter.Key.ToLower();
                            string value = filter.Value.ToLower();
                            switch (key){
                                case "name":
                                    orQuery = orQuery.Union(context.storage.Where(p => p.name != null && p.name.ToLower().Contains(value)));
                                    break;
                            }
                        }

                        query = orQuery;
                    }
                    storagesData = query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return storagesData;
        }

        public Storage getStorage(string id){
            Storage storage = new Storage();
            try
            {
                using (var context = new DrakekDB())
                {
                    Storage getstorage = context.storage.Where(s => s.id == id).FirstOrDefault();
                    if(getstorage != null) storage = getstorage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return storage;
        }

        public void updateStorage(Storage storageToUpdate)
        {   
            try
            {
                using (var context = new DrakekDB())
                {
                    if(!string.IsNullOrEmpty(storageToUpdate.id)){
                        var storage = context.storage.Where(s => s.id == storageToUpdate.id).FirstOrDefault();
                        if(!string.IsNullOrEmpty(storageToUpdate.name)) storage.name = storageToUpdate.name;
                        context.SaveChanges();
                    }
                    else{
                        storageToUpdate.id = supportFunctions.generateID("strg", 5);
                        storageToUpdate.createdDate = DateTime.Now;
                        context.storage.Add(storageToUpdate);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void deleteStorage(string id)
        {
            try
            {
                using (var context = new DrakekDB())
                {
                    var storage = context.storage.Where(s => s.id == id).FirstOrDefault();
                    context.storage.Remove(storage);
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