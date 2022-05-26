﻿using System;
using System.Windows.Input;

namespace TriviadorTheGame.ViewModels.BaseViewModel
{
    public class CaptureTheCellCommand
    {
        private readonly Action _action;
        private readonly Predicate<object> _canExecute;

        public CaptureTheCellCommand(Action action, Predicate<object> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public CaptureTheCellCommand(Action action) : this(action, null)
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
}