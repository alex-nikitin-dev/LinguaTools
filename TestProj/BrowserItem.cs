using CefSharp;
using CefSharp.WinForms;
using System.Threading;

namespace TestProj
{
    internal class BrowserItem
    {
        readonly ChromiumWebBrowser _browser;
        string _url;
        string _prepareJSCode;
        string _prepareUrl;
        string _mainFrameJSCode;
        string _otherJSCode;
        public string CSSDarkTheme { get; set; }
        public ColorTheme ColorTheme { get; set; }


        string _browserName;
        public string BrowserName => _browserName;

        public ChromiumWebBrowser Browser => _browser;

        public string Url => _url;

        private BrowserItem(ChromiumWebBrowser browser, 
            string url, 
            string browserName,
            string cssDarkTheme,
            string mainFrameJSCode,
            string otherJSCode,
            string prepareUrl, string prepareJSCode,
            bool legacyBinding = true)
        {
            _browser = browser;
            _url = url;
            _browserName = browserName;
            CSSDarkTheme = cssDarkTheme;
            _needPreparing = false;
            _browser.FrameLoadEnd += _browser_FrameLoadEnd;
            _browser.LoadingStateChanged += _browser_LoadingStateChanged;
            _mainFrameJSCode = mainFrameJSCode;
            _otherJSCode = otherJSCode;
            _prepareUrl = prepareUrl;
            _prepareJSCode = prepareJSCode;
            _browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = legacyBinding;
            ColorTheme = ColorTheme.Light;
        }
        public BrowserItem(string url, 
            string browserName, 
            string cssDarkTheme,
            string mainFrameJSCode = null,
            string otherJSCode = null, 
            string prepareUrl = null, 
            string prepareJSCode = null)
            : this(new ChromiumWebBrowser(), 
                  url, 
                  browserName,
                  cssDarkTheme,
                  mainFrameJSCode,  
                  otherJSCode, 
                  prepareUrl,
                  prepareJSCode)
        {
            _browser.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        public void Go(string text)
        {
            _browser.Load($"{_url}{text}");
        }

        public void Prepare()
        {
            if (string.IsNullOrEmpty(_prepareUrl))
                return;

            _needPreparing = true;
            _browser.Load(_prepareUrl);
        }

        private void DoPrepare()
        {
            if (!string.IsNullOrEmpty(_prepareJSCode))
                _browser.EvaluateScriptAsync(_prepareJSCode);
        }
        private bool _needPreparing;
        private void _browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsValid == false)
                return;

            if (_needPreparing && e.Frame.IsMain)
            {
                _needPreparing = false;
                DoPrepare();
            }

            if (e.Frame.IsMain)
                InsertMainFrameJavaScript();

            InsertOtherJavaScript();
        }
        private void InsertOtherJavaScript()
        {
            if (!string.IsNullOrEmpty(_otherJSCode))
                _browser.ExecuteScriptAsyncWhenPageLoaded(_otherJSCode);
        }
        private void InsertMainFrameJavaScript()
        {
            if (!string.IsNullOrEmpty(_mainFrameJSCode))
                _browser.ExecuteScriptAsyncWhenPageLoaded(_mainFrameJSCode);
        }

        private void _browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading)
                return;

            if (ColorTheme == ColorTheme.Dark)
            {
                SetBrowserColorsCSS(CSSDarkTheme);
            }
        }
        private void SetBrowserColorsCSS(string css)
        {
            //SynchronizationContext.Current.Send(new SendOrPostCallback(
            //delegate (object state)
            //{

            //}
            //), null);

            _browser.ExecuteScriptAsync($@"
                    var style1 = document.createElement('style');
                    style1.innerText = `{css}`;
                    document.head.appendChild(style1);
                    ");
        }

        public void ReLoad()
        {
            if (_browser.IsBrowserInitialized)
                _browser.Reload();
        }
    }
}
