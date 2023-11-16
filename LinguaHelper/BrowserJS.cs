using System.IO;
using System.Text.Json.Serialization;

namespace LinguaHelper
{
    internal class BrowserJS
    {
        public string MainFrameJSCode { get; private set; }
        public string BindCefObjectsFuncName { get; private set; }
        public string LoginFuncName { get; private set; }
        public string RedirectAfterLoginFuncName { get; private set; }
        public string DeleteAdFuncName { get; private set; }
        public string AllItemsToClickFuncName { get; private set; }
        public string SetDarkThemeFuncName { get; private set; }
        public string SetLightThemeFuncName { get; private set; }

        [JsonConstructor]
        public BrowserJS(string mainFrameJSCodePath, string bindCefObjectsFuncName, string loginFuncName, string redirectAfterLoginFuncName, string deleteAdFuncName, string allItemsToClickFuncName, string setDarkThemeFuncName, string setLightThemeFuncName)
        {
            if (File.Exists(mainFrameJSCodePath))
                MainFrameJSCode = File.ReadAllText(mainFrameJSCodePath);
            else
                MainFrameJSCode = "";

            BindCefObjectsFuncName = bindCefObjectsFuncName;
            LoginFuncName = loginFuncName;
            RedirectAfterLoginFuncName = redirectAfterLoginFuncName;
            DeleteAdFuncName = deleteAdFuncName;
            AllItemsToClickFuncName = allItemsToClickFuncName;
            SetDarkThemeFuncName = setDarkThemeFuncName;
            SetLightThemeFuncName = setLightThemeFuncName;
        }
    }
}
