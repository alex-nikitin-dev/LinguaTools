using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace TestProj
{
    class History:IEnumerable<HistoryDataItem>
    {
        private readonly List<HistoryDataItem> _historyData;
        private readonly string _filePath;
        public List<string> Categories { get; private set; }
        private readonly string _dateTimeFormat;
        private readonly string _dateTimeFileFormat;
        public bool HistoryHasBeenChanged { get; private set; }
        public bool EnumerateForward { get; set; }

        public History(string dateTimeFormat, string dataTimeFileFormat, string filePath, bool enumerateForward)
        {
            _dateTimeFormat = dateTimeFormat;
            _dateTimeFileFormat = dataTimeFileFormat;
            _filePath = filePath;
            _historyData = new List<HistoryDataItem>();
            HistoryHasBeenChanged = false;
            EnumerateForward = enumerateForward;
        }

        public void LoadData(string path)
        {
            if (!File.Exists(path))
            {
                File.CreateText(path).Close();
            }
            var rows = File.ReadAllLines(path);
            _historyData.Clear();

            foreach (var row in rows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                var items = row.Split(new[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);

                _historyData.Add(new HistoryDataItem(items[0],
                    items[1],
                    DateTime.ParseExact(items[2], _dateTimeFormat, CultureInfo.InvariantCulture)));
            }

            Categories = GetAllCategories();
        }
        private List<string> GetAllCategories()
        {
            var result = new List<string>();
            foreach (var dataItem in _historyData)
            {
                if (!result.Exists(x => string.Compare(x, dataItem.Category, StringComparison.Ordinal) == 0))
                {
                    result.Add(dataItem.Category);
                }
            }

            return result;
        }
        public bool IsHistoryContainsCategory(string category)
        {
           // return _historyData.Exists(x => string.Compare(x.Category, category, StringComparison.Ordinal) == 0);
           return Categories.Exists(x => string.Compare(x, category, StringComparison.Ordinal) == 0);
        }

        public (bool isExist,HistoryDataItem item) IsThereItem(string phrase, string category)
        {
            foreach (var dataItem in _historyData)
            {
                if (string.CompareOrdinal(dataItem.Phrase, phrase) == 0 &&
                    string.CompareOrdinal(dataItem.Category, category) == 0)
                {
                    return (true, dataItem);
                }
            }
            return (false, null);
        }

        public event EventHandler<NewCategoryAddedEventArgs> NewCategoryAdded;

        protected virtual void OnNewCategoryAdded(NewCategoryAddedEventArgs e)
        {
            var handler = NewCategoryAdded;
            handler?.Invoke(this, e);
        }

        public event EventHandler<NewHistoryItemAddedEventArgs> NewHistoryItemAdded;

        protected virtual void OnNewHistoryItemAdded(NewHistoryItemAddedEventArgs e)
        {
            var handler = NewHistoryItemAdded;
            handler?.Invoke(this, e);
        }

        public event EventHandler<HistoryItemHasBeenUpdatedArgs> HistoryItemHasBeenUpdated;

        protected virtual void OnHistoryItemHasBeenUpdated(HistoryItemHasBeenUpdatedArgs e)
        {
            var handler = HistoryItemHasBeenUpdated;
            handler?.Invoke(this, e);
        }

        public void AddHistoryItem(string text, string category)
        {
            var isCategoryNew =  !IsHistoryContainsCategory(category);
            var thereItem =   IsThereItem(text, category);

            if (thereItem.isExist)
            {
                var old = thereItem.item.Copy();
                thereItem.item.Date = DateTime.Now;
                HistoryHasBeenChanged = true;
                OnHistoryItemHasBeenUpdated(new HistoryItemHasBeenUpdatedArgs(old, thereItem.item));
            }
            else
            {
                var item = new HistoryDataItem(text, category, DateTime.Now);
                _historyData.Add(item);
                HistoryHasBeenChanged = true;
                OnNewHistoryItemAdded(new NewHistoryItemAddedEventArgs(item, isCategoryNew));
            }

            if (isCategoryNew)
            {
                Categories.Add(category);
                OnNewCategoryAdded(new NewCategoryAddedEventArgs(category));
            }
        }

        public async Task SaveHistoryToFile(string path, bool saveWithTimeStamp = false)
        {
            await Task.Run(() =>
            {
                File.Delete(path);
                if (saveWithTimeStamp) path = GetFilePathWithTimeStamp(path);

                using (var file = File.AppendText(path))
                {
                    foreach (var data in _historyData)
                    { 
                        file.WriteLine($@"{data.Phrase};;{data.Category};;{GetDataTimeFormatted(data.Date)}");
                    }
                }
            });
        }
        private string GetTimeStampNowForFile()
        {
            return GetDataTimeFormattedForFile(DateTime.Now);
        }
        private string GetDataTimeFormatted(DateTime dt)
        {
            return dt.ToString(_dateTimeFormat, CultureInfo.InvariantCulture);
        }

        private string GetDataTimeFormattedForFile(DateTime dt)
        {
            return dt.ToString(_dateTimeFileFormat, CultureInfo.InvariantCulture);
        }

        private string GetFileNameWithTimeStamp(string path)
        {
            return Path.GetFileNameWithoutExtension(path) + "." + GetTimeStampNowForFile();
        }
        private string GetFilePathWithTimeStamp(string path)
        {
            return CombinePath(path, GetFileNameWithTimeStamp(path));
        }

        private string CombinePath(string oldFullPath, string newFileNameWithoutExt)
        {
            var ext = Path.GetExtension(oldFullPath);
            var dir = Path.GetDirectoryName(oldFullPath);

            return Path.Combine(dir ?? throw new InvalidOperationException(), newFileNameWithoutExt + ext);
        }

        private string GetCopyPath()
        {
            var copyName = Path.GetFileNameWithoutExtension(_filePath) + "_copy_" + GetTimeStampNowForFile();
            return CombinePath(_filePath, copyName);
        }
        public async Task SaveHistoryToFileAsACopy()
        {
            await SaveHistoryToFile(GetCopyPath());
        }
        void CreateHistoryFileCopy()
        {
            File.Copy(_filePath, GetCopyPath());
        }
        public async Task SaveHistoryToFile(bool saveTheCopy = true)
        {
            if (saveTheCopy)
                CreateHistoryFileCopy();

            await SaveHistoryToFile(_filePath);
        }

        public IEnumerator<HistoryDataItem> GetEnumerator()
        {
            return new HistoryEnum(_historyData, EnumerateForward);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void DeleteHistoryItem(string phrase, string category)
        {
            for (var i = 0; i < _historyData.Count; i++)
            {
                var dataItem = _historyData[i];
                if (string.CompareOrdinal(dataItem.Phrase, phrase) == 0 &&
                    string.CompareOrdinal(dataItem.Category, category) == 0)
                {
                    _historyData.Remove(dataItem);
                    HistoryHasBeenChanged = true;
                    i--;
                }
            }
        }
    }
}
