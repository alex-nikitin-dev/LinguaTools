using System.Runtime.Versioning;
namespace ThemeManagement
{
    internal class ColorHelper
    {
        [SupportedOSPlatform("windows")]
        public static (Color backColor, Color foreColor) GetHighLightColor(Color backColor, Color foreColor, Theme theme)
        {
            switch (theme)
            {
                case Theme.Dark:
                    return (ControlPaint.Light(backColor), ControlPaint.Dark(foreColor));
                case Theme.Light:
                    return (ControlPaint.Dark(backColor), ControlPaint.Light(foreColor));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
