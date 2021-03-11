using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace QuickSearchControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class QsControl : UserControl, INotifyPropertyChanged
    {
        private string filterText;

        private ObservableCollection<IResultItem> ItemsSource { get; set; }
        public QsControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        private void OnFocus(object sender, RoutedEventArgs e)
        {
            CommandText = null;
            ResultsView.Visibility = Visibility.Visible;
        }

        public void Init(string filePath = null)
        {
            ItemsSource = DataProvider.GetDataFromFile(filePath);
            CommandText = null;
            ResultsView.ItemsSource = ItemsSource;
            var view = CollectionViewSource.GetDefaultView(ItemsSource) as CollectionView;
            view.Filter = UseFilter;
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
            SelectedItem= ResultsView.SelectedItem as IResultItem;
            ResultsView.Visibility = Visibility.Collapsed;
            CommandText = SelectedItem.CommandText;
            RaisePropertyChanged("SelectedItem");
            FilterText = string.Empty;
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

        public string CommandText { get; private set; }
        public IResultItem SelectedItem { get; private set; }

        public string FilterText { 
            get { return filterText; }
            set 
            { 
                filterText = value;
                RaisePropertyChanged("FilterText");
                if (ItemsSource != null && ItemsSource.Count > 0)
                {
                    CollectionViewSource.GetDefaultView(ItemsSource).Refresh();
                }
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

    public interface IResultItem
    {
        string Text { get; set; }
        string Description { get; set; }
        string CommandText { get; set; }
        string Category { get; set; }
        string KeyboardShortcut { get; set; }
        string ImagePath { get; set; }
        string DisplayText { get; set; }
    }
}