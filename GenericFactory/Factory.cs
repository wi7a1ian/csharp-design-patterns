using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DependencyInjectionContainer
{

    public static class Factory<T>
    {
        static IUnityContainer cont = null;

        public static T Create(string CustomerType)
        {
            if (cont == null)
            {
                cont = new UnityContainer();
                cont.RegisterType<ICustomer, Lead>("Lead");
                cont.RegisterType<ICustomer, Customer>("Customer");
            }

            return (T)cont.Resolve<T>(CustomerType.ToString());
        }
    }
}
