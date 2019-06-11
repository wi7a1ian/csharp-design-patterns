using System;
using System.Collections.Generic;

namespace ServiceLocator
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceLocator loc = new SimpleServiceLocator();
            loc.Register<IDummy, Dummy>();
            loc.Register<IDummyWithCtor, DummyWithCtor>();

            for( int i = 0; i < 1000; ++i)
            {
                var x = loc.Resolve<IDummy>();
                if (x == null || !(x is Dummy)) throw new Exception("Service locator failed");

                var y = loc.Resolve<IDummyWithCtor>("some param");
                if (y == null || !(y is DummyWithCtor)) throw new Exception("Service locator failed");
            }
        }
    }

    public interface IServiceLocator
    {
        void Register<T, TImp>();
        T Resolve<T>(params object[] args)  where T: class;
    }

    class SimpleServiceLocator : IServiceLocator
    {
        private readonly IDictionary<Type, Type> map = new Dictionary<Type, Type>();
        public void Register<T, TImp>()
        {
            map.Add(typeof(T), typeof(TImp));
        }

        public T Resolve<T>(params object[] args) where T : class
        {
            T imp = null;
            if (map.ContainsKey(typeof(T)))
            {
                imp = Activator.CreateInstance(map[typeof(T)], args) as T;
            }
            return imp;
        }
    }

    interface IDummy { }
    class Dummy : IDummy { }

    interface IDummyWithCtor { }
    class DummyWithCtor : IDummyWithCtor {
        public DummyWithCtor(string name){ } 
    }


}
