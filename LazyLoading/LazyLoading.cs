using Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoading
{
    public class LazyLoadingSimpleFactory
    {
        public LazyLoadingSimpleFactory(int choice)
        {
            ICustomer obj = Factory.Create(choice);
        }
    }
}
