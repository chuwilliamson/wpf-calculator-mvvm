using System;
using System.Windows.Input;

namespace WpfCalculator.ViewModel
{
    // RelayCommand class: implements ICommand to handle button click commands in the View
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute; // Action to execute when command is invoked
        private readonly Predicate<object> _canExecute; // Predicate to determine if the command can execute

        // Constructor: initializes the command with execute logic and optionally a condition for execution
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // Determines whether the command can execute (returns true if no condition is provided)
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        // Executes the action associated with the command
        public void Execute(object parameter) => _execute(parameter);

        // Event that is raised when the execution status changes
        public event EventHandler CanExecuteChanged;

        // Raises the CanExecuteChanged event to notify the View to reevaluate if the command can execute
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
