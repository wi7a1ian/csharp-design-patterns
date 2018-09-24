using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    public class Customer : CustomerBase
    {
        public Customer(IValidationStratergy<ICustomer> _Validate)
            : base(_Validate)
        {

        }

        public ICustomer Clone()
        {
            return (ICustomer)this.MemberwiseClone();
        }
    }
}
