using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuickSearchControl
{
    public static class DataProvider
    {
        public static ObservableCollection<IResultItem> GetDummyData()
        {
            return new ObservableCollection<IResultItem>()
            {
                new QueryResultItem(){Text="Project Toolbar", Description="Show or hide the Project Tools toolbar", CommandText="tsm project tools"},
                new QueryResultItem(){Text="Project Settings", Description="Edit the timing parameters, input files, and output selection that define a project", CommandText="tsm project settings"},
                new QueryResultItem(){Text="Scenario Manager", Description="Manage scenarios and files", CommandText="G30 Toggle Project Settings"},
                new QueryResultItem(){Text="Parking Toolbar", Description="Show or hide the Parking toolbar", CommandText="tsm parking toolbox"},
                new QueryResultItem(){Text="Turn Prohibition Editor", Description="Show or hide the Turn Prohibition Editor", CommandText="Toggle G60 Turn Prohibition Editor"}
            };
        }

        public static ObservableCollection<IResultItem> GetDataFromFile(string filePath)
        {
            try
            {
                var collection = new ObservableCollection<IResultItem>();
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Items source for the available commands has not been set. Filling it with dummy data.");
                    collection = GetDummyData();
                }

                return collection;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
        }
    }

    public class QueryResultItem : IResultItem
    {
        public string Text { get; set; }
        public string Description { get; set; }
        public string CommandText { get; set; }
        public string Category { get; set; }
        public string KeyboardShortcut { get; set; }
        public string ImagePath { get; set; }
        
        private string displayText;
        public string DisplayText
        {
            get
            {
                return $"{Text} - {Description}\t{KeyboardShortcut?.ToString()}";
            }
            set
            {
                displayText = value;              
            }
        }
    }
}
