using System;

namespace TestProj
{
    public class BoundObject
    {
        public string Text;
        public event EventHandler<BrowserTextSelectedEventArgs> BrowserTextSelected;

        protected virtual void OnBrowserTextSelected(BrowserTextSelectedEventArgs e)
        {
            var handler = BrowserTextSelected;
            handler?.Invoke(this, e);
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
