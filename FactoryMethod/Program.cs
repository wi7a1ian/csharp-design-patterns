using System;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseUiFactory uiFactory = new Windows10UiFactory();
            IWindow window = uiFactory.GetWindow(WindowStyle.Borderless);
        }
    }

    public enum WindowStyle
    {
        Normal,
        Borderless,
        Fullscreen,
        Multiwindow
    }

    public interface IWindow { }
    public class WindowsmMeWindow : IWindow{ }
    public class WindowsXpWindow : IWindow{ }
    public class Windows10Window : IWindow{ }

    public abstract class BaseUiFactory
    {
        public IWindow GetWindow(WindowStyle style)
        {
            var window = CreateWindowInstance(style);

            if(window == null)
            {
                // fallback
                window = CreateWindowInstance(WindowStyle.Normal);
            }

            return window;
        }

        protected abstract IWindow CreateWindowInstance(WindowStyle style);
    }

    public class WindowsMeUiFactory : BaseUiFactory
    {
        private static IWindow windowInstance = new WindowsmMeWindow();
        protected override IWindow CreateWindowInstance(WindowStyle style)
        {
            return windowInstance; // you need to reuse same window
        }
    }

    public class WindowsXpUiFactory : BaseUiFactory
    {
        protected override IWindow CreateWindowInstance(WindowStyle style)
        {
            return new WindowsXpWindow();
        }
    }

    public class Windows10UiFactory : BaseUiFactory
    {
        protected override IWindow CreateWindowInstance(WindowStyle style)
        {
            IWindow window = null;
            switch (style)
            {
                case WindowStyle.Normal:
                case WindowStyle.Borderless:
                    window = new Windows10Window();
                    break;
                case WindowStyle.Fullscreen:
                case WindowStyle.Multiwindow:
                default:
                    break;
            }
            return window;
        }
    }
}
