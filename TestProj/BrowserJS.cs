namespace TestProj
{
    internal class BrowserJS : IBrowserJS
    {
        public string PrepareJSCode { get; set; }
        public string[] ColorThemeJSCode { get; set; }
        public string MainFrameJSCode { get; set; }
        public string OtherJSCode { get; set; }

        public string ForAsyncEvalJSCode { get; set; }


        public BrowserJS(string prepareJSCode = null, string[] colorThemeJSCode = null, string mainFrameJSCode = null, string otherJSCode = null, string forAsyncEvalJSCode = null)
        {
            PrepareJSCode = prepareJSCode;
            ColorThemeJSCode = colorThemeJSCode;
            MainFrameJSCode = mainFrameJSCode;
            OtherJSCode = otherJSCode;
            ForAsyncEvalJSCode = forAsyncEvalJSCode;
        }
    }
}
