using System;


namespace WpfCalculator.Model
{
    // The Calculator class provides basic arithmetic operations.
    public class Calculator
    {
        // Method for adding two numbers
        public double Add(double num1, double num2)
        {
            return num1 + num2; // Returns the sum of num1 and num2
        }

        // Method for subtracting the second number from the first number
        public double Subtract(double num1, double num2)
        {
            return num1 - num2; // Returns the result of num1 minus num2
        }

        // Method for multiplying two numbers
        public double Multiply(double num1, double num2)
        {
            return num1 * num2; // Returns the product of num1 and num2
        }

        // Method for dividing the first number by the second number
        public double Divide(double num1, double num2)
        {
            // Check for division by zero to avoid an exception
            if (num2 == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            return num1 / num2; // Returns the result of num1 divided by num2
        }
    }
}
