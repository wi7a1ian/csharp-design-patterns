using System;
using System.Collections.Generic;
using System.Net;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            var gamePortHub = new GamePortHub();
            var gameLobby = new GameLobby();
            using(IDisposable unsub = gamePortHub.Subscribe(gameLobby))
            {
                var conn = new Connection
                {
                    PeerId = Guid.NewGuid(),
                    PeerAddress = new IPEndPoint(1337, 80),
                    PeerName = "John"
                };

                gamePortHub.Connected(conn);
                Console.WriteLine("Karl: Hi!");
                gamePortHub.Disconnected(conn);
            }
            gamePortHub.Close();
        }
    }

    public class Connection
    {
        public Guid PeerId { get; set; }
        public IPEndPoint PeerAddress { get; set; }
        public string PeerName { get; set; }
    }

    public class GamePortHub : IObservable<Connection>
    {
        private IList<IObserver<Connection>> observers = new List<IObserver<Connection>>();

        public IDisposable Subscribe(IObserver<Connection> observer)
        {
            if(!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        public void Connected(Connection conn)
        {
            foreach (var observer in observers)
            {
                if(conn.PeerAddress.Address == IPAddress.None)
                {
                    observer.OnError(new Exception("Invalid IP address"));
                }
                else
                {
                    observer.OnNext(conn);
                }
            }
        }

        public void Disconnected(Connection conn)
        {
            foreach (var observer in observers)
            { 
                observer.OnCompleted();
            }
        }

        public void Close()
        {
            foreach (var observer in observers)
            {
                observer.OnCompleted();
            }
            observers.Clear();
        }

        private class Unsubscriber : IDisposable
        {
            private IList<IObserver<Connection>> observers;
            private IObserver<Connection> observer;

            public Unsubscriber(IList<IObserver<Connection>> observers, IObserver<Connection> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            #region IDisposable Support
            private bool disposedValue = false;

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue && disposing)
                {
                    if (observer != null && observers.Contains(observer))
                    {
                        observers.Remove(observer);
                    }
                }
                disposedValue = true;
            }

            public void Dispose()
            {
                Dispose(true);
            }
            #endregion
        }
    }

    public class GameLobby : IObserver<Connection>
    {
        private Connection lastConn;

        public void OnCompleted()
        {
            if(lastConn != null)
            {
                Console.WriteLine($"{lastConn.PeerName} has disconnected");
            }
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"Someone failed to connect: {error}");
        }

        public void OnNext(Connection value)
        {
            lastConn = value;
            if (lastConn != null)
            {
                Console.WriteLine($"{lastConn.PeerName} has connected");
            }
        }
    }

}
