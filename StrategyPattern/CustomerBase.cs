using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
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

        private IValidationStratergy<ICustomer> _ValidationType = null;

        public CustomerBase(IValidationStratergy<ICustomer> _Validate)
        {
            _ValidationType = _Validate;
        }

        public IValidationStratergy<ICustomer> ValidationType
        {
            get
            {
                return _ValidationType;
            }
            set
            {
                _ValidationType = value;
            }
        }

        public virtual void Validate()
        {
            _ValidationType.Validate(this);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
