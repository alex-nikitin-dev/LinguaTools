﻿using System.Drawing;

namespace LinguaHelper
{
    internal class ColorThemeProvider
    {
        public ColorTheme ColorTheme { get; private set; }

        public Color Background { get; private set; }
        public Color Foreground { get; private set; }

        public uint BackgroundArgb { get => (uint)Background.ToArgb(); }

        public uint ForegroundArgb { get => (uint)Foreground.ToArgb(); }

        public ColorThemeProvider(ColorTheme colorTheme, Color foreground, Color background)
        {
            ColorTheme = colorTheme;
            Foreground = foreground;
            Background = background;
        }
        public ColorThemeProvider(ColorTheme colorTheme, string foreground, string background)
            : this(colorTheme, ColorTranslator.FromHtml(foreground), ColorTranslator.FromHtml(background))
        {

        }
    }
}
