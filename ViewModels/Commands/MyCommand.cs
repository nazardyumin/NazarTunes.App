using System;
using System.Windows.Input;

namespace NazarTunes.ViewModels.Commands
{
    public class MyCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?> _canExecute;

        public MyCommand(Action<object?> execute, Predicate<object?> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
