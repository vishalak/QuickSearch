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
            MyControl.Init();
        }
    }
}
