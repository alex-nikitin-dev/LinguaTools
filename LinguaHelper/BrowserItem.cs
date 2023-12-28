using CefSharp;
using CefSharp.WinForms;
using System;
using System.Drawing;
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
        public string CSSDarkThemeDefaultPage { get; set; }

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
        private string _defaultPagePath;
        private string _defaultWelcomeText;
        private readonly bool _defaultTranslateOnSelection;
        public bool TranslateOnSelection { get; set; }
        private BrowserItem(ChromiumWebBrowser browser,
            string url,
            string browserName,
            string cssDarkTheme,
            string cssDarkThemeDefault,
            ColorTheme theme,
           BrowserJS browserJS,
            string loginUrl,
            string requestParams = null,
            string defaultPagePath = null,
            string defaultWelcomeText = null,
            bool translateOnSelection = false,
            bool legacyBinding = false)
        {
            _browser = browser;
            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.LocalStorage = CefState.Enabled;
            _browser.BrowserSettings = browserSettings;
            Active = false;
            IsClickVergin = true;
            _gotoAfterLogin = null;
            _url = url;
            BrowserName = browserName;
            CSSDarkTheme = cssDarkTheme;
            CSSDarkThemeDefaultPage = cssDarkThemeDefault;
            _needLogin = false;
            _colorTheme = theme;
            _browser.FrameLoadEnd += _browser_FrameLoadEnd;
            var requestHandler = new CustomRequestHandler();
            requestHandler.UserGesture += RequestHandler_UserGesture;
            _browser.RequestHandler = requestHandler;
            _loginUrl = loginUrl;
            _requestParams = requestParams;
            _defaultPagePath = Path.Exists(Path.GetFullPath(defaultPagePath)) ? defaultPagePath : null;
            _defaultWelcomeText = defaultWelcomeText;
            TranslateOnSelection = translateOnSelection;
            _defaultTranslateOnSelection = translateOnSelection;
            _browserJS = browserJS;
            _browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = legacyBinding;
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

        [JsonConstructor]
        public BrowserItem(string url,
            string browserName,
            string cssDarkThemePath,
            string cssDarkThemeDefaultPagePath,
            BrowserJS browserJS = null,
            string loginUrl = null,
            string requestParams = null,
            string defaultPagePath = null,
            string defaultWelcomeText = null,
            bool translateOnSelection = false,
            ColorTheme theme = ColorTheme.Light)
            : this(new ChromiumWebBrowser(),
                  url,
                  browserName,
                  null,
                  null,
                  theme,
                  browserJS,
                  loginUrl,
                  requestParams,
                  defaultPagePath,
                  defaultWelcomeText,
                  translateOnSelection)
        {
            _browser.Dock = DockStyle.Fill;
            if (File.Exists(cssDarkThemePath))
                CSSDarkTheme = File.ReadAllText(cssDarkThemePath);
            if (File.Exists(cssDarkThemeDefaultPagePath))
                CSSDarkThemeDefaultPage = File.ReadAllText(cssDarkThemeDefaultPagePath);
        }
        #endregion

        #region public Actions
        public void ResetTranslateOnSelection()
        {
            TranslateOnSelection = _defaultTranslateOnSelection;
        }

        bool _userLoadCommand;
        public void Go(string text,bool isUserCommand, bool force = false)
        {
            if (force || IsLoadAllowed())
            {
                IsDefaultPageLoaded = false;
                _userLoadCommand = isUserCommand;    
                _browser.Load($"{_url}{text}{_requestParams}");
            }
            else
            {
                LoadDefaultPage();
            }
        }
        private void LoadFile(string path)
        {
            try
            { 
                _browser.Load($"file:///{GetEscapedFilePath(path)}");
            }
            catch (Exception e)
            {
                OnBrowerErrorOccured(this, $"Load file exception on {_browser.Address}: {e.Message}");
            }
        }
        private string GetEscapedFilePath(string path)
        {
            try
            {
                string fullPath = Path.GetFullPath(path);
                string[] pathSegments = fullPath.Split(Path.DirectorySeparatorChar);
                for (int i = 0; i < pathSegments.Length; i++)
                {
                    pathSegments[i] = Uri.EscapeDataString(pathSegments[i]);
                }
                return string.Join("/", pathSegments);
            }
            catch (Exception e)
            {
                OnBrowerErrorOccured(this, $"Cannot get escaped file path: {path}: {e.Message}");
            }

            return null;
        }
        /// <summary>
        /// The default page has loaded and shown in the browser
        /// </summary>
        private bool IsDefaultPageLoaded = false;
        public void LoadDefaultPage()
        {
            if (_defaultPagePath != null)
            {
                IsDefaultPageLoaded = true;
                LoadFile(_defaultPagePath);
            }
            else
                OnBrowerErrorOccured(this, $"The default page:\"{_defaultPagePath}\" doesn't exists");
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

        public void ReLoad(bool force = false, bool ignoreCache = true, bool userCommand = false)
        {
            _userLoadCommand = userCommand;
            if (_browser.IsBrowserInitialized && (IsLoadAllowed() || force) && !IsDefaultPageLoaded)
                _browser.Reload(ignoreCache);
            else if (_browser.IsBrowserInitialized && IsDefaultPageLoaded)
                LoadDefaultPage();
            else
                OnBrowerErrorOccured(this, $"Cannot reload browser: Name: {BrowserName} Address: {_browser.Address}");            
        }
        //public void ReloadJSCode(BrowserJS browserJS)
        //{
        //    if (browserJS != null)
        //        _browserJS = browserJS;
        //}
        #endregion

        #region Login
        private string _gotoAfterLogin;

        public void Login(string gotoAfterPreparing = null)
        {
            if (string.IsNullOrEmpty(_loginUrl)) return;

            _needLogin = true;
            _gotoAfterLogin = gotoAfterPreparing;
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

        #region JavaScript injection
        private async Task InjectJavaScript()
        {
            try
            {
                if (_browserJS == null) return; 
                var response = await EvaluateJScriptFuncAsync(_browserJS?.IsJsInjectedFuncName, false);
                if (!(response != null && response.Success && (response.Result as bool? == true)))
                {
                    ExecuteJScriptFuncAsync(_browserJS?.WholeJSCode, false);
                }
                else
                {
                    OnBrowerErrorOccured(this, $"Can't check if Java Script is injected on {_browser.Address}");
                }
            }
            catch (Exception e)
            {
                OnBrowerErrorOccured(this, $"Java Script injection exception on {_browser.Address}: {e.Message}");
            }
        }

        #endregion

        #region Browser events
        private void RequestHandler_UserGesture()
        {
            _userLoadCommand = true;
        }
        private async void _browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (!e.Frame.IsValid)
                return;

            if (e.Frame.IsMain)
            {
                await InjectJavaScript();
                ApplyColorTheme();
                if(!IsDefaultPageLoaded) BindCefObjects(false);
                ExecuteColorThemeJS(false);
                if(!IsDefaultPageLoaded) DeleteAd(false);
                if (!IsDefaultPageLoaded) AcceptAllCookies(false);
                if (!IsDefaultPageLoaded) DoUserCommands();
                if(IsDefaultPageLoaded) SetDefaultPageText();
            }
        }
        private void SetDefaultPageText()
        {
            if (_defaultWelcomeText != null)
                ExecuteJScriptFuncAsync(_browserJS?.SetDefaultPageTextFuncName, false, _defaultWelcomeText);
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
            if (injectJs) await InjectJavaScript();
            try
            {
                if (!CanExecuteJavascriptInMainFrame())
                    return null;

                if (string.IsNullOrEmpty(funcName))
                    return null;
                    
                return await _browser.EvaluateScriptAsync(funcName, args);
                
            }
            catch (Exception e)
            {
                OnBrowerErrorOccured(this, $"EvaluateJScriptFuncAsync {_browser.Address}, Message: {e.Message}");
                return null;
            }
        }
        private async Task<JavascriptResponse> EvaluateJScriptFuncAsync(string funcName, bool injectJs)
        {
            return await EvaluateJScriptFuncAsync(funcName, injectJs, "");
        }
        #endregion

        #region JavaScript execution

        private async void ExecuteJScriptFuncAsync(string funcName, bool injectJs, params string[] args)
        {
            if (injectJs) await InjectJavaScript();
            try
            {
                if (!CanExecuteJavascriptInMainFrame())
                    return;

                if (!string.IsNullOrEmpty(funcName))
                    _browser.ExecuteScriptAsync(funcName, args);
            }
            catch (Exception e)
            {
                OnBrowerErrorOccured(this, $"ExecuteJScriptFuncAsync {_browser.Address}, Message: {e.Message}");
            }           
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
                    SetBrowserColorsCSS(IsDefaultPageLoaded ? CSSDarkThemeDefaultPage : CSSDarkTheme);
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
        private bool CanExecuteJavascriptInMainFrame(string message=null, bool generateException = false)
        {
            if (_browser.CanExecuteJavascriptInMainFrame)
                return true;
            
            if (generateException)
                OnBrowerErrorOccured(this, $"CanExecuteJavascriptInMainFrame is false. address = {_browser.Address} additional info: {(message == null ? "none":message)}");

            return false;
        }

        /// <summary>
        /// processes all items to click and returns true if all items are clicked
        /// </summary>
        public void ProcessAllItemsToClickAsync()
        {
            if(_browserJS == null) return;  
            if(!CanExecuteJavascriptInMainFrame()) return;
            foreach (var item in _browserJS.AllItemsToClick)
            {
                ClickOnElementByClassNameAsync(item);
            }
        }
        #endregion
    }
}