using Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoading2
{
    public static class Factory
    {
        private static Lazy<List<ICustomer>> customers = null;

        static Factory()
        {
            customers = new Lazy<List<ICustomer>>(() => LoadCustomers());
        }

        private static List<ICustomer> LoadCustomers()
        {
            List<ICustomer> custs = new List<ICustomer>();
            custs.Add(new Lead());
            custs.Add(new Customer());
            return custs;
        }

        public static ICustomer Create(int CustomerType)
        {
            return customers.Value[CustomerType];
        }
    }
}
