using LinguaHelper.Properties;

namespace TestProj
{
    internal static class GoogleJS
    {
        private static string LightThemeJSCode => Settings.Default.GoogleLightThemeJS;
        private static string DarkThemeJSCode => Settings.Default.GoogleDarkThemeJS;
        private static string ColorThemePrepareJSCode => Settings.Default.GoogleColorThemePrepareJS;

        public static IBrowserJS GetInstance()
        {
            return new BrowserJS(null,
                new string[] { LightThemeJSCode, DarkThemeJSCode },
                GenericJS.MainFrameJSCode,
                null,
                null,
                null,
                ColorThemePrepareJSCode);
        }
    }
}
