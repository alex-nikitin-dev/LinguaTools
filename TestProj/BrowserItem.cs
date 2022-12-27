using CefSharp.WinForms;
using CefSharp;
using System.Windows.Forms;
using ABI.Windows.Foundation;
using System.Threading;
using CefSharp.Web;
using System;
using System.Threading.Tasks;

namespace TestProj
{
    internal class BrowserItem
    {
        readonly ChromiumWebBrowser _browser;
        private readonly string _url;
        private readonly string _requestParams;
        readonly string _prepareUrl;
        public bool IsClickVergin { get; private set; }

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

        //TODO: should't be string
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
            bool legacyBinding = false)
        {
            _browser = browser;
            IsClickVergin = true;
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
            _browser.Dock = DockStyle.Fill;
        }

        public delegate void FinishAllTasksDelegate(BrowserItem sender);
        public event FinishAllTasksDelegate FinishAllTasks;
        private void OnFinishAllTasks(BrowserItem sender)
        {
            FinishAllTasks?.Invoke(sender);
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

        private bool _outsideLoadCommand = false;
        public void Go(string text, bool force = false)
        {
            if (force || IsLoadAllowed())
            {
                ResetLoadSettings();
                _outsideLoadCommand = true;
                _browser.Load($"{_url}{text}{_requestParams}");
            }
        }

        private string _gotoAfterPreparing;

        public void Prepare(string gotoAfterPreparing = null)
        {
            if (string.IsNullOrEmpty(_prepareUrl)) return;

            _needPreparing = true;
            _gotoAfterPreparing = gotoAfterPreparing;
            _outsideLoadCommand = true;
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

        private bool IsAutoRedirectNeeded => !string.IsNullOrEmpty(_gotoAfterPreparing);

        private bool _needPreparing;
        private bool _needSetColorTheme;
        private void _browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            var autoRedirecting = false;

            if (e.Frame.IsValid == false)
                return;

            if (_needPreparing && e.Frame.IsMain)
            {
                _needPreparing = false;
                if (!DoPrepare() && IsAutoRedirectNeeded)
                {
                    Go(_gotoAfterPreparing, true);
                    autoRedirecting = true;
                }
            }
            else if (IsAutoRedirectNeeded && e.Frame.IsMain)
            {
                Go(_gotoAfterPreparing, true);
                autoRedirecting= true;
            }

            if (_needSetColorTheme && e.Frame.IsMain)
            {
                _needSetColorTheme = false;
                InsertColorThemeJS();
            }

            if (e.Frame.IsMain)
            {
                InsertMainFrameJavaScript();
                DoAsyncJSCode();
            }

            InsertOtherJavaScript();

            if(_outsideLoadCommand && e.Frame.IsMain && !IsAutoRedirectNeeded && !_needPreparing)
            {
                OnFinishAllTasks(this);
            }

            if(BrowserName == "Google")
            {
                bool b = false;
            }

            if (e.Frame.IsMain && !autoRedirecting)
                _outsideLoadCommand = false;
        }

        public void DoAsyncJSCode()
        {
            if (_browserJS != null && !string.IsNullOrEmpty(_browserJS.ForAsyncEvalJSCode))
            {
                _browser.EvaluateScriptAsync(_browserJS.ForAsyncEvalJSCode);
            }
        }

        private void InsertColorThemeJS()
        {
            //TODO: this solution is wrong. Need to replace with somthing like a dictionary with keys ColorTheme
            if (_browserJS != null && _browserJS.ColorThemeJSCode != null && _browserJS.ColorThemeJSCode.Length >= 2)
                _browser.ExecuteScriptAsync(_browserJS.ColorThemeJSCode[(int)ColorTheme]);
        }
        private void InsertOtherJavaScript()
        {
            if (_browserJS != null && !string.IsNullOrEmpty(_browserJS.OtherJSCode))
                _browser.ExecuteScriptAsync(_browserJS.OtherJSCode);
        }
        private void InsertMainFrameJavaScript()
        {
            if (_browserJS != null && !string.IsNullOrEmpty(_browserJS.MainFrameJSCode))
                _browser.ExecuteScriptAsync(_browserJS.MainFrameJSCode);
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
            if (css == null) return;

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

        private async Task<(double x, double y)?> GetElementPositionByClassName(string elementClassName)
        {
            var jsReponse = await _browser.EvaluateScriptAsync(
            @$"(function () {{
            var element = document.querySelector('{elementClassName}');
            element.focus();
            var elementRect = element.getBoundingClientRect();
            return elementRect.left + `#` + elementRect.top;
            }})();"
            );
            if (jsReponse.Success && jsReponse.Result != null)
            {
                var jsonString = (string)jsReponse.Result;
                var result = jsonString.Split('#');
                if (result.Length == 2) 
                {
                    return (double.Parse(result[0]), double.Parse(result[1]));
                }
            }

            return null;
        }

        public async void ClickOnElementByClassName(string elementClassName)
        {
            var responce = await GetElementPositionByClassName(elementClassName);
            if (responce != null)
            {
                var x = (int)Math.Round(responce.Value.x) + 1;
                var y = (int)Math.Round(responce.Value.y) + 1;

                var host = _browser.GetBrowser().GetHost();
                host.SendMouseMoveEvent(x, y, false, CefEventFlags.None);
                Thread.Sleep(100);
                host.SendMouseClickEvent(x, y, MouseButtonType.Left, false, 0, CefEventFlags.LeftMouseButton);
                Thread.Sleep(10);
                host.SendMouseClickEvent(x, y, MouseButtonType.Left, true, 0, CefEventFlags.LeftMouseButton);

                IsClickVergin = false;
            }
        }
    }
}