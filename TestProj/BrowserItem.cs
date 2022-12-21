using CefSharp.WinForms;
using CefSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using CefSharp.DevTools.IndexedDB;
using System.Windows.Forms;

namespace TestProj
{
    internal class BrowserItem
    {
        readonly ChromiumWebBrowser _browser;
        private readonly string _url;
        private readonly string _requestParams;
        readonly string _prepareUrl;

        private IBrowserJS _browserJS;

        public string CSSDarkTheme { get; set; }

        private ColorTheme _colorTheme;
        public ColorTheme ColorTheme
        {
            get => _colorTheme;
            set
            {
                _colorTheme = value;
                _needSetColorTheme = true;
            }
        }

        public string BrowserName { get; }

        public ChromiumWebBrowser Browser => _browser;

        public string Url => _url;

        private BrowserItem(ChromiumWebBrowser browser, 
            string url,
            string browserName,
            string cssDarkTheme,
            ColorTheme theme,
           IBrowserJS browserJS,
            string prepareUrl,
            string requestParams = null,
            bool legacyBinding = true)
        {
            _browser = browser;
            _gotoAfterPreparing = null;
            _url = url;
            BrowserName = browserName;
            CSSDarkTheme = cssDarkTheme;
            _needPreparing = false;
            _browser.FrameLoadEnd += _browser_FrameLoadEnd;
            _browser.LoadingStateChanged += _browser_LoadingStateChanged;
            _prepareUrl = prepareUrl;
            _requestParams = requestParams;
            _browserJS = browserJS;
            _browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = legacyBinding;
            ColorTheme = theme;
            
            
            _needSetColorTheme = true;
        }

        public BrowserItem(string url, 
            string browserName, 
            string cssDarkTheme,
            ColorTheme theme,
            IBrowserJS browserJS = null,
            string prepareUrl = null,
            string requestParams = null)
            : this(new ChromiumWebBrowser(), 
                  url, 
                  browserName,
                  cssDarkTheme,
                  theme,
                  browserJS,
                  prepareUrl,
                  requestParams)
        {
            _browser.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        public void ReloadJSCode(IBrowserJS browserJS)
        {
            if (browserJS != null)
                _browserJS = browserJS;
        }

        private bool IsLoadAllowed()
        {
            return !_needPreparing && !_browser.IsLoading;
        }

        private void ResetLoadSettings()
        {
            _gotoAfterPreparing = null;
        }

        public void Go(string text, bool force = false)
        {
            if (force || IsLoadAllowed())
            {
                ResetLoadSettings();
                _browser.Load($"{_url}{text}{_requestParams}");
            }
        }

        private string _gotoAfterPreparing;

        public void Prepare(string gotoAfterPreparing = null)
        {
            if (string.IsNullOrEmpty(_prepareUrl)) return;

            _needPreparing = true;
            _gotoAfterPreparing = gotoAfterPreparing;
            _browser.Load(_prepareUrl);
        }

        private bool DoPrepare()
        {
            if (_browserJS != null && !string.IsNullOrEmpty(_browserJS.PrepareJSCode))
            {
                _browser.EvaluateScriptAsync(_browserJS.PrepareJSCode);
                return true;
            }

            return false;
        }

        private void GotoAfterPreparing()
        {
            if (_gotoAfterPreparing == null)
                return;
            Go(_gotoAfterPreparing, true);
        }

        private bool IsAutoRedirectNeeded => !string.IsNullOrEmpty(_gotoAfterPreparing);

        private bool _needPreparing;
        private bool _needSetColorTheme;
        private void _browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsValid == false)
                return;

            if (_needPreparing && e.Frame.IsMain)
            {
                _needPreparing = false;
                if (!DoPrepare() && IsAutoRedirectNeeded) GotoAfterPreparing();
            }
            else if (IsAutoRedirectNeeded && e.Frame.IsMain)
            {
                GotoAfterPreparing();
            }

            if (_needSetColorTheme && e.Frame.IsMain)
            {
                _needSetColorTheme = false;
                InsertColorThemeJS();
            }

            if (e.Frame.IsMain)
                InsertMainFrameJavaScript();

            InsertOtherJavaScript();
        }

        private void InsertColorThemeJS()
        {
            if (_browserJS != null && _browserJS.ColorThemeJSCode != null && _browserJS.ColorThemeJSCode.Length >= 2)
                _browser.ExecuteScriptAsyncWhenPageLoaded(_browserJS.ColorThemeJSCode[(int)ColorTheme]);
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