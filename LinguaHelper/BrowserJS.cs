using LinguaHelper.Properties;
using System;
using System.IO;
using System.Text.Json.Serialization;

namespace LinguaHelper
{
    internal class BrowserJS
    {
        public string SpecificJSCode { get; private set; }
        public string GeneralJSCode { get; private set; }
        public string BindCefObjectsFuncName { get; private set; }
        public string LoginFuncName { get; private set; }
        public string RedirectAfterLoginFuncName { get; private set; }
        public string DeleteAdFuncName { get; private set; }
        public string[] AllItemsToClick { get; private set; }
        public string SetDarkThemeFuncName { get; private set; }
        public string SetLightThemeFuncName { get; private set; }
        public string IsJsInjectedFuncName { get; private set; }
        public string AcceptAllCookiesFuncName { get; private set; }
        public string WholeJSCode { get; private set; }
        public string SetDefaultPageTextFuncName { get; private set; }

        [JsonConstructor]
        public BrowserJS(string specificJSCodePath, string generalJSCodePath, string bindCefObjectsFuncName, string loginFuncName, string redirectAfterLoginFuncName, string deleteAdFuncName, string[] allItemsToClick, string setDarkThemeFuncName, string setLightThemeFuncName,string isJsInjectedFuncName, string acceptAllCookiesFuncName, string setDefaultPageTextFuncName)
        {
            SpecificJSCode = LoadJSCode(specificJSCodePath);
            GeneralJSCode = LoadJSCode(generalJSCodePath);
            WholeJSCode  = GeneralJSCode + Environment.NewLine + SpecificJSCode;

            BindCefObjectsFuncName = bindCefObjectsFuncName;
            LoginFuncName = loginFuncName;
            RedirectAfterLoginFuncName = redirectAfterLoginFuncName;
            DeleteAdFuncName = deleteAdFuncName;
            AllItemsToClick = allItemsToClick != null ? (string[])allItemsToClick.Clone() : new string[0];
            SetDarkThemeFuncName = setDarkThemeFuncName;
            SetLightThemeFuncName = setLightThemeFuncName;
            IsJsInjectedFuncName = isJsInjectedFuncName;
            AcceptAllCookiesFuncName = acceptAllCookiesFuncName;
            SetDefaultPageTextFuncName = setDefaultPageTextFuncName;
        }

        private string LoadJSCode(string path)
        {
            if (File.Exists(path))
                return File.ReadAllText(path);
            else
                return "";
        }
    }
}
