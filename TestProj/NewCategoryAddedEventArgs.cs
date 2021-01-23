using System;

namespace TestProj
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
