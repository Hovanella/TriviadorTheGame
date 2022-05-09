using System;
using System.Windows.Input;

namespace TriviadorTheGame.ViewModels.BaseViewModel
{

    public class RelayCommand : ICommand
    {
        private Action _action;
        private Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action action, Predicate<object> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public RelayCommand(Action action) : this(action, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
}