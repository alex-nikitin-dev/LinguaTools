using System;

namespace TestProj
{
    internal class BrowserJS: IBrowserJS
    {
        public string PrepareJSCode { get; set; }
        public string MainFrameJSCode { get; set; }
        public string OtherJSCode { get; set; }

        public BrowserJS(string prepareJSCode=null, string mainFrameJSCode = null, string otherJSCode = null)
        {
            PrepareJSCode = prepareJSCode;
            MainFrameJSCode = mainFrameJSCode;
            OtherJSCode = otherJSCode;
        }
    }

    
}
