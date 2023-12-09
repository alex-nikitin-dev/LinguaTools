using CefSharp;
using CefSharp.WinForms;
using System;
using System.IO;
using System.Runtime.Versioning;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinguaHelper
{
    [SupportedOSPlatform("windows")]
    internal class BrowserItem
    {
        #region .ctor
        readonly ChromiumWebBrowser _browser;
        private readonly string _url;
        private readonly string _requestParams;
        readonly string _loginUrl;
        public BoundObject BoundObject { get; }
        public string BoundObjectName => "browserItemBoundOperator";
        public bool IsClickVergin { get; private set; }
        private BrowserJS _browserJS;
        public string CSSDarkTheme { get; set; }

        public bool Active { get; private set; }

        private bool _needSetColorTheme;
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
           BrowserJS browserJS,
            string loginUrl,
            string requestParams = null,
            bool legacyBinding = false)
        {
            _browser = browser;
            Active = true;
            IsClickVergin = true;
            _gotoAfterLogin = null;
            _url = url;
            BrowserName = browserName;
            CSSDarkTheme = cssDarkTheme;
            _needLogin = false;
            
            _browser.FrameLoadEnd += _browser_FrameLoadEnd;
            var requestHandler = new CustomRequestHandler();
            requestHandler.UserGesture += RequestHandler_UserGesture;
            _browser.RequestHandler = requestHandler;
            _loginUrl = loginUrl;
            _requestParams = requestParams;
            _browserJS = browserJS;
            _browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = legacyBinding;
            ColorTheme = theme;
            _needSetColorTheme = true;

            BoundObject = new BoundObject();
            _browser.JavascriptObjectRepository.ResolveObject += (sender, e) =>
            {
                var repo = e.ObjectRepository;
                if (e.ObjectName == BoundObjectName)
                {
                    repo.Register(BoundObjectName, BoundObject, new BindingOptions());
                }
            };

            _browser.KeyboardHandler = new KeyboardHandler();
        }

        

        private void RequestHandler_UserGesture()
        {
            _userLoadCommand = true;
        }

        [JsonConstructor]
        public BrowserItem(string url,
            string browserName,
            string cssDarkThemePath,
            BrowserJS browserJS = null,
            string loginUrl = null,
            string requestParams = null,
            ColorTheme theme = ColorTheme.Dark)
            : this(new ChromiumWebBrowser(),
                  url,
                  browserName,
                  null,
                  theme,
                  browserJS,
                  loginUrl,
                  requestParams)
        {
            _browser.Dock = DockStyle.Fill;
            if (File.Exists(cssDarkThemePath))
                CSSDarkTheme = File.ReadAllText(cssDarkThemePath);
        }
        #endregion

        #region public Actions
        bool _userLoadCommand;
        public void Go(string text,bool isUserCommand, bool force = false)
        {
            if (force || IsLoadAllowed())
            {
                _userLoadCommand = isUserCommand;    
                _browser.Load($"{_url}{text}{_requestParams}");
            }
        }
        public void Show()
        {
            _browser.Show();
        }
        public void Activate()
        {
            Active = true;
        }

        public void Deactivate()
        {
            Active = false;
        }

        public void ReLoad()
        {
            if (_browser.IsBrowserInitialized)
                _browser.Reload();
        }
        //public void ReloadJSCode(BrowserJS browserJS)
        //{
        //    if (browserJS != null)
        //        _browserJS = browserJS;
        //}
        #endregion

        #region events
        //public delegate void FinishAllTasksDelegate(BrowserItem sender);

        //public event FinishAllTasksDelegate FinishAllTasks;
        //private void OnFinishAllTasks(BrowserItem sender)
        //{
        //    FinishAllTasks?.Invoke(sender);
        //}


        #endregion
      
        #region Login
        private string _gotoAfterLogin;

        public void Login(string gotoAfterPreparing = null)
        {
            if (string.IsNullOrEmpty(_loginUrl)) return;

            _needLogin = true;
            _gotoAfterLogin = gotoAfterPreparing;
            //_outsideLoadCommand = true;
            _browser.Load(_loginUrl);
        }

        private bool DoLogin(bool injectJs = true)
        {
            if (_needLogin)
            {
                _needLogin = false;
                ExecuteJScriptFuncAsync(_browserJS?.LoginFuncName, injectJs, "login", "pass");
                return true;
            }
            else if (IsAutoRedirectNeeded)
            {
                ExecuteJScriptFuncAsync(_browserJS?.RedirectAfterLoginFuncName,injectJs, "");
                return true;
            }

            return false;
        }

        private bool IsAutoRedirectNeeded => !string.IsNullOrEmpty(_gotoAfterLogin);

        private bool _needLogin;
        #endregion

        #region Checking JavaScript injected
        private  void InjectJavaScript()
        {
            try
            {
                //var response = await EvaluateJScriptFuncAsync(_browserJS?.IsJsInjectedFuncName, false);
                //if(response.Success && (response.Result as bool? == true))
                //{
                //    ExecuteJScriptFuncAsync(_browserJS?.MainFrameJSCode, false);
                //}
                ExecuteJScriptFuncAsync(_browserJS?.MainFrameJSCode, false);
            }
            catch
            {
                // This catch block is triggered if isJsInjected() function is not defined,
                // which implies that the JS has not been injected yet.
                // return false;
            }
        }
        #endregion

        #region FrameLoadEnd event
        private void _browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (!e.Frame.IsValid)
                return;

            if (e.Frame.IsMain)
            {
                InjectJavaScript();
                ApplyColorTheme();
                BindCefObjects(false);
                ExecuteColorThemeJS(false);
                DeleteAd(false);
                AcceptAllCookies(false);
                DoUserCommands();
            }
        }

        private void DoUserCommands()
        {
            if (_userLoadCommand)
            {
                _userLoadCommand = false;
                ProcessAllItemsToClickAsync();
            }
        }

        private bool IsLoadAllowed()
        {
            return !_needLogin && Active;
        }

        #endregion

        #region JavaScript evaluation
        private async Task<JavascriptResponse> EvaluateJScriptFuncAsync(string funcName, bool injectJs, params string[] args)
        {
            if (injectJs) InjectJavaScript();
            return await _browser.EvaluateScriptAsync(funcName, args);
        }
        private async Task<JavascriptResponse> EvaluateJScriptFuncAsync(string funcName, bool injectJs)
        {
            return await EvaluateJScriptFuncAsync(funcName, injectJs, "");
        }
        #endregion

        #region JavaScript execution
        private void ExecuteJScriptFuncAsync(string funcName, bool injectJs, params string[] args)
        {
           if(injectJs)InjectJavaScript();
            _browser.ExecuteScriptAsync(funcName, args);
        }
        private void ExecuteJScriptFuncAsync(string funcName,bool injectJs)
        {
            ExecuteJScriptFuncAsync(funcName, injectJs, "");
        }
        #endregion

        #region JavaScript calling
        private void BindCefObjects(bool injectJs = true)
        {
            ExecuteJScriptFuncAsync(_browserJS?.BindCefObjectsFuncName, injectJs);
        }
        private void AcceptAllCookies(bool injectJs = true)
        {
            ExecuteJScriptFuncAsync(_browserJS?.AcceptAllCookiesFuncName, injectJs);
        }
        private async void DeleteAd(bool injectJs = true)
        {
            await EvaluateJScriptFuncAsync(_browserJS?.DeleteAdFuncName,injectJs);
        }
        private void ExecuteColorThemeJS(bool injectJs = true)
        {
            if (!_needSetColorTheme) return;

            if (ColorTheme == ColorTheme.Light)
                ExecuteJScriptFuncAsync(_browserJS?.SetLightThemeFuncName, injectJs);
            else if (ColorTheme == ColorTheme.Dark)
                ExecuteJScriptFuncAsync(_browserJS?.SetDarkThemeFuncName, injectJs);
        }
        private void SetBrowserColorsCSS(string css)
        {
            if (css == null) return;

            ExecuteJScriptFuncAsync($@"
                    var style1 = document.createElement('style');
                    style1.innerText = `{css}`;
                    document.head.appendChild(style1);
                    ", false);
        }
        private void ApplyColorTheme()
        {
            switch (ColorTheme)
            {
                case ColorTheme.Dark:
                    SetBrowserColorsCSS(CSSDarkTheme);
                    break;
            }
        }
        #endregion

        #region Clicking on elements
        private async Task<(double x, double y)?> GetElementPositionByClassNameAcync(string elementClassName)
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

        private async void ClickOnElementByClassNameAsync(string elementClassName)
        {
            var responce = await GetElementPositionByClassNameAcync(elementClassName);
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

        public delegate void BrowerErrorDelegate(BrowserItem sender, string message);
        public event BrowerErrorDelegate BrowerErrorOccured;
        protected virtual void OnBrowerErrorOccured(BrowserItem sender, string message)
        {
            BrowerErrorOccured?.Invoke(sender, message);
        }
        private bool CanExecuteJavascriptInMainFrame()
        {
            if (_browser.CanExecuteJavascriptInMainFrame)
                return true;
            else
                OnBrowerErrorOccured(this, $"Can't execute javascript in main frame of {_browser.Address}");

            return false;
        }

        /// <summary>
        /// processes all items to click and returns true if all items are clicked
        /// </summary>
        public void ProcessAllItemsToClickAsync()
        {
            if(!CanExecuteJavascriptInMainFrame()) return;
            foreach (var item in _browserJS.AllItemsToClick)
            {
                ClickOnElementByClassNameAsync(item);
            }
        }
        #endregion
    }
}