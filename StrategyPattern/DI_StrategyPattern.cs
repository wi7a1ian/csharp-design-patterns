using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    /// <summary>
    /// DIC using Unity
    /// </summary>
    public class DependencyInjectionContainer
    {
        public DependencyInjectionContainer(int choice)
        {
            ICustomer obj = Factory.Create(choice);
        }
    }
}
