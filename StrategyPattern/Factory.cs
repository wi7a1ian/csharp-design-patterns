using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace StrategyPattern
{

    public static class Factory
    {
        static IUnityContainer cont = null;

        static Factory()
        {
            cont = new UnityContainer();
            cont.RegisterType<ICustomer, Lead>("0", new InjectionConstructor(new CustomerAllValidation()));
            cont.RegisterType<ICustomer, Customer>("1", new InjectionConstructor(new CustomerAllValidation()));
        }

        public static ICustomer Create(int CustomerType)
        {
            return (ICustomer)cont.Resolve<ICustomer>(CustomerType.ToString());
        }
    }
}
