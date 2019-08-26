using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            var dialogMediator = new AsyncDialogMediator();
            var presenter = new DialogPresenter(dialogMediator);
            var homeViewModel = new HomeViewModel(dialogMediator);
            var dirTreeViewModel = new DirTreeViewModel(dialogMediator);
            
            Task.Run( async () =>
            { 
                for(int i = 0; i < 10; ++i)
                {
                    await homeViewModel.Update();
                    await dirTreeViewModel.Update();
                }
            }).Wait();
        }
    }

    public interface IDialogPresenter
    {
        bool ShowDialog(string title, string text);
    }

    public interface IDialogMediator 
    {
        void RegisterPresenter(IDialogPresenter presenter);
        Task<bool> ShowDialog(string text);
    }

    public class AsyncDialogMediator : IDialogMediator
    {
        private IDialogPresenter presenter;

        public void RegisterPresenter(IDialogPresenter presenter)
        {
            this.presenter = presenter;
        }

        public async Task<bool> ShowDialog(string text)
        {
            if(presenter != null)
            {
                return await Task.Run( () => presenter.ShowDialog("Dang!", text));
            }
            else
            {
                return false;
            }
        }
    }

    public class DialogPresenter : IDialogPresenter
    {
        public DialogPresenter(AsyncDialogMediator dialogMediator)
        {
            dialogMediator.RegisterPresenter(this);
        }

        public bool ShowDialog(string title, string text)
        {
            Console.WriteLine($"[{title} {text}]");
            Console.ReadKey();
            return true;
        }
    }

    public class HomeViewModel
    {
        private AsyncDialogMediator dialogMediator;

        public HomeViewModel(AsyncDialogMediator dialogMediator)
        {
            this.dialogMediator = dialogMediator;
        }

        public async Task Update()
        {
            var errorMessage = $"Something failed in {nameof(HomeViewModel)}";
            await dialogMediator.ShowDialog(errorMessage);
        }
    }

    public class DirTreeViewModel
    {
        private AsyncDialogMediator dialogMediator;

        public DirTreeViewModel(AsyncDialogMediator dialogMediator)
        {
            this.dialogMediator = dialogMediator;
        }

        public async Task Update()
        {
            var errorMessage = $"Something failed in {nameof(DirTreeViewModel)}";
            var dialogTask = dialogMediator.ShowDialog(errorMessage);

            Console.WriteLine("Yo");

            await dialogTask;
        }
    }
}
