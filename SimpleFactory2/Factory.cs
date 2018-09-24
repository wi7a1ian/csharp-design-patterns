using Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory2
{
    /// <summary>
    /// In the above architecture the factory class will perform very badly if we have 
    /// lot of concrete objects tomorrow and if we are creating factory instances again and 
    /// again it will lead to lot of memory consumption.
    /// 
    /// So if we can just have ONE INSTANCE of the factory class with all concrete objects 
    /// loaded once that would really boost up the performance.
    /// </summary>
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
            return customers[CustomerType];
        }
    }
}
