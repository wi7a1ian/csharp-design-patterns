using System;

namespace LazyInitialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new SimpleLazy<SomeHeavyClass>();
            var y = new SimpleLazy<SomeHeavyClass>(() => new SomeHeavyClass());
            
            //...
            // do something here
            //...

            ISomeHeavyClass xi = x.Value; // load when really needed
            ISomeHeavyClass yi = y.Value; // load when really needed

            if((xi == null || xi == null) || (yi == null || yi == null))
            {
                throw new Exception();
            }
        }
    }

    public interface ILazy<T> where T : class
    {
        T Value { get; }
    }

    public class SimpleLazy<T> 
        : ILazy<T> where T : class, new()
    {
        private T instance = null;
        private Func<T> factory = null;

        public SimpleLazy(Func<T> factory = null)
        {
            this.factory = factory;
        }

        public T Value
        {
            get
            {
                if (instance == null)
                {
                    if(factory != null)
                    {
                        instance = factory();
                    }
                    else
                    {
                        instance = new T();
                    }
                }

                return instance;
            }
        }
    }

    public class ISomeHeavyClass { }
    public class SomeHeavyClass : ISomeHeavyClass { }
}
