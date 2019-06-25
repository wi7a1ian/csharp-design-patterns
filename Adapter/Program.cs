using System;
using System.Drawing;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            var externalProcessor = new SomeExternalRectangleProcessor();
            var ourAdapter = new SomeExternalRecProcAdapter(externalProcessor);
            
            Process(ourAdapter);
        }

        static void Process(ISomeInternalInterface recProcessor)
        {
            // nop
        }
    }


    public interface ISomeExternalInterface
    {
        int ProcessRectangle(Rectangle rec);
    }

    public class SomeExternalRectangleProcessor : ISomeExternalInterface
    {
        public int ProcessRectangle(Rectangle rec)
        {
            return 0;
        }
    }

    public interface ISomeInternalInterface
    {
        void ProcessRectangle(int x, int y, int width, int height);
    }

    public class SomeExternalRecProcAdapter : ISomeInternalInterface
    {
        private readonly ISomeExternalInterface processor;
        public SomeExternalRecProcAdapter(ISomeExternalInterface processor)
        {
            this.processor = processor;
        }

        public void ProcessRectangle(int x, int y, int width, int height)
        {
            if(processor.ProcessRectangle(new Rectangle(x, y, width, height)) != 0)
            {
                throw new Exception("fail");
            }
        }
    }
}
