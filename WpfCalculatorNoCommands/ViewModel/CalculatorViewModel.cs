using System;
using System.ComponentModel;
using System.Windows.Input;
using WpfCalculatorSingleTextBox.Model;

namespace WpfCalculatorSingleTextBox.ViewModel
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _number1 = "";  // First number input
        private string _number2 = "";  // Second number input
        private string _selectedOperator = "";  // Operator chosen by the user (+, -, *, /)
        private string _operationDisplay = "";  // Displayed operation/result

        private readonly CalculatorModel _calculator;  // Model instance for performing operations

        // Property bound to the TextBox to show the current operation or result
        public string OperationDisplay
        {
            get => _operationDisplay;
            set
            {
                _operationDisplay = value;
                OnPropertyChanged(nameof(OperationDisplay));  // Notify UI of changes
            }
        }

        // RelayCommand properties for handling buttons in the UI
        public ICommand NumberCommand { get; }
        public ICommand OperatorCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand DecimalCommand { get; }

        // Constructor initializes the model and RelayCommand bindings for button actions
        public CalculatorViewModel()
        {
            // Initialize the CalculatorModel
            _calculator = new CalculatorModel();

            // Initialize RelayCommands
            NumberCommand = new RelayCommand(param => AppendNumber(param.ToString()));
            OperatorCommand = new RelayCommand(param => SetOperator(param.ToString()));
            EqualsCommand = new RelayCommand(param => CalculateResult());
            ClearCommand = new RelayCommand(param => Clear());
            DecimalCommand = new RelayCommand(param => AppendDecimal());
        }

        // Method to append numbers to the current input
        public void AppendNumber(string number)
        {
            if (string.IsNullOrEmpty(_selectedOperator))
            {
                _number1 += number;
                UpdateOperationDisplay();
            }
            else
            {
                _number2 += number;
                UpdateOperationDisplay();
            }
        }

        // Method to append a decimal point, ensuring only one decimal is allowed
        public void AppendDecimal()
        {
            if (string.IsNullOrEmpty(_selectedOperator))
            {
                if (!_number1.Contains("."))
                {
                    _number1 += ".";
                    UpdateOperationDisplay();
                }
            }
            else
            {
                if (!_number2.Contains("."))
                {
                    _number2 += ".";
                    UpdateOperationDisplay();
                }
            }
        }

        // Method to set the selected operator (+, -, *, /)
        public void SetOperator(string operatorSymbol)
        {
            if (!string.IsNullOrEmpty(_number1))
            {
                _selectedOperator = operatorSymbol;
                UpdateOperationDisplay();
            }
        }

        // Method to calculate the result using the Model
        public void CalculateResult()
        {
            if (!string.IsNullOrEmpty(_number1) && !string.IsNullOrEmpty(_number2) && !string.IsNullOrEmpty(_selectedOperator))
            {
                double result = 0;
                double num1 = Convert.ToDouble(_number1);
                double num2 = Convert.ToDouble(_number2);

                try
                {
                    // Use the CalculatorModel to perform the appropriate operation
                    switch (_selectedOperator)
                    {
                        case "+":
                            result = _calculator.Add(num1, num2);
                            break;
                        case "-":
                            result = _calculator.Subtract(num1, num2);
                            break;
                        case "*":
                            result = _calculator.Multiply(num1, num2);
                            break;
                        case "/":
                            result = _calculator.Divide(num1, num2);
                            break;
                    }

                    // Display the result and clear the input for the next operation
                    OperationDisplay = result.ToString();
                    ClearInputAfterResult();
                }
                catch (DivideByZeroException ex)
                {
                    // Handle division by zero exception
                    OperationDisplay = "Error: " + ex.Message;
                    ClearInputAfterResult();
                }
            }
        }

        // Method to clear/reset the input and operation display
        public void Clear()
        {
            _number1 = "";
            _number2 = "";
            _selectedOperator = "";
            OperationDisplay = "";
        }

        // Updates the operation display to reflect the ongoing input
        private void UpdateOperationDisplay()
        {
            if (string.IsNullOrEmpty(_selectedOperator))
            {
                OperationDisplay = _number1;
            }
            else
            {
                OperationDisplay = $"{_number1} {_selectedOperator} {_number2}";
            }
        }

        // Clears the input after the result is displayed, resetting for the next calculation
        private void ClearInputAfterResult()
        {
            _number1 = "";
            _number2 = "";
            _selectedOperator = "";
        }

        // Event handler for property changes to notify the UI
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
