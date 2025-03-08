using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Text.RegularExpressions;
using drakek.Controller;

namespace Drakek.Controller
{
    public class SupportFunctions
    {
        public void previewTextInput(object sender, TextCompositionEventArgs e, string allowType)
        {
            e.Handled = !IsTextAllowed(e.Text, allowType);
        }

        public void previewTextPasting(object sender, DataObjectPastingEventArgs e, string allowType)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text, allowType))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private static bool IsTextAllowed(string text, string allowType)
        {
            switch(allowType.ToLower())
            {
                case "number":
                    Regex regex = new Regex($"[^0-9]+");
                    return !regex.IsMatch(text);
                    
                case "text":
                    regex = new Regex($"[^A-z]+");
                    return !regex.IsMatch(text);
                    
            }
            return false;            
        }

        public string generateID(string prefix, int length)
        {
            return prefix + Guid.NewGuid().ToString("N").Substring(0, length);
        }
    }
}