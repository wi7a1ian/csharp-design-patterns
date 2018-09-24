using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    /// <summary>
    /// Abstraction: It’s an OOP principle where we show only necessary things to the consumer.
    /// </summary>
    public interface ICustomer
    {
        string CustomerName { get; set; }
        string PhoneNumber { get; set; }
        decimal BillAmount { get; set; }
        DateTime BillDate { get; set; }
        string Address { get; set; }
        void Validate();

    }
}
