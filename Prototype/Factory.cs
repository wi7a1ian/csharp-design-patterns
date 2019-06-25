using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern
{

    public static class Factory
    {
        private static List<ICustomer> customers = new List<ICustomer>();

        static Factory()
        {
            customers.Add(new Lead());
            customers.Add(new Customer());
        }

        public static ICustomer Create(int CustomerType)
        {
            return customers[CustomerType].Clone() as ICustomer;
        }
    }
}
