using Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoading
{
    /// <summary>
    /// Loading the objects on demand - If you see for now the objects are loaded irrespective 
    /// you want them or not. How about we just load Just-in-time, in other words when we 
    /// want the objects we load them.
    /// </summary>
    public static class Factory
    {
        private static List<ICustomer> customers = null;

        private static void LoadCustomers()
        {
            customers = new List<ICustomer>();
            customers.Add(new Lead());
            customers.Add(new Customer());
        }

        public static ICustomer Create(int CustomerType)
        {
            if (customers == null)
            {
                LoadCustomers();
            }
            return customers[CustomerType];
        }
    }
}
