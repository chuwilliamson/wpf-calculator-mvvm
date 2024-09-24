using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCalculator.Model
{
    public class Calculator
    {
        public double Add(double num1, double num2) => num1 + num2;
        public double Subtract(double num1, double num2) => num1 - num2;
        public double Multiply(double num1, double num2) => num1 * num2;
        public double Divide(double num1, double num2) => num1 / num2;
    }
}
