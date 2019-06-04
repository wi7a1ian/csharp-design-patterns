using System;

namespace AbstractFactory
{
    public enum WindowStyle
    {
        Normal,
        NoFrame
    }

    public interface IGuiSplashscreen
    {
        void Show(string text, TimeSpan duration);
    }

    public interface IGuiMainWindow
    {
        void Show();
    }

    public interface IGuiFactory
    { 
        IGuiSplashscreen CreateSplashscreen();
        IGuiMainWindow CreateMainWindow(WindowStyle style);

    };
    
    public class GameEngine
    {
        public GameEngine(IGuiFactory uiFactory)
        {
            IGuiSplashscreen splash = uiFactory.CreateSplashscreen();
            splash.Show("Loading...", TimeSpan.FromSeconds(5));

            IGuiMainWindow window = uiFactory.CreateMainWindow(WindowStyle.NoFrame);
            window.Show();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var engine = new GameEngine(new imps.WpfGuiFactory());
            //...
        }
    }

    namespace imps
    {
        class Splashscreen : IGuiSplashscreen
        {
            public void Show(string text, TimeSpan duration)
            {
                throw new NotImplementedException();
            }
        }

        public class WpfGuiFactory : IGuiFactory
        {
            class WpfMainWindow : IGuiMainWindow
            {
                public WpfMainWindow(WindowStyle style)
                {
                    // nop
                }

                public void Show()
                {
                    throw new NotImplementedException();
                }
            }

            public IGuiMainWindow CreateMainWindow(WindowStyle style)
            {
                return new WpfMainWindow(style);
            }

            public IGuiSplashscreen CreateSplashscreen()
            {
                return new Splashscreen();
            }
        }
    }
}
