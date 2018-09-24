using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    /// <summary>
    /// “NEW” keyword is the main culprit for tight coupling .
    /// PIC Pattern for decoupling  - Acronym of PIC is “Polymorphism + Interfaces + Centralizing object creation”
    /// </summary>
    public class SimpleFactory
    {
        public SimpleFactory(int choice)
        {
            ICustomer icust = null;
            Factory obj = new Factory();
            icust = obj.Create(choice);
        }
    }
}
