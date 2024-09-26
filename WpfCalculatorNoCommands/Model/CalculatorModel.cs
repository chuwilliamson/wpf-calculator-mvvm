using System;

namespace WpfCalculatorSingleTextBox.Model
{
    // The CalculatorModel handles core arithmetic operations
    public class CalculatorModel
    {
        // Performs addition of two numbers
        public double Add(double num1, double num2)
        {
            return num1 + num2;
        }

        // Performs subtraction of two numbers
        public double Subtract(double num1, double num2)
        {
            return num1 - num2;
        }

        // Performs multiplication of two numbers
        public double Multiply(double num1, double num2)
        {
            return num1 * num2;
        }

        // Performs division of two numbers, with error handling for division by zero
        public double Divide(double num1, double num2)
        {
            if (num2 == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return num1 / num2;
        }
    }
}
