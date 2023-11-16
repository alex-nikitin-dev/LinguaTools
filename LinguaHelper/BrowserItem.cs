using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public void Activate()
        {
            Active = true;
        }

        public void Deactivate()
        {
            Active = false;
        }

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
            _browser.LoadingStateChanged += _browser_LoadingStateChanged;
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

        public void Show()
        {
            _browser.Show();
        }

        //public delegate void FinishAllTasksDelegate(BrowserItem sender);

        //public event FinishAllTasksDelegate FinishAllTasks;
        //private void OnFinishAllTasks(BrowserItem sender)
        //{
        //    FinishAllTasks?.Invoke(sender);
        //}

        public void ReloadJSCode(BrowserJS browserJS)
        {
            if (browserJS != null)
                _browserJS = browserJS;
        }

        private bool IsLoadAllowed()
        {
            return !_needLogin && Active;
        }

        private void ResetLoadSettings()
        {
            _gotoAfterLogin = null;
            _jsInjected = false;
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

        private string _gotoAfterLogin;

        public void Login(string gotoAfterPreparing = null)
        {
            if (string.IsNullOrEmpty(_loginUrl)) return;

            _needLogin = true;
            _gotoAfterLogin = gotoAfterPreparing;
            _outsideLoadCommand = true;
            _browser.Load(_loginUrl);
        }

        private bool DoLogin()
        {
            if (_needLogin)
            {
                _needLogin = false;
                _browser.EvaluateScriptAsync(_browserJS?.LoginFuncName, "login", "pass");
                return true;
            }
            else if (IsAutoRedirectNeeded)
            {
                _browser.EvaluateScriptAsync(_browserJS?.RedirectAfterLoginFuncName, "");
                return true;
            }

            return false;
        }

        private bool IsAutoRedirectNeeded => !string.IsNullOrEmpty(_gotoAfterLogin);

        private bool _needLogin;
        private bool _needSetColorTheme;
        private bool _jsInjected = false;
        private void _browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsValid == false)
                return;

            var autoRedirecting = false;
            if (e.Frame.IsMain)
            {
                InjectJavaScript();
                autoRedirecting = DoLogin();
                BindCefObjects();
                ExecuteColorThemeJS();
            }

            DeleteAd();
            ////ProcessAllItemsToClickAsync();
            //if (_outsideLoadCommand && e.Frame.IsMain && !IsAutoRedirectNeeded && !_needLogin)
            //{

            //    //OnFinishAllTasks(this);
            //}

            if (_outsideLoadCommand && e.Frame.IsMain)
            {
                ProcessAllItemsToClickAsync();
            }

            if (e.Frame.IsMain && !autoRedirecting)
                _outsideLoadCommand = false;
        }
        private void ExecuteJScriptFuncAsync(string funcName, params string[] args)
        {
            if (_jsInjected)
                _browser.ExecuteScriptAsync(funcName, args);
        }
        private void ExecuteJScriptFuncAsync(string funcName)
        {
            ExecuteJScriptFuncAsync(funcName, "");
        }
        private void BindCefObjects()
        {
            ExecuteJScriptFuncAsync(_browserJS?.BindCefObjectsFuncName);
        }

        private void DeleteAd()
        {
            ExecuteJScriptFuncAsync(_browserJS?.DeleteAdFuncName);
        }

        private void InjectJavaScript()
        {
            if (_jsInjected) return;
            _jsInjected = true;

            ExecuteJScriptFuncAsync(_browserJS?.MainFrameJSCode);
        }

        private void ExecuteColorThemeJS()
        {
            if (!_needSetColorTheme) return;
            _needSetColorTheme = true;

            if (ColorTheme == ColorTheme.Light)
                ExecuteJScriptFuncAsync(_browserJS?.SetLightThemeFuncName);
            else if (ColorTheme == ColorTheme.Dark)
                ExecuteJScriptFuncAsync(_browserJS?.SetDarkThemeFuncName);
        }

        private void _browser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading)
                return;

            if (ColorTheme == ColorTheme.Dark)
            {
                SetBrowserColorsCSS(CSSDarkTheme);
            }

            DeleteAd();
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

        /// <summary>
        /// processes all items to click and returns true if all items are clicked
        /// </summary>
        public async void ProcessAllItemsToClickAsync()
        {
            var response = await _browser.EvaluateScriptAsync(_browserJS?.AllItemsToClickFuncName, "");
            if (response != null && response.Success && response.Result != null)
            {
                var itemsToClick = (response.Result as List<object>)?.Cast<string>().ToArray();
                if (itemsToClick != null)
                {
                    foreach (var item in itemsToClick)
                    {
                        ClickOnElementByClassNameAsync(item);
                    }
                }
            }
        }
    }
}