namespace LinguaHelper
{
    class VirtualDesktopItem
    {
        public int Index { get; private set; }
        public string Name { get; private set; }
        public bool IsCurrent { get; private set; }
        
        public VirtualDesktopItem(int index, string name, bool isCurrent)
        {
            Index = index;
            Name = name;
            IsCurrent = isCurrent;
        }

        public void Switch()
        {
            VirtualDesktopPowerShell.SwitchToDesktop(Index);
        }
    }
}
