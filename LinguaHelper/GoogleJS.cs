using LinguaHelper.Properties;

namespace TestProj
{
    internal static class GoogleJS
    {
        private static string LightThemeJSCode =>
           $@"document.getElementsByClassName('{Settings.Default.GoogleThemeClassName}')[0].click();
            ";
        private static string DarkThemeJSCode =>
          $@"document.getElementsByClassName('{Settings.Default.GoogleThemeClassName}')[1].click();
            ";

        public static IBrowserJS GetInstance()
        {
            return new BrowserJS(null,
                new string[] { LightThemeJSCode,DarkThemeJSCode },
                GenericJS.MainFrameJSCode,
                null);
        }
    }
}
