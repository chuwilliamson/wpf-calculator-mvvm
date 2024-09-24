﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get => _number1; // Getter returns the current value of _number1
            set
            {
                _number1 = value; // Setter updates _number1
                OnPropertyChanged(nameof(Number1)); // Notifies the View that the property has changed
            }
        }

        public double Number2
        {
            get => _number2; // Getter returns the current value of _number2
            set
            {
                _number2 = value; // Setter updates _number2
                OnPropertyChanged(nameof(Number2)); // Notifies the View that the property has changed
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

        // Event that is raised when a property value changes (used for data binding)
        public event PropertyChangedEventHandler PropertyChanged;

        // Helper method to raise the PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
