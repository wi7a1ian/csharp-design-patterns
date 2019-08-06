using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace Command
{
    /// <summary>
    /// https://gist.github.com/wi7a1ian/28c042b64cfd26e8e3bb5de64c0d50f6
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute)
        : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { /*CommandManager.RequerySuggested += value;*/ }
            remove { /*CommandManager.RequerySuggested -= value;*/ }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }

    public class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action execute) : this(execute, null) { }
        public RelayCommand(Action execute, Func<bool> canExecute) : base(param => execute(), param => (canExecute != null) ? canExecute() : true) { }
    }
}
