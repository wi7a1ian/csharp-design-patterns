using Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoading
{
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
