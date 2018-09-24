using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    public class Lead : CustomerBase
    {
        public Lead(IValidationStratergy<ICustomer> _Validate)
            : base(_Validate)
        {

        }

        public override void Validate()
        {
            base.Validate();
        }
    }
}
