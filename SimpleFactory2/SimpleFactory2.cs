using Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory2
{
    /// <summary>
    /// By centralizing object creation and returning a generic interface reference helps 
    /// to minimize changes when changes are applied to the application. This falls in creation category.
    /// </summary>
    public class SimpleFactory2
    {
        public SimpleFactory2(int choice)
        {
            ICustomer obj = Factory.Create(choice);
        }
    }
}
