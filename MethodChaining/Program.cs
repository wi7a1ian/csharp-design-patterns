using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace MethodChaining
{
    class Program
    {
        static void Main(string[] args)
        {
            var recCollection = new RectangleCollection();
            recCollection.Add(new Rectangle(0, 0, 10, 10));
            recCollection.Add(new Rectangle(0, 0, 10, 10));
            recCollection.Add(new Rectangle(10, 0, 10, 90));
            recCollection.Add(new Rectangle(10, 20, 10, 10));
            recCollection.Add(new Rectangle(5, 0, 70, 10));

            var xPosList = recCollection.Where( x => x.X < 20).Select( x => x.X);

            foreach( var x in xPosList)
                Console.WriteLine($"x = {x}");
        }
    }


    public interface ISelectable<T>
    {
        IEnumerable<G> Select<G>(Func<T, G> selector);
    }

    public interface ISearchable<T>
    {
        ISelectable<T> Where(Func<T, bool> filter);
    }

    public interface IRectangleCollection : ISearchable<Rectangle>
    {
        void Add(Rectangle rec);
    }

    public class RectangleCollection : IRectangleCollection
    {
        IList<Rectangle> list = new List<Rectangle>();

        public void Add(Rectangle rec)
        {
            list.Add(rec);
        }

        public ISelectable<Rectangle> Where(Func<Rectangle, bool> filter)
        {
            return new RectangleCollectionSelector(list.Where(filter).ToList());
        }

        private class RectangleCollectionSelector : ISelectable<Rectangle>
        {
            IList<Rectangle> list;

            public RectangleCollectionSelector(IList<Rectangle> list)
            {
                this.list = list;
            }

            public IEnumerable<G> Select<G>(Func<Rectangle, G> selector)
            {
                return list.AsParallel().Select(selector);
            }
        }
    }

}
