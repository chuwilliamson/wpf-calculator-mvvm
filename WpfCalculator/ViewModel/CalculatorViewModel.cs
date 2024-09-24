using System;
using System.ComponentModel;
using System.Windows.Input;
using WpfCalculator.Model;

namespace WpfCalculator.ViewModel
{
    // The CalculatorViewModel class implements INotifyPropertyChanged to support data binding
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        // Private fields to store input values and result
        private double _number1;
        private double _number2;
        private double _result;

        // Reference to the Calculator model, which handles the arithmetic logic
        private readonly Calculator _calculator;

        // Public properties for Number1, Number2, and Result, which the View (UI) binds to
        public double Number1
        {
            get => _number1;
            set
            {
                _number1 = value;
                OnPropertyChanged(nameof(Number1));
                // Update the commands when Number1 changes
                ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
                ((RelayCommand)SubtractCommand).RaiseCanExecuteChanged();
                ((RelayCommand)MultiplyCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DivideCommand).RaiseCanExecuteChanged();
            }
        }

        public double Number2
        {
            get => _number2;
            set
            {
                _number2 = value;
                OnPropertyChanged(nameof(Number2));
                // Update the commands when Number2 changes
                ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
                ((RelayCommand)SubtractCommand).RaiseCanExecuteChanged();
                ((RelayCommand)MultiplyCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DivideCommand).RaiseCanExecuteChanged();
            }
        }

        public double Result
        {
            get => _result; // Getter returns the current value of _result
            set
            {
                _result = value; // Setter updates _result
                OnPropertyChanged(nameof(Result)); // Notifies the View that the property has changed
            }
        }

        // ICommand properties for the calculator operations, bound to buttons in the View
        public ICommand AddCommand { get; }
        public ICommand SubtractCommand { get; }
        public ICommand MultiplyCommand { get; }
        public ICommand DivideCommand { get; }

        // Constructor: initializes the Calculator model and the commands for each operation
        public CalculatorViewModel()
        {
            _calculator = new Calculator(); // Instantiate the Calculator model

            // Initialize commands with corresponding methods for arithmetic operations
            AddCommand = new RelayCommand(_ => Add(), _ => CanCalculate());
            SubtractCommand = new RelayCommand(_ => Subtract(), _ => CanCalculate());
            MultiplyCommand = new RelayCommand(_ => Multiply(), _ => CanCalculate());
            DivideCommand = new RelayCommand(_ => Divide(), _ => CanCalculate());
        }

        // Methods that call the respective Calculator model methods to perform calculations
        private void Add() => Result = _calculator.Add(Number1, Number2);
        private void Subtract() => Result = _calculator.Subtract(Number1, Number2);
        private void Multiply() => Result = _calculator.Multiply(Number1, Number2);
        private void Divide() => Result = _calculator.Divide(Number1, Number2);

        // A helper method that determines whether a calculation can be performed
        // (In this case, we allow calculations if either number is non-zero)
        private bool CanCalculate() => Number1 != 0 || Number2 != 0;
        
        
       // private bool CanCalculate() => true;//always return true to enable the buttons

        // Event that is raised when a property value changes (used for data binding)
        public event PropertyChangedEventHandler PropertyChanged;

        // Helper method to raise the PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

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
