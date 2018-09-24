using Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceIfWithPolymorphism
{
    /// <summary>
    /// If there is polymorphism and if you see lots of IF conditions that means polymorphism benefit is not exploited
    /// </summary>
    public class ReplaceIfWithPolymorphism
    {
        public ReplaceIfWithPolymorphism(int choice)
        {
            ICustomer icust = null;
            Factory obj = new Factory();
            icust = obj.Create(choice);
        }
    }
}
