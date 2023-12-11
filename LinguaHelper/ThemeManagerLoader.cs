using LinguaHelper.Properties;
using System.Runtime.Versioning;
using ThemeManagement;
namespace LinguaHelper
{
    [SupportedOSPlatform("windows")]
    internal class ThemeManagerLoader
    {
        public static ThemeManager LoadThemeManager()
        {
            var stt = Settings.Default;
            var darkColors = new TotalColors(stt.DarkBackground,stt.DarkForeground,stt.DarkSelectedBackground,stt.DarkSelectedForegound);
            var lightColors = TotalColors.CreateTotalColorFromTheme(Theme.Light);
            var manager = new ThemeManager(lightColors, darkColors, stt.DarkTheme ? Theme.Dark : Theme.Light);
            return manager;
        }
    }
}
