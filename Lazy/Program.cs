using System;

namespace LazyInitialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new SimpleLazy<SomeHeavyClass>();
            
            //...
            // do something here
            //...

            ISomeHeavyClass y = x.Value; // load when really needed

            if(x == null || y == null)
            {
                throw new Exception();
            }
        }
    }

    public interface ILazy<T> where T : class
    {
        T Value { get; }
    }

    public class SimpleLazy<T> : ILazy<T> where T : class, new()
    {
        private T instance = null;

        public T Value 
        { 
            get 
            { 
                if (instance == null)
                {
                    instance = new T();
                }

                return instance;
            }
        }
    }

    public class ISomeHeavyClass { }
    public class SomeHeavyClass : ISomeHeavyClass { }
}
