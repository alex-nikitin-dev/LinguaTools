using System;

namespace TestProj
{
    public class BoundObject
    {
        public string Text;
        public event EventHandler<BrowserTextSelectedEventArgs> BrowserTextSelected;

        protected virtual void OnBrowserTextSelected(BrowserTextSelectedEventArgs e)
        {
            BrowserTextSelected?.Invoke(this, e);
        }
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once IdentifierTypo
        public void onselect(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
                OnBrowserTextSelected(new BrowserTextSelectedEventArgs(msg));
        }
    }
}
