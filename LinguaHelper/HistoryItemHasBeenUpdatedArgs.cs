using System;

namespace TestProj
{
    class HistoryItemHasBeenUpdatedArgs : EventArgs
    {
        public HistoryDataItem OldItem { get; set; }
        public HistoryDataItem UpdatedItem { get; set; }

        public HistoryItemHasBeenUpdatedArgs(HistoryDataItem oldItem, HistoryDataItem updatedItem)
        {
            OldItem = oldItem;
            UpdatedItem = updatedItem;
        }
    }
}
