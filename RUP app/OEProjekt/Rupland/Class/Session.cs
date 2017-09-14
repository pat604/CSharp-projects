using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace CurrentProject.Class
{
    static class Session
    {

        public static void Set(object key, object value)
        {
            Application.Current.Properties.Add(key, value);
        }

        public static T Get<T>(object key)
        {
            return (T)Application.Current.Properties[key];
        }

        public static void Clear() 
        {
            Application.Current.Properties.Clear();
        }

    }
}
