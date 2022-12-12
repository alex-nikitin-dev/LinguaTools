using CefSharp.WinForms;
using CefSharp;
namespace TestProj
{
    internal class BrowserItem
    {
        readonly ChromiumWebBrowser _browser;
        private readonly string _url;
        readonly string _prepareUrl;

        private IBrowserJS _browserJS;

        public string CSSDarkTheme { get; set; }
        public ColorTheme ColorTheme { get; set; }


        public string BrowserName { get; }

        public ChromiumWebBrowser Browser => _browser;

        public string Url => _url;

        private BrowserItem(ChromiumWebBrowser browser, 
            string url, 
            string browserName,
            string cssDarkTheme,
           IBrowserJS browserJS,
            string prepareUrl,
            bool legacyBinding = true)
        {
            _browser = browser;
            _url = url;
            BrowserName = browserName;
            CSSDarkTheme = cssDarkTheme;
            _needPreparing = false;
            _browser.FrameLoadEnd += _browser_FrameLoadEnd;
            _browser.LoadingStateChanged += _browser_LoadingStateChanged;

            _prepareUrl = prepareUrl;
            _browserJS = browserJS;
            _browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = legacyBinding;
            ColorTheme = ColorTheme.Light;
        }
        public BrowserItem(string url, 
            string browserName, 
            string cssDarkTheme,
            IBrowserJS browserJS = null,
            string prepareUrl = null)
            : this(new ChromiumWebBrowser(), 
                  url, 
                  browserName,
                  cssDarkTheme,
                  browserJS,
                  prepareUrl)
        {
            _browser.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        public void ReloadJSCode(IBrowserJS browserJS)
        {
            if (browserJS != null)
                _browserJS = browserJS;
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
            if (_browserJS != null && !string.IsNullOrEmpty(_browserJS.PrepareJSCode))
                _browser.EvaluateScriptAsync(_browserJS.PrepareJSCode);
            
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
            if (_browserJS != null && !string.IsNullOrEmpty(_browserJS.OtherJSCode))
                _browser.ExecuteScriptAsyncWhenPageLoaded(_browserJS.OtherJSCode);
        }
        private void InsertMainFrameJavaScript()
        {
            if (_browserJS != null && !string.IsNullOrEmpty(_browserJS.MainFrameJSCode))
                _browser.ExecuteScriptAsyncWhenPageLoaded(_browserJS.MainFrameJSCode);
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
