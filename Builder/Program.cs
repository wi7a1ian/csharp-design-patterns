using System;
using System.Drawing;
using System.Diagnostics;

namespace Builder
{
    class Program
    {
        static bool isWpfEnv = false;

        static void Main(string[] args)
        {
            IWindowBuilder builder = new imp.ConcreteWindowBuilder();
            
            builder.SetTitle("Yo")
                .ForOs(OsType.Windows)
                .SetDimensions(800,600);
            
            if (isWpfEnv) 
            {
                builder.AttachTo(Process.GetCurrentProcess());
            }

            SetSizeByDeviceDimensions(builder);

            IWindow mainWindow = builder.Create();
            mainWindow.Show();
        }

        static void SetSizeByDeviceDimensions(IWindowBuilder builder)
        {
            int width = 800;
            int height = 600;
            builder.SetDimensions(width, height);
        }
    }

    enum OsType
    {
        Windows,
        Linux
    }

    internal interface IWindow
    {
        void Show();
    }

    internal interface IWindowBuilder
    {
        IWindowBuilder SetTitle(string title);
        IWindowBuilder ForOs(OsType os);
        IWindowBuilder AttachTo(Process mainUiProcess);
        IWindowBuilder SetDimensions(int width, int height);

        IWindow Create();
    }

    namespace imp
    {
        class WindowsWindow : IWindow
        {
            internal WindowsWindow(string title, Size dimensions)
            {
                // nop
            }

            public void Show()
            {
                throw new NotImplementedException();
            }

            public void Initialize()
            {
                // nop
            }

            public void DoSomeComplexSetup()
            {
                // nop
            }
        }

        class ConcreteWindowBuilder : IWindowBuilder
        {
            string title = "Default window title";
            Size dimensions = new Size(800, 600);
            OsType os = OsType.Windows;

            public IWindowBuilder SetTitle(string title)
            {
                this.title = title;
                return this;
            }

            public IWindowBuilder ForOs(OsType os)
            {
                this.os = os;
                return this;
            }

            public IWindowBuilder AttachTo(Process mainUiProcess)
            {
                throw new NotImplementedException();
            }

            public IWindowBuilder SetDimensions(int width, int height)
            {
                dimensions = new Size(width, height);
                return this;
            }


            public IWindow Create()
            {
                if(os != OsType.Windows)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    var window = new WindowsWindow(title, dimensions /* ... */);
                    
                    window.Initialize();
                    window.DoSomeComplexSetup();

                    return window;
                }
            }
        }

    }
}
