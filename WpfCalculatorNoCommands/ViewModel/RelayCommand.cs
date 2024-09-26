using System;
using System.Windows.Input;

namespace WpfCalculatorSingleTextBox.ViewModel
{
    // Implementation of RelayCommand for binding commands to buttons in the UI
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        // Constructor to define the action and condition for the command
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // Check if the command can be executed
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        // Execute the command action
        public void Execute(object parameter) => _execute(parameter);

        // Event to handle command changes
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
