using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern
{
    /// <summary>
    /// This is a creational design pattern where we create a fresh new clone / instance of an object.
    /// </summary>
    public class PrototypePattern
    {
        public PrototypePattern(int choice)
        {
            ICustomer obj = Factory.Create(choice);
        }
    }
}
