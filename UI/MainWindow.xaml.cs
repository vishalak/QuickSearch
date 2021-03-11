using System.Windows;
using QuickSearchControl;

namespace QuickSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var filePath = "C:\\temp\\commands.txt";
            MyControl.Init(filePath);
        }
    }
}
