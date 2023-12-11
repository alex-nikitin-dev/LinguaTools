using System.Runtime.InteropServices;
using System.Runtime.Versioning;


namespace ThemeManagement
{
    public class ThemeManager
    {
        #region properties       
        public  TotalColors LightModeColors { get; set; }= TotalColors.CreateTotalColorFromTheme(Theme.Light);
        
        public  TotalColors DarkModeColors { get; set; }= TotalColors.CreateTotalColorFromTheme(Theme.Dark);
        public ThemeManager()
        {

        }

        public ThemeManager(TotalColors lightModeColors, TotalColors darkModeColors, Theme theme)
            : this()
        {
            SetTotalColors(lightModeColors, darkModeColors);
            CurrentTheme = theme;
        }

        public void SetTotalColors(TotalColors lightModeColors, TotalColors darkModeColors)
        {
            LightModeColors = lightModeColors;
            DarkModeColors = darkModeColors;
        }
        public void SetTotalColors(ThemeManager themeManager)
        {
            SetTotalColors(themeManager.LightModeColors, themeManager.DarkModeColors);
        }
        public Theme CurrentTheme { get; private set; } = Theme.Light;

        public TotalColors CurrentColors => CurrentTheme == Theme.Light ? LightModeColors : DarkModeColors;
        #endregion
        public void ApplyTheme(Control control, Theme theme)
        {
            CurrentTheme = theme;
            ApplyControlColorsRecursively(control);
        }
        public void ApplyTheme(Control control)
        {
            ApplyControlColorsRecursively(control);
        }
        private void ApplyControlColorsRecursively(Control control)
        {
            control.BackColor = CurrentColors.BackColor;
            control.ForeColor = CurrentColors.ForeColor;

            if (control is Form)
            {
                UseImmersiveDarkMode(control.Handle, CurrentTheme == Theme.Dark);
            }
            else if (control is DataGridView)
            {
                ApplyDataGridViewColors((DataGridView)control);
            }
            else if (control is MenuStrip)
            {
                foreach (ToolStripItem menuItem in ((MenuStrip)control).Items)
                {
                    SetMenuStripColorsRecursively(menuItem);
                }

                var highLightColors = ColorHelper.GetHighLightColor(CurrentColors.BackColor, CurrentColors.ForeColor, CurrentTheme);
                ((MenuStrip)control).Renderer = new MyRenderer(highLightColors.backColor, CurrentColors.BackColor);
            }

            ApplyControlChildrenColors(control);
        }
       
        [SupportedOSPlatform("windows")]
        void SetMenuStripColorsRecursively(ToolStripItem parent)
        {
            parent.BackColor = CurrentColors.BackColor;
            parent.ForeColor = CurrentColors.ForeColor;


            if (parent is ToolStripMenuItem)
            {
                var parentsDropDown = ((ToolStripMenuItem)parent).DropDown;
                ((parentsDropDown as ToolStripDropDownMenu)!).ShowCheckMargin = true;
                ((parentsDropDown as ToolStripDropDownMenu)!).ShowImageMargin = false;
                parentsDropDown.BackColor = CurrentColors.BackColor;
                parentsDropDown.ForeColor = CurrentColors.ForeColor;
                parentsDropDown.Invalidate(true);
            }

            if (parent is ToolStripDropDownItem)
            {
                foreach (ToolStripItem childItem in ((ToolStripDropDownItem)parent).DropDownItems)
                {
                    SetMenuStripColorsRecursively(childItem);
                }
            }
        }

        private void ApplyControlChildrenColors(Control control)
        {
            foreach (Control child in control.Controls)
            {
                if (child is IThemeable themeable)
                {
                    themeable.CurrentTheme = CurrentTheme;
                }
                else
                {
                    ApplyControlColorsRecursively(child);
                }
            }
        }

        private void ApplyDataGridViewColors(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.BackgroundColor = CurrentColors.BackColor;
            dgv.DefaultCellStyle.BackColor = CurrentColors.BackColor;
            dgv.DefaultCellStyle.ForeColor = CurrentColors.ForeColor;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = CurrentColors.BackColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = CurrentColors.ForeColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = CurrentColors.SelectedBackColor;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = CurrentColors.SelectedForeColor;
            dgv.RowHeadersDefaultCellStyle.BackColor = CurrentColors.BackColor;
            dgv.RowHeadersDefaultCellStyle.ForeColor = CurrentColors.ForeColor;
            dgv.RowHeadersDefaultCellStyle.SelectionBackColor = CurrentColors.SelectedBackColor;
            dgv.RowHeadersDefaultCellStyle.SelectionForeColor = CurrentColors.SelectedForeColor;
            dgv.GridColor = CurrentColors.ForeColor;
        }

        public Color GetCurrentBackColorIf(bool isSelected)
        {
            return isSelected ? CurrentColors.SelectedBackColor : CurrentColors.BackColor;
        }
        public Color GetCurrentForeColorIf(bool isSelected)
        {
            return isSelected ? CurrentColors.SelectedForeColor : CurrentColors.ForeColor;
        }


        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        internal static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (IsWindows10OrGreater(17763))
            {
                var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = enabled ? 1 : 0;
                return DwmSetWindowAttribute(handle, attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }

        private static bool IsWindows10OrGreater(int build = -1)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }
    }
}
