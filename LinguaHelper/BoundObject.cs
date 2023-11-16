using Newtonsoft.Json;
using System;

namespace LinguaHelper
{
    /// <summary>
    /// This class is used to bind C# object to JS code.
    /// </summary>
    public class BoundObject
    {
        /// <summary>
        /// Occurs when text is selected in browser.
        /// </summary>
        public event EventHandler<BrowserTextSelectedEventArgs> BrowserTextSelected;
        protected virtual void OnBrowserTextSelected(BrowserTextSelectedEventArgs e)
        {
            BrowserTextSelected?.Invoke(this, e);
        }

        /// <summary>
        /// Must be called from JS code when text is selected in browser.
        /// </summary>
        /// <param name="selectedText">the very text itself</param>
        public void onselect(string selectedText)
        {
            if (!string.IsNullOrEmpty(selectedText))
                OnBrowserTextSelected(new BrowserTextSelectedEventArgs(selectedText));
        }

        /// <summary>
        /// Occurs when JS error has happened while executing JS code.
        /// </summary>
        public event EventHandler<JScriptErrorEventArgs> JScriptErrorOccured;
        protected virtual void OnJsErrorOccured(JScriptErrorEventArgs e)
        {
            JScriptErrorOccured?.Invoke(this, e);
        }
        /// <summary>
        /// Must be called from JS code when JS error has happened. 
        /// </summary>
        /// <param name="jsError">json serialized error message from js code</param>
        public void onJsError(string jsError)
        {
            if (!string.IsNullOrEmpty(jsError))
                OnJsErrorOccured(JsonConvert.DeserializeObject<JScriptErrorEventArgs>(jsError));
        }
    }
}
