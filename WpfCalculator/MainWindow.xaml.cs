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
        public MainWindow()
        {
            InitializeComponent();
            // Set the DataContext to the ViewModel
            DataContext = new ViewModel.CalculatorViewModel();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel == null) return;

            // Detect if the user pressed an operator key
            if (e.Key == Key.Add || e.Key == Key.OemPlus)
            {
                viewModel.OperationCommand.Execute("Add");
                Number2TextBox.Focus(); // Move focus to Number2 after selecting operator
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                viewModel.OperationCommand.Execute("Subtract");
                Number2TextBox.Focus();
            }
            else if (e.Key == Key.Multiply)
            {
                viewModel.OperationCommand.Execute("Multiply");
                Number2TextBox.Focus();
            }
            else if (e.Key == Key.Divide || e.Key == Key.OemQuestion)
            {
                viewModel.OperationCommand.Execute("Divide");
                Number2TextBox.Focus();
            }

            // Check if Enter is pressed to calculate the result
            if (e.Key == Key.Enter)
            {
                viewModel.EqualsCommand.Execute(null);
                Number1TextBox.Focus(); // Reset focus to Number1 after calculation
            }
        }

    }
}
