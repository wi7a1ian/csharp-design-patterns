using System;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            var dialogMediator = new DialogMediator();
            var presenter = new DialogPresenter(dialogMediator);
            var homeViewModel = new HomeViewModel(dialogMediator);
            var dirTreeViewModel = new DirTreeViewModel(dialogMediator);
            
            for(int i = 0; i < 10; ++i)
            {
                homeViewModel.Update();
                dirTreeViewModel.Update();
            }
        }
    }

    public interface IDialogPresenter
    {
        bool ShowDialog(string title, string text);
    }

    public interface IDialogMediator 
    {
        void RegisterPresenter(IDialogPresenter presenter);
        bool ShowDialog(string text);
    }

    public class DialogMediator : IDialogMediator
    {
        private IDialogPresenter presenter;

        public void RegisterPresenter(IDialogPresenter presenter)
        {
            this.presenter = presenter;
        }

        public bool ShowDialog(string text)
        {
            if(presenter != null)
            {
                return presenter.ShowDialog("Dang!", text);
            }
            else
            {
                return false;
            }
        }
    }

    public class DialogPresenter : IDialogPresenter
    {
        public DialogPresenter(DialogMediator dialogMediator)
        {
            dialogMediator.RegisterPresenter(this);
        }

        public bool ShowDialog(string title, string text)
        {
            Console.WriteLine($"{title} {text}");
            Console.ReadKey();
            return true;
        }
    }


    public class HomeViewModel
    {
        private DialogMediator dialogMediator;

        public HomeViewModel(DialogMediator dialogMediator)
        {
            this.dialogMediator = dialogMediator;
        }

        public void Update()
        {
            var errorMessage = $"Something failed in {nameof(HomeViewModel)}";
            dialogMediator.ShowDialog(errorMessage);
        }
    }

    public class DirTreeViewModel
    {
        private DialogMediator dialogMediator;

        public DirTreeViewModel(DialogMediator dialogMediator)
        {
            this.dialogMediator = dialogMediator;
        }

        public void Update()
        {
            var errorMessage = $"Something failed in {nameof(DirTreeViewModel)}";
            dialogMediator.ShowDialog(errorMessage);
        }
    }
}
