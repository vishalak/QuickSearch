using System;
using System.Collections.ObjectModel;
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
            MyControl.SetItemSource(GetDummyData());
        }

        private ObservableCollection<IResultItem> GetDummyData()
        {
            return new ObservableCollection<IResultItem>()
            {
                new QueryResultItem(){Text="Simulation", Description="Run TransDNA Mesoscopic Simulation"},
                new QueryResultItem(){Text="DTA", Description="Run TransDNA Dynamic Traffic Assignment Simulation"},
                new QueryResultItem(){Text="Scenario Manager", Description="Open TransDNA Scenario Manager"},
                new QueryResultItem(){Text="Import Scenario", Description="Import a TransDNA Scenario"}
            };
        }
    }
    public class QueryResultItem : IResultItem
    {
        public string Text { get; set; }
        public string Description { get; set; }

    }
}
