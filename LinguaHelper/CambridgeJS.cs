namespace TestProj
{
    internal static class CambridgeJS
    {
        public static IBrowserJS GetInstance()
        {
            return new BrowserJS(null,
                null,
                GenericJS.MainFrameJSCode,
                OaldJS.OtherJSCode);
        }
    }
}
