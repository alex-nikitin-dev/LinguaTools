using System;

namespace LinguaHelper
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
