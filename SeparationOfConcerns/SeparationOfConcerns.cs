using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    /// <summary>
    /// The primary work of interfaces is to decouple classes from each other.
    /// </summary>
    public class SeparationOfConcerns
    {
        public SeparationOfConcerns(int choice)
        {
            ICustomer icust = null;

            if (choice == 0)
            {
                icust = new Lead();
            }
            else
            {
                icust = new Customer();
            }

            icust.CustomerName = "Marek";
            icust.Address = "Katowice";
            icust.PhoneNumber = "123456";
            icust.BillDate = DateTime.Now;
            icust.BillAmount = 100m;
        }
    }
}
