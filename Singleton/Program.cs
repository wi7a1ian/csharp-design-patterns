using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            SampleSingleton.Instance.Hello();
        }
    }

    public class SampleSingleton
    {
        private static readonly SampleSingleton _instance = new SampleSingleton();
        public static SampleSingleton Instance => _instance;

        private SampleSingleton()
        {
            // nop
        }

        public void Hello()
        {
            // nop
        }
    }
}
