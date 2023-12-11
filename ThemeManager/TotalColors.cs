using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace ThemeManagement
{
    [SupportedOSPlatform("windows")]
    public class TotalColors
    {
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }

        public Color SelectedBackColor { get; set; }
        public Color SelectedForeColor { get; set; }


        public TotalColors(Color backColor, Color foreColor, Color selectedBackColor, Color selectedForeColor)
        {
            BackColor = backColor;
            ForeColor = foreColor;
            SelectedBackColor = selectedBackColor;
            SelectedForeColor = selectedForeColor;

        }
        public TotalColors(string backColor, string foreColor, string selectedBackColor, string selectedForeColor)
            : this(ColorTranslator.FromHtml(backColor), ColorTranslator.FromHtml(foreColor), ColorTranslator.FromHtml(selectedBackColor), ColorTranslator.FromHtml(selectedForeColor))
        {

        }

        public TotalColors() 
            : this(SystemColors.Control, 
                  SystemColors.ControlText,
                  ControlPaint.Dark(SystemColors.Control),
                  SystemColors.ControlText)
        {

        }

        public static TotalColors CreateTotalColorFromTheme(Theme theme)
        {
            return theme switch
            {
                Theme.Light => new TotalColors(SystemColors.Control, SystemColors.ControlText, ControlPaint.Dark(SystemColors.Control), SystemColors.ControlText),
                Theme.Dark => new TotalColors(SystemColors.ControlDarkDark, SystemColors.ControlLightLight, SystemColors.ControlDark, SystemColors.ControlLightLight),
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}
