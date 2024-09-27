using System.Windows;
using System.Windows.Input;
using WpfCalculatorSingleTextBox.ViewModel;

namespace WpfCalculatorSingleTextBox
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OperationDisplayTextBox.Focus();  // Ensure focus starts on the TextBox
        }

        // Event handler to capture key presses for numbers, operators, and actions
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel == null) return;

            // Handle number keys (0-9) for the main keyboard
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                int numberPressed = (int)e.Key - (int)Key.D0;
                viewModel.AppendNumber(numberPressed.ToString());
            }
            // Handle number keys (0-9) from the numpad
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                int numberPressed = (int)e.Key - (int)Key.NumPad0;
                viewModel.AppendNumber(numberPressed.ToString());
            }

            // Handle the decimal key (period)
            else if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
            {
                viewModel.AppendDecimal();
            }

            // Handle operator keys
            else if (e.Key == Key.Add || e.Key == Key.OemPlus)
            {
                viewModel.SetOperator("+");
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                viewModel.SetOperator("-");
            }
            else if (e.Key == Key.Multiply || (e.Key == Key.D8 && Keyboard.Modifiers == ModifierKeys.Shift))
            {
                viewModel.SetOperator("*");
            }
            else if (e.Key == Key.Divide || e.Key == Key.OemQuestion)
            {
                viewModel.SetOperator("/");
            }

            // Handle Enter key to calculate the result
            else if (e.Key == Key.Enter)
            {
                viewModel.CalculateResult();
            }

            // Handle Escape key to clear/reset the calculator
            else if (e.Key == Key.Escape)
            {
                viewModel.Clear();
            }
        }
    }
}
