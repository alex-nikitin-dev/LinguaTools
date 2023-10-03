using System;
using System.Collections;
using System.Collections.Generic;

namespace LinguaHelper
{
    class VirtualDesktopEnum : IEnumerator<VirtualDesktopItem>
    {
        private readonly List<VirtualDesktopItem> items;
        private int _curIndex = -1;

        public VirtualDesktopEnum(List<VirtualDesktopItem> list)
        {
            items = new List<VirtualDesktopItem>(list);
        }
        public void Dispose()
        {
            _curIndex = -1;
        }

        public bool MoveNext()
        {
            _curIndex++;
            return (_curIndex < items.Count);
        }

        public void Reset()
        {
            _curIndex = -1;
        }

        public VirtualDesktopItem Current
        {
            get
            {
                try
                {
                    return items[_curIndex];
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
