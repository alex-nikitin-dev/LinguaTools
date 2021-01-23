using System;

namespace TestProj
{
    class HistoryDataItem
    {
        public string Phrase { get; }
        public DateTime Date { get; }
        public string Category { get; }

        public HistoryDataItem(string phrase, string category, DateTime date)
        {
            Phrase = phrase;
            Category = category;
            Date = date;
        }
    }
}
