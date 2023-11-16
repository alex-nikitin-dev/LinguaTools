using System;

namespace LinguaHelper
{
    class HistoryDataItem
    {
        public string Phrase { get; }
        public DateTime Date { get; set; }
        public string Category { get; }

        public HistoryDataItem(string phrase, string category, DateTime date)
        {
            Phrase = phrase;
            Category = category;
            Date = date;
        }

        public HistoryDataItem Copy()
        {
            return new HistoryDataItem(Phrase, Category, Date);

        }
    }
}
