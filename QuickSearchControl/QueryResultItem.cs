namespace QuickSearchControl
{
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
                return string.IsNullOrEmpty(displayText) ? $"{Text} - {Description}" : displayText;
            }
            set
            {
                displayText = value;              
            }
        }
    }
}
