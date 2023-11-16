using System;
using System.Collections;
using System.Collections.Generic;

namespace LinguaHelper
{
    class HistoryEnum : IEnumerator<HistoryDataItem>
    {
        private readonly List<HistoryDataItem> _data;
        private int _curIndex = -1;
        private readonly bool _forward;
        public HistoryEnum(List<HistoryDataItem> data, bool forward = true)
        {
            _data = new List<HistoryDataItem>(data);
            _forward = forward;
            Reset();
        }
        public void Dispose()
        {
            Reset();
        }

        public bool MoveNext()
        {
            if (_forward)
                _curIndex++;
            else
                _curIndex--;

            return _forward ? (_curIndex < _data.Count) : (_curIndex >= 0);
        }

        public void Reset()
        {
            if (_forward)
                _curIndex = -1;
            else
                _curIndex = _data.Count;
        }

        public HistoryDataItem Current
        {
            get
            {
                try
                {
                    return _data[_curIndex];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => Current;
    }
}
