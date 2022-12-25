namespace TestProj
{
    internal static class GenericJS
    {
        //DictionaryOnSelectJS
        public static string MainFrameJSCode =>
            $@"
              document.body.onmouseup = function()
              {{
                    b1.onselect(document.getSelection().toString());    
              }};
            ";

        public static IBrowserJS GetInstance()
        {
            return new BrowserJS(null,
                null,
                MainFrameJSCode,
                null);
        }
    }
}
