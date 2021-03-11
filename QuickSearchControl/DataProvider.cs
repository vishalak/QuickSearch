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
                new QueryResultItem(){Text="Project Toolbar", Description="Show or hide the Project Tools toolbar", 
                    CommandText="tsm project tools", Category="macro"},
                new QueryResultItem(){Text="Project Settings", Description="Edit the timing parameters, input files, and output selection that define a project", 
                    CommandText="tsm project settings", Category="dbox"},
                new QueryResultItem(){Text="Scenario Manager", Description="Manage scenarios and files", 
                    CommandText="G30 Toggle Project Settings", Category="macro"},
                new QueryResultItem(){Text="Parking Toolbar", Description="Show or hide the Parking toolbar", 
                    CommandText="tsm parking toolbox", Category="macro"},
                new QueryResultItem(){Text="Turn Prohibition Editor", Description="Show or hide the Turn Prohibition Editor", 
                    CommandText="Toggle G60 Turn Prohibition Editor", Category="macro"}
            };
        }

        public static ObservableCollection<IResultItem> GetDataFromFile(string filePath)
        {
            try
            {
                var collection = new ObservableCollection<IResultItem>();
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Either no file has been specified or the specified file does not exist. " +
                        "Items source for the available commands could not be set from file. Filling it with dummy data.", caption:"No settings file");
                    collection = GetDummyData();
                }
                else
                {
                    var lines = File.ReadAllLines(filePath);
                    //skip line 1, assuming its header
                    for (var i=1; i<lines.Length; i++)
                    {
                        //parse the line contents and add the resulting item to the collection
                        var items = lines[i].Split(';');
                        var qri = new QueryResultItem();
                        for(var j = 0; j<items.Length; j++)
                        {
                            var item = items[j].Trim();
                            switch (j)
                            {
                                case 0: 
                                    qri.Text = item;
                                    break;
                                case 1:
                                    qri.Description = item;
                                    break;
                                case 2:
                                    qri.CommandText = item;
                                    break;
                                case 3:
                                    qri.Category = item.ToLower();
                                    break;
                                case 4:
                                    qri.KeyboardShortcut = item;
                                    break;
                                case 5:
                                    qri.ImagePath = item;
                                    break;
                                case 6:
                                    qri.DisplayText = item;
                                    break;

                            }
                        }
                        collection.Add(qri);
                    }
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
}
