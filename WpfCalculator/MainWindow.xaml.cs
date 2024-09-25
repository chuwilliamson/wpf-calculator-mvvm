using System.Windows;
using System.Windows.Input;
using WpfCalculator.ViewModel;


namespace WpfCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isNumber1Active = true;  // New flag to track which number is active

        public MainWindow()
        {
            InitializeComponent();
            Number1TextBox.Focus();  // Start with Number1TextBox in focus
        }

        // Event handler to capture key presses for operations
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel == null) return;

            // Automatically ensure focus remains on number input fields
            if (_isNumber1Active)
            {
                Number1TextBox.Focus();  // Keep focus on Number1 if it's active
            }
            else
            {
                Number2TextBox.Focus();  // Keep focus on Number2 if it's active
            }

            // Detect if the user pressed an operator key
            if (e.Key == Key.Add || e.Key == Key.OemPlus)
            {
                e.Handled = true;  // Suppress operator key from being added to the TextBox
                viewModel.OperationCommand.Execute("Add");
                _isNumber1Active = false;  // Switch to Number2
                Number2TextBox.Focus();  // Set focus to Number2TextBox
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                e.Handled = true;
                viewModel.OperationCommand.Execute("Subtract");
                _isNumber1Active = false;
                Number2TextBox.Focus();
            }
            else if (e.Key == Key.Multiply)
            {
                e.Handled = true;
                viewModel.OperationCommand.Execute("Multiply");
                _isNumber1Active = false;
                Number2TextBox.Focus();
            }
            else if (e.Key == Key.Divide || e.Key == Key.OemQuestion)
            {
                e.Handled = true;
                viewModel.OperationCommand.Execute("Divide");
                _isNumber1Active = false;
                Number2TextBox.Focus();
            }

            // Check if Enter is pressed to calculate the result
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                viewModel.EqualsCommand.Execute(null);
                _isNumber1Active = true;  // Reset to start entering Number1 again
                Number1TextBox.Focus();  // Set focus back to Number1TextBox
            }
        }
    }
}
