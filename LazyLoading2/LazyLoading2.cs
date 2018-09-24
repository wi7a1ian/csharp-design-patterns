using Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoading2
{

    /// <summary>
    /// Using Lazy keyword.
    /// 
    /// This factory pattern class has a defect, can you guess what it is ?
    /// </summary>
    public class LazyLoading2
    {
        public LazyLoading2(int choice)
        {
            ICustomer obj = Factory.Create(choice);
        }
    }
}
