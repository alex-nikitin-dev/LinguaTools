using System;

namespace TestProj
{
    class NewHistoryItemAddedEventArgs:EventArgs
    {
        public HistoryDataItem HistoryItem { get; set; }
        public bool IsCategoryNew { get; set; }

        public NewHistoryItemAddedEventArgs(HistoryDataItem item,bool isCategoryNew)
        {
            HistoryItem = item;
            IsCategoryNew = isCategoryNew;
        }
    }
}

