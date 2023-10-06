using Microsoft.CodeAnalysis.CSharp;

namespace LinguaHelper
{
    class VirtualDesktopItem
    {
        public int Index { get; private set; }
        public string Name { get; private set; }
        public bool IsVisible { get; private set; }
        
        public VirtualDesktopItem(int index, string name, bool isVisible)
        {
            Index = index;
            Name = name;
            IsVisible = isVisible;
        }

        public async void Switch()
        {
             await VirtualDesktopPowerShell.SwitchToDesktopAsync(Index);
        }
    }
}
