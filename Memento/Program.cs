using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            var memory = new List<Memento<NavState>>(); // caretaker - is responsible for the memento's safekeeping
            var appNav = new AppNavEngine(); // originator

            appNav.NavigateTo("/home");
            appNav.NavigateTo("/comments");
            appNav.NavigateTo("/comments/512");

            memory.Add(appNav.CreateMemento()); // memento pushed into caretaker
            try
            {
                appNav.NavigateTo("/comments/512/update");
                appNav.NavigateTo(string.Empty);
            }
            catch
            {
                // failed, lets restore the state
                appNav.RestoreMemento(memory.Last());
            }

            Debug.Assert(memory.Last().ToString() == appNav.CreateMemento().ToString());
            Debug.Assert(memory.Last().State.Location == appNav.CreateMemento().State.Location);
        }
    }

    /// <summary>
    /// Stores internal state T of the Originator object. 
    /// Protect against access by objects of other than the Originator.
    /// </summary>
    public class Memento<T>
    {
        private string state;

        public Memento(T state)
        {
            this.state = JsonConvert.SerializeObject(state);
        }

        public T State => JsonConvert.DeserializeObject<T>(state);

        public override string ToString() => state;
    }

    public class Memorable<T>
    {
        protected T state;

        /// <summary>
        /// Creates a memento containing a snapshot of current internal state.
        /// </summary>
        public Memento<T> CreateMemento()
        {
            return new Memento<T>(state);
        }

        /// <summary>
        /// Uses the memento to restore internal state.
        /// </summary>
        public void RestoreMemento(Memento<T> memento)
        {
            this.state = memento.State;
        }
    }

    public struct NavState
    {
        public string Location;
        public int Counter;
        public IList<string> History;
    }

    public class AppNavEngine : Memorable<NavState>
    {
        public AppNavEngine()
        {
            state = new NavState { Location = "/", Counter = 0, History = new List<string>() };
        }

        public void NavigateTo(string location)
        {
            if(string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentException("Provide correct navigation location", nameof(location));
            }

            ++state.Counter;
            state.History.Add(state.Location);
            state.Location = location;
        }
    }
}
