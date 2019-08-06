using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Command
{
    class Program
    {
        static Queue<ICommand> eventQueue = new Queue<ICommand>();

        static void Main(string[] args)
        {
            eventQueue.Enqueue(new RelayCommand(() => Console.WriteLine("Hi")));
            eventQueue.Enqueue(new RelayCommand(() => Console.WriteLine("Bye"), () => false));

            var prm = new { someParam = true };
            eventQueue.AsParallel().Where( x => x.CanExecute(prm)).ForAll( x => x.Execute(prm));
        }
    }
}
