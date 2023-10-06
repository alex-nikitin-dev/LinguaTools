using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinguaHelper
{
    internal class VirtualDesktopCollection: IEnumerable<VirtualDesktopItem>
    {
        private List<VirtualDesktopItem> _desktops;
        public VirtualDesktopCollection()
        {
            _desktops = new ();
        }
        public void Add(VirtualDesktopItem desktop)
        {
            _desktops.Add(desktop);
        }
        
        //public VirtualDesktopItem this[int index] => _desktops[index];
        public VirtualDesktopItem Current => GetCurrentDesktop();


        private VirtualDesktopItem GetCurrentDesktop()
        {
            return _desktops.Where(d => d.IsVisible).FirstOrDefault();
        }

        public IEnumerator<VirtualDesktopItem> GetEnumerator()
        {
            return new VirtualDesktopEnum(_desktops);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
