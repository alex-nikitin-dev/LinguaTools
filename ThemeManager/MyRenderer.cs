using System.Runtime.Versioning;

namespace ThemeManagement
{
    [SupportedOSPlatform("windows")]
    public class MyRenderer : ToolStripProfessionalRenderer
    {
        private Color _backColorOfHighlightedItem;
        //private Color _foreColorOfHighlightedItem;
        public MyRenderer(Color backColorOfHighlightedItem, Color backColor/*,Color foreColor*/)
            : base(new MyProfessionalColorTable(backColor))
        {
            _backColorOfHighlightedItem = backColorOfHighlightedItem;
            //_foreColorOfHighlightedItem = foreColor;
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
            Color backColor = e.Item.Selected ? _backColorOfHighlightedItem : e.Item.BackColor;
            using SolidBrush brush = new(backColor);
            e.Graphics.FillRectangle(brush, rc);
            // base.OnRenderMenuItemBackground(e);
        }

        //protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        //{
        //    e.TextColor = e.Item.Selected ? _foreColorOfHighlightedItem : e.Item.ForeColor;
        //    base.OnRenderItemText(e);
        //}
    }
}
