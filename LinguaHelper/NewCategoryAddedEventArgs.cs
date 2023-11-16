using System;

namespace LinguaHelper
{
    public class NewCategoryAddedEventArgs : EventArgs
    {
        public string Category { get; set; }

        public NewCategoryAddedEventArgs(string category)
        {
            Category = category;
        }
    }
}
