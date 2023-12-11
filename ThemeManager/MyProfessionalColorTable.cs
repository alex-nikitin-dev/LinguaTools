namespace ThemeManagement
{
    public class MyProfessionalColorTable : ProfessionalColorTable
    {

        Color _backColor;
        public MyProfessionalColorTable(Color backColor)
        {
            _backColor = backColor;
        }

        public override Color ImageMarginGradientBegin => _backColor;
        public override Color ImageMarginGradientMiddle => _backColor;
        public override Color ImageMarginGradientEnd => _backColor;
    }
}
