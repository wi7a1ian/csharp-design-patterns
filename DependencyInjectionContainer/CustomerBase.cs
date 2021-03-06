﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    /// <summary>
    /// Abstract classes – Do not allow to instantiate "half-defined thing"
    /// </summary>
    public abstract class CustomerBase : ICustomer
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public string Address { get; set; }
        public abstract void Validate();
     
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
