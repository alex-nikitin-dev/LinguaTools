namespace LinguaHelper
{
    //internal static class GenericJS
    //{
    //    //DictionaryOnSelectJS
    //    public static string MainFrameJSCode =>
    //        $@"
    //        (async function()
    //     {{
    //      await CefSharp.BindObjectAsync(""dictionaryOperator1"");
    //      document.body.onmouseup = function()
    //            {{
    //                dictionaryOperator1.onselect(document.getSelection().toString());
    //            }};
    //     }})();
    //        ";

    //    //public static string MainFrameJSCode =>
    //    //   $@"
    //    //      CefSharp.BindObjectAsync('dictionaryOperator1');
    //    //      document.body.onmouseup = function()
    //    //      {{
    //    //            dictionaryOperator1.onselect(document.getSelection().toString());
    //    //            //alert(document.getSelection().toString());
    //    //      }};
    //    //    ";

    //    public static IBrowserJS GetInstance()
    //    {
    //        return new BrowserJS(null,
    //            null,
    //            MainFrameJSCode,
    //            null);
    //    }
    //}
}
