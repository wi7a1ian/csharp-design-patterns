using Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceIfWithPolymorphism
{
    /// <summary>
    /// In Polymorphism IF can be replaced with a dynamic polymorphic collection.
    /// </summary>
    public class Factory
    {
        private List<ICustomer> customers = new List<ICustomer>();

        public Factory()
        {
            customers.Add(new Lead());
            customers.Add(new Customer());
        }

        public ICustomer Create(int CustomerType)
        {
            return customers[CustomerType];
        }
    }
}
