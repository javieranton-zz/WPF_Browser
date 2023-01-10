using System.Windows;

namespace WPF_Rx_Browser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                DataContext = new MainViewModel(Browser);
            };
            Closed += (s, e) => ((MainViewModel)DataContext).Dispose();
        }
    }
}
