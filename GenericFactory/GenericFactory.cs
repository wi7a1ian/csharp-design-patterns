using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjectionContainer;

namespace DependencyInjectionContainer
{
    /// <summary>
    /// This is a creational design pattern where we create a fresh new clone / instance of an object.
    /// </summary>
    public class GenericFactory
    {
        public GenericFactory(string choice = "Customer")
        {
            var obj = Factory<ICustomer>.Create(choice);
        }
    }
}
