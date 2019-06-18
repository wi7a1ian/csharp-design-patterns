using System;

namespace Dispose
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var obj = new SomeClassThatNeedToDisposeUnmanagedStuff())
            {
                Console.WriteLine("Lets Kill It!");
            }

            //{ // Same as:
            //    var obj = new SomeClassThatNeedToDisposeUnmanagedStuff();
            //    try
            //    {
            //        Console.WriteLine("Lets Kill It!");
            //    }
            //    finally
            //    {
            //        if (obj != null)
            //            ((IDisposable)obj).Dispose();
            //    }
            //}
        }

        //If you do override the Object.Finalize method, your class should implement the following pattern.
        class SomeClassThatNeedToDisposeUnmanagedStuff : IDisposable
        {
            // Flag: Has Dispose already been called? 
            bool disposed = false;

            // Public implementation of Dispose pattern callable by consumers. 
            public void Dispose()
            {
                Dispose(true); // Free resources

                // We don't need the destructor because our resources are already freed.
                GC.SuppressFinalize(this);
            }

            // Protected implementation of Dispose pattern. 
            protected virtual void Dispose(bool disposing)
            {
                if (disposed)
                    return;

                if (disposing)
                {
                    //
                    // Free any other managed objects here. 
                    // TODO
                }

                // 
                // Free any unmanaged objects here. 
                // TODO

                disposed = true;
            }

            // Override Object.Finalize
            // This is the same as override void Finalize(){try{...}finally{base.Finalize();}}}
            // Also, if an object has a destructor it has to pass through a finalization queue before it is destroyed.
            ~SomeClassThatNeedToDisposeUnmanagedStuff()
            {
                Dispose(false); // We already freed all the resources
            }
        }
    }
}
