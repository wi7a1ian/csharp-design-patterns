using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DependencyInjectionContainer
{

    public static class Factory
    {
        static IUnityContainer cont = null;

        static Factory()
        {
            cont = new UnityContainer();
            cont.RegisterType<ICustomer, Lead>("0");
            cont.RegisterType<ICustomer, Customer>("1");
        }

        public static ICustomer Create(int CustomerType)
        {
            return (ICustomer)cont.Resolve<ICustomer>(CustomerType.ToString());
        }
    }
}
