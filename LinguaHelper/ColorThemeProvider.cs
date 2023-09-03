using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj
{
    internal class ColorThemeProvider
    {
        public  ColorTheme ColorTheme { get; private set; }

        public Color Background { get; private set; }
        public Color Foreground { get; private set; }

        public uint BackgroundArgb { get=>(uint)Background.ToArgb(); }

        public uint ForegroundArgb { get => (uint)Foreground.ToArgb(); }

        public ColorThemeProvider(ColorTheme colorTheme, Color foreground, Color background)
        {
            ColorTheme = colorTheme;
            Foreground = foreground;
            Background = background;
        }
        public ColorThemeProvider(ColorTheme colorTheme, string foreground, string background)
            :this(colorTheme,ColorTranslator.FromHtml(foreground),ColorTranslator.FromHtml(background))
        {

        }
    }
}
