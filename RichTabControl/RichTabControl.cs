using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;
using ThemeManagement;
namespace RichControls
{
    [SupportedOSPlatform("windows")]
    public class RichTabControl : TabControl, IThemeable
    {
        #region properties
        public ThemeManager ThemeManager { get; private set; }

        public Theme CurrentTheme
        {
            get
            {
                return ThemeManager.CurrentTheme;
            }
            set
            {
                ThemeManager.ApplyTheme(this, value);
                Invalidate(true);
            }
        }
        #endregion

        #region .ctor
        public RichTabControl()
        {
            InitializeControl();
        }

        public RichTabControl(ThemeManager themeManager)
            : this()
        {
            ThemeManager = themeManager ?? throw new ArgumentNullException(nameof(themeManager));
        }

        private void InitializeControl()
        {
            ThemeManager = new ThemeManager();
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
                     ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            DrawMode = TabDrawMode.OwnerDrawFixed;
            Padding = new Point(0, 0);
        }
        #endregion
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var brush = new SolidBrush(ThemeManager.CurrentColors.BackColor);
            e.Graphics.FillRectangle(brush, ClientRectangle);  // Fills the background of the control
            DrawTabPages(e.Graphics); 
            DrawTabBorders(e.Graphics); 
        }

        private void DrawTabPages(Graphics graphics)
        {
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            for (int index = 0; index < TabCount; index++)
            {
                TabPage page = TabPages[index];
                Rectangle tabBounds = GetTabRect(index);
                var isSelected = SelectedIndex == index;
                var textBrush = new SolidBrush(ThemeManager.GetCurrentForeColorIf(isSelected));
                var backBrush = new SolidBrush(ThemeManager.GetCurrentBackColorIf(isSelected));

                graphics.FillRectangle(backBrush, tabBounds); // Fills the background of the tab page
                graphics.DrawString(page.Text, Font, textBrush, tabBounds, new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });
            }
        }

        private void DrawTabBorders(Graphics graphics)
        {
            if (SelectedIndex < 0) return;
            Rectangle bodyRect = TabPages[SelectedIndex].Bounds;
            bodyRect.Inflate(3, 3); // Probably need to adjust these values.
            ControlPaint.DrawBorder(graphics, bodyRect, ThemeManager.CurrentColors.BackColor, ButtonBorderStyle.Solid);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x85) // WM_NCPAINT
            {
                // Ignoring WM_NCPAINT removes borders and frames.
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }

}