using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    public class Factory
    {
        public ICustomer Create(int CustomerType)
        {
            if (CustomerType == 0)
            {
                return new Lead();
            }
            else
            {
                return new Customer();
            }
        }
    }
}
