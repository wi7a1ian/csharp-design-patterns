using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NullObject
{
    class Program
    {
        static void Main(string[] args)
        {
            var recCollection = new RectangleCollection();
            recCollection.Add(new Rectangle(0, 0, 10, 10));
            recCollection.Add(new Rectangle(10, 20, 10, 10));
            recCollection.Add(new Rectangle(5, 0, 70, 10));

            FindXPositionsLowerThan10(recCollection);
            FindXPositionsLowerThan10(new RectangleCollection());
            FindXPositionsLowerThan10(RectangleCollection.Empty);
        }

        static void FindXPositionsLowerThan10(IRectangleCollection recCollection)
        {
            var xPosList = recCollection.Where(x => x.X < 10).Select(x => x.X);

            foreach (var x in xPosList)
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
        public static readonly IRectangleCollection Empty = new NullISearchableCollection();

        IList<Rectangle> list = new List<Rectangle>();

        public void Add(Rectangle rec)
        {
            list.Add(rec);
        }

        public ISelectable<Rectangle> Where(Func<Rectangle, bool> filter)
        {
            if(list.Any())
            {
                return new RectangleCollectionSelector(list.Where(filter).ToList());
            }
            else
            {
                return new NullSelectableCollection();
            }
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

        private sealed class NullISearchableCollection : IRectangleCollection
        {
            public void Add(Rectangle rec) { }

            public ISelectable<Rectangle> Where(Func<Rectangle, bool> filter)
            {
                return new NullSelectableCollection();
            }
        }

        private sealed class NullSelectableCollection : ISelectable<Rectangle>
        {
            public IEnumerable<G> Select<G>(Func<Rectangle, G> selector)
            {
                return new List<G>();
            }
        }
    }
}
