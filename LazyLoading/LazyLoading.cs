using Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoading
{
    /// <summary>
    /// This is a creational design pattern where we load objects only when we need it. 
    /// The opposite of Lazy loading is eager loading.
    /// 
    /// This factory pattern class has a defect, can you guess what it is ?
    /// </summary>
    public class LazyLoadingSimpleFactory
    {
        public LazyLoadingSimpleFactory(int choice)
        {
            ICustomer obj = Factory.Create(choice);
        }
    }
}
