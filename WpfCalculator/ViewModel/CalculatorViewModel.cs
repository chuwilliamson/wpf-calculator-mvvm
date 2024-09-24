using System;
using System.ComponentModel;
using System.Windows.Input;
using WpfCalculator.Model;

namespace WpfCalculator.ViewModel
{
    // The CalculatorViewModel class implements INotifyPropertyChanged to support data binding
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _number1 = ""; // Stores the first number input
        private string _number2 = ""; // Stores the second number input
        private double _result; // Stores the result of the calculation
        private string _selectedOperation = ""; // Stores the current selected operation (Add, Subtract, etc.)
        private bool _isNumber1Active = true; // Indicates whether we are entering Number1 or Number2
        private readonly Calculator _calculator; // Calculator model that performs arithmetic operations

        // Public property to bind Number1 to the View
        public string Number1
        {
            get => _number1;
            set
            {
                _number1 = value;
                OnPropertyChanged(nameof(Number1)); // Notify view when Number1 changes
            }
        }

        // Public property to bind Number2 to the View
        public string Number2
        {
            get => _number2;
            set
            {
                _number2 = value;
                OnPropertyChanged(nameof(Number2)); // Notify view when Number2 changes
            }
        }

        // Public property to bind the Result to the View
        public double Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result)); // Notify view when the Result changes
            }
        }

        // ICommand properties for arithmetic operations and equals functionality
        public ICommand OperationCommand { get; } // Command to handle operation button clicks
        public ICommand EqualsCommand { get; } // Command to handle equals button click
        public ICommand ClearCommand { get; } // Command to handle clearing the inputs
        public ICommand NumberButtonCommand { get; } // Command to handle number button clicks (0-9)

        // Constructor: Initializes the Calculator model and commands
        public CalculatorViewModel()
        {
            _calculator = new Calculator(); // Instantiate the Calculator model

            // Initialize the commands
            OperationCommand = new RelayCommand(SetOperation); // Bind operation buttons (Add, Subtract, etc.)
            EqualsCommand = new RelayCommand(ExecuteOperation); // Bind equals button
            ClearCommand = new RelayCommand(Clear); // Bind clear button
            NumberButtonCommand = new RelayCommand(OnNumberButtonClick); // Bind number buttons
        }

        // Sets the current operation (Add, Subtract, Multiply, Divide) based on the user's choice
        private void SetOperation(object parameter)
        {
            if (parameter != null)
            {
                _selectedOperation = parameter.ToString(); // Store the selected operation as a string
                _isNumber1Active = false; // After choosing an operation, switch to entering Number2
            }
        }

        // Executes the stored operation when the Equals button is pressed
        private void ExecuteOperation(object parameter)
        {
            // Ensure that a valid operation has been selected and Number2 is provided
            if (!string.IsNullOrEmpty(_selectedOperation) && !string.IsNullOrEmpty(Number2))
            {
                // Perform the selected operation using the Calculator model
                switch (_selectedOperation)
                {
                    case "Add":
                        Result = _calculator.Add(ConvertToDouble(Number1), ConvertToDouble(Number2));
                        break;
                    case "Subtract":
                        Result = _calculator.Subtract(ConvertToDouble(Number1), ConvertToDouble(Number2));
                        break;
                    case "Multiply":
                        Result = _calculator.Multiply(ConvertToDouble(Number1), ConvertToDouble(Number2));
                        break;
                    case "Divide":
                        Result = _calculator.Divide(ConvertToDouble(Number1), ConvertToDouble(Number2));
                        break;
                }

                // After performing the operation, reset for a new calculation
                Reset();
            }
        }

        // Handles number button clicks and appends the clicked number to either Number1 or Number2
        private void OnNumberButtonClick(object parameter)
        {
            if (parameter == null) return;

            var clickedNumber = parameter.ToString(); // Get the clicked number as a string
            if (_isNumber1Active)
            {
                Number1 += clickedNumber; // Append the clicked number to Number1
            }
            else
            {
                Number2 += clickedNumber; // Append the clicked number to Number2
            }
        }

        // Clears the calculator's input and result fields
        private void Clear(object parameter)
        {
            Number1 = ""; // Clear Number1
            Number2 = ""; // Clear Number2
            Result = 0; // Reset the result
            _selectedOperation = ""; // Clear the selected operation
            _isNumber1Active = true; // Reset focus to Number1 input for the next calculation
        }

        // Converts the string input to double for calculation purposes
        private double ConvertToDouble(string input)
        {
            double.TryParse(input, out double result); // Try to convert the input string to a double
            return result;
        }

        // Resets the calculator after an operation is performed
        private void Reset()
        {
            Number1 = ""; // Clear Number1
            Number2 = ""; // Clear Number2
            _selectedOperation = ""; // Clear the selected operation
            _isNumber1Active = true; // Set focus back to Number1 for the next calculation
        }

        // Event that is raised when a property value changes (used for data binding in the View)
        public event PropertyChangedEventHandler PropertyChanged;

        // Helper method to raise the PropertyChanged event and notify the View of changes
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // RelayCommand class: Implements ICommand to handle button click commands in the View
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute; // The action to execute when the command is invoked
        private readonly Predicate<object> _canExecute; // Predicate to determine if the command can execute

        // Constructor: Initializes the command with execute logic and optionally a condition for execution
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // Determines whether the command can execute (returns true if no condition is provided)
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        // Executes the action associated with the command
        public void Execute(object parameter) => _execute(parameter);

        // Event that is raised when the execution status changes (to refresh the enabled state)
        public event EventHandler CanExecuteChanged;

        // Raises the CanExecuteChanged event to notify the View to re-evaluate the command's executable state
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
