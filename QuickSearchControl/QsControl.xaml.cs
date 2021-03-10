using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace QuickSearchControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class QsControl : UserControl
    {
        private ObservableCollection<IResultItem> ItemsSource { get; set; }
        public QsControl()
        {
            InitializeComponent();
        }

        private bool UseFilter(object obj)
        {
            var item = obj as IResultItem;
            var text = FilterTextBox.Text;
            if (string.IsNullOrEmpty(text) || item == null)
            {
               return true;
            }
            
            var result = item.Text.ToLower().Contains(text) || item.Description.ToLower().Contains(text);
            return result;
        }

        private void OnFilterTextChanged(object sender, TextChangedEventArgs args)
        {
            CollectionViewSource.GetDefaultView(ResultsView.ItemsSource).Refresh();
        }

        public void SetItemSource(ObservableCollection<IResultItem> collection)
        {
            ItemsSource = collection;
            var view = CollectionViewSource.GetDefaultView(ItemsSource) as CollectionView;
            view.Filter = UseFilter;
        }

        private void OnFocus(object sender, RoutedEventArgs e)
        {
            ResultsView.ItemsSource = ItemsSource;
            ResultsView.Visibility = Visibility.Visible;
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            //ResultsView.Items.Clear();
        }

        private void FilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Down && ResultsView.Items?.Count > 0)
            {
                ResultsView.SelectedIndex = 0;
                ResultsView.Focus();
            }
        }

        private void ResultsView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExecuteCommand();
        }

        private void ExecuteCommand()
        {
            var item = ResultsView.SelectedItem as IResultItem;
            if (item != null) MessageBox.Show($"Run macro for :{item.Text}");
            ResultsView.Visibility = Visibility.Collapsed;
            FilterTextBox.Text = "";
        }

        private void ResultsView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ExecuteCommand();
            }
            else if  (e.Key == Key.Up && ResultsView.SelectedIndex == 0)
            {
                FilterTextBox.Focus();
            }
        }
    }

    public interface IResultItem
    {
        string Text { get; set; }
        string Description { get; set; }
    }
}