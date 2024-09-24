using System.Windows;


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
    }
}
