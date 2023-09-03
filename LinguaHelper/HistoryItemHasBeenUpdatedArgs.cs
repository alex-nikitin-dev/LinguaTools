using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
